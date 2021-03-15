//Noah heeft niks gedaan
#include <Arduino.h>

void setup() {
  Serial.begin(9600);
}

void loop() {
  int value = analogRead(A0);
  Serial.println("Digital Value :");
  Serial.println(value);  
  delay(250);
}