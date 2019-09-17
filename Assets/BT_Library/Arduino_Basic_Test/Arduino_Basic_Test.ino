int potentiometerValue;
/*
 * incluir las demas variales!
 * 1 inicia control bimanual
 * 2 inicia ***
 * 3 inicia ***
 * ***
 */
char incomingByte;
char comparison = '1';
char Stop = '0';
bool control = false;
void setup () {
  Serial.begin(9600);
}
void loop() {
  incomingByte = Serial.read();
  potentiometerValue = analogRead(A0);
  if(incomingByte == comparison) {
    control = true;
  }
  if(incomingByte == Stop) {
    control = false;
    Serial.println("STOP");
    delay(100);
  }
  if(control == true) {
    Serial.println(potentiometerValue);
    delay(100);
  }
  delay(100);
}
