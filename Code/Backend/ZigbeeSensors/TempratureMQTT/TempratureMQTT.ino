// INCLUDES //


#include "PubSubClient.h"
#include <ESP8266WiFi.h>
#include <WiFiClient.h>
// ATTRIBUTES //

// MQTT attributes
const char* device_name = "TEMPSENSOR";
const char* ssid = "devolo-bcf2afde08e2";
const char* password = "TPUHHEFJCIJWLAXS";


WiFiClient wifiClient;
PubSubClient mqttClient;

// 
const int DELAY_TIME = 10000;

char data[6] = ""; // received string of XBee-nodes, ex.: 12.34

// METHODS //

void setup_wifi() {
  Serial.print((String)device_name + " Attempting to connect to ");
  Serial.println(ssid);
  
  WiFi.persistent(false);
  
  WiFi.begin(ssid, password);
  Serial.print((String)device_name + " Connecting");
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    delay(1000);
  }
  Serial.println("");
  Serial.println((String)device_name + " Connected to WiFi!");
  Serial.println((String)device_name + " IP address: ");
  Serial.println(WiFi.localIP());
}

void setup_mqtt() 
{
  mqttClient.setClient(wifiClient);
  mqttClient.setServer("test.mosquitto.org",1883);
  mqttClient.connect("EMON_CLIENT_2");
  if(mqttClient.connected()){
    Serial.println("MQTT CONNECTED");
  }
  else
  {
    Serial.println("MQTT NOT CONNECTED");
  }
  mqttClient.subscribe("EMON_CHANNEL_2");
  delay(2000);
}

void callback(char* topic, byte* payload, unsigned int length) {
  payload[length] = '\0';
  String message = (char*)payload;
}

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  setup_wifi();
  setup_mqtt();
}

void loop() {
  if (Serial.available()){               
    Serial.println("Serial AVAILABLE");                          
   // length of '6' is required for the 5 characters (12345 => "24.14") + the escape character ('\0')    
   // which gets added at a later moment


    String str = Serial.readStringUntil('\n');
    Serial.println(str);
    while(Serial.available())
    {
      //Clears the buffer
      Serial.read();                
    }
    if(str.length() > 4){ // if valid input
      str.toCharArray(data,6);
      mqttClient.publish("EMON_CHANNEL_2", data);
   }
  }
  else
  {
    Serial.println("Serial NOT AVAILABLE");
  }
  if (!mqttClient.connected()) {
    Serial.println((String)device_name + " MQTT disconnected");
    setup_mqtt();
    delay(5000);
  }

  delay(DELAY_TIME);    
}
