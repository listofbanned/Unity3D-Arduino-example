int potentiometerRightValue;
int potentiometerLeftValue;
// boton
/*
 * int potentiometerLeftValue;
 **** digital variables ****
 * 1 bimanual
 * 2 prueba2
 * 3 prueba3
 * ***
 */
char incomingByte;
char firstGame = '1';
char secondGameR = "2R";
char secondGameL = "2L";
char thirdGame = '3';
char fourGame = '4';
char fiveGame = '5';
char sixGame = '6';
char sevenGame = '7';
char eightGame = '8';
char nineGame = '8';
char Stop = '0';
bool bimanual = false;
bool anticipationR = false;
bool anticipationL = false;
bool auditive = false;
bool reactimetry = false;
bool dazzling = false;
bool visual = false;
bool colors = false;
bool Ishihara = false;
bool foria = false;
void setup () {
  Serial.begin(9600);
}
void loop() {
  incomingByte = Serial.read();
  potentiometerRightValue = analogRead(A0);
  potentiometerLeftValue = analogRead(A1);
  if(incomingByte == Stop) {
    control = false;
    //Serial.println(incomingByte); //print results from apk
    Serial.println("HAS STOPPED");
    delay(100);
  }
  if(incomingByte == firstGame) {
    bimanual = true;
    anticipationR = false;
    anticipationL = false;
    auditive = false;
    reactimetry = false;
    dazzling = false;
    visual = false;
    colors = false;
    Ishihara = false;
    foria = false;
    Serial.println("FirstGame");
  }
  if(incomingByte == secondGameR) {
    bimanual = false;
    anticipationR = true;
    anticipationL = false;
    auditive = false;
    reactimetry = false;
    dazzling = false;
    visual = false;
    colors = false;
    Ishihara = false;
    foria = false;
    Serial.println("SecondGameR");
  }
  if(incomingByte == secondGameL) {
    bimanual = false;
    anticipationR = false;
    anticipationL = true;
    auditive = false;
    reactimetry = false;
    dazzling = false;
    visual = false;
    colors = false;
    Ishihara = false;
    foria = false;
    Serial.println("SecondGameL");
  }
  if(incomingByte == thirdGame) {
    bimanual = false;
    anticipationR = false;
    anticipationL = false;
    auditive = true;
    reactimetry = false;
    dazzling = false;
    visual = false;
    colors = false;
    Ishihara = false;
    foria = false;
    Serial.println("ThirdGame");
  }
  if(incomingByte == fourGame) {
    bimanual = false;
    anticipationR = false;
    anticipationL = false;
    auditive = false;
    reactimetry = true;
    dazzling = false;
    visual = false;
    colors = false;
    Ishihara = false;
    foria = false;
    Serial.println("FourGame");
  }
  if(incomingByte == fiveGame) {
    bimanual = false;
    anticipationR = false;
    anticipationL = false;
    auditive = false;
    reactimetry = false;
    dazzling = true;
    visual = false;
    colors = false;
    Ishihara = false;
    foria = false;
    Serial.println("FiveGame");
  }
  if(incomingByte == sixGame) {
    bimanual = false;
    anticipationR = false;
    anticipationL = false;
    auditive = false;
    reactimetry = false;
    dazzling = false;
    visual = true;
    colors = false;
    Ishihara = false;
    foria = false;
    Serial.println("SixGame");
  }
  if(incomingByte == sevenGame) {
    bimanual = false;
    anticipationR = false;
    anticipationL = false;
    auditive = false;
    reactimetry = false;
    dazzling = false;
    visual = false;
    colors = true;
    foria = false;
    Serial.println("SevenGame");
  }
  if(incomingByte == eightGame) {
    bimanual = false;
    anticipationR = false;
    anticipationL = false;
    auditive = false;
    reactimetry = false;
    dazzling = false;
    visual = false;
    colors = false;
    Ishihara = true;
    foria = false;
    Serial.println("EightGame");
  }
  if(incomingByte == nineGame) {
    bimanual = false;
    anticipationR = false;
    anticipationL = false;
    auditive = false;
    reactimetry = false;
    dazzling = false;
    visual = false;
    colors = false;
    Ishihara = false;
    foria = true;
    Serial.println("NineGame");
  }
  if(bimanual == true) {
    Serial.print("R");
    Serial.println(potentiometerRightValue);
    Serial.print("L");
    Serial.println(potentiometerLeftValue);
    delay(100);
  }
  if(anticipationR == true) {
    //boton read(a0);
    //if(boton == HIGH)
    Serial.println("buttonRightPressesd");
  }
  if(anticipationR == true) {
    //boton2 read(a1);
    //if(boton2 == HIGH)
    Serial.println("buttonLeftPressesd");
  }
  if(auditive == true) {
    //botonAud read()
    //botonAud2 read();
    //if(botonAud == HIGH)
    Serial.println("buttonAuditiveRight");
    //if(botonAud2 == HIGH)
    Serial.println("buttonAuditiveLeft");
  }
  if(reactimetry == true) {
    //botonAct read()
    //botonAct2 read();
    //if(botonAct == HIGH)
    Serial.println("buttonReactimetryRight");
    //if(botonAct2 == HIGH)
    Serial.println("buttonReactimetryLeft");
  }
  //dazzling isn't function in arduino, or results for print
  if(visual == true) {
    //boton read();
    //if(boton == HIGH)
    Serial.println("visualButtonPressed");
  }
  if(colors == true) {
    //boton read();
    //if(boton == HIGH)
    Serial.println("colorsButtonPressed");
  }
  if(Ishihara == true) {
    //boton read();
    //if(boton == HIGH)
    Serial.println("IshiharaButtonPressed");
  }
  delay(100);
}