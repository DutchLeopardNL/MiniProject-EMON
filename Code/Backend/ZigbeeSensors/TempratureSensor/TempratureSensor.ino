

float temprature;
void setup()
{
  Serial.begin(9600);
}

void loop()
{
  temprature = analogRead(0);
  temprature=(5.0*temprature*1000.0)/(1024*10);
  Serial.println(temprature);
  delay(5000);
}
