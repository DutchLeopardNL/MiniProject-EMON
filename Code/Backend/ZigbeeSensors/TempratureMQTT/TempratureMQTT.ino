

#include "PubSubClient.h"
#include <ESP8266WiFi.h>
#include <WiFiClient.h>

const char* device_name = "TEMPSENSOR";
const char* ssid = "devolo-bcf2afde08e2";
const char* password = "TPUHHEFJCIJWLAXS";


WiFiClient wifiClient;
PubSubClient mqttClient;


const int DELAY_TIME = 10000;

char data[6] = ""; // received string of XBee-nodes, ex.: 12.34

void init_wifi() {
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

void init_mqtt() 
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
  init_wifi();
  init_mqtt();
}

void loop() {
  if (Serial.available()){                                  
    String str = Serial.readStringUntil('\n');
    Serial.println(str);
    
    while(Serial.available())
    {
      //Clears the buffer
      Serial.read();                
    }

    //Check if temprature length is higher than 4 22.22 = 5
    if(str.length() > 4)
    {
      str.toCharArray(data,6);
      mqttClient.publish("EMON_CHANNEL_2", data);
    }
  }
  //Check MQTT connection
  if (!mqttClient.connected()) {
    Serial.println((String)device_name + " MQTT disconnected");
    init_mqtt();
    delay(5000);
  }

  delay(DELAY_TIME);    
}
