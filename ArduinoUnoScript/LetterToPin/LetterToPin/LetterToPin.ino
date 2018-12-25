/*
 * Bluetooth HC-06 (SLAVE) control from your Android phone RSB May 2016
 */
#include "Arduino.h"
#include <SoftwareSerial.h>

// Instantiate our BT object. First value is RX pin, second value TX pin
// NOTE: do NOT connect the RX directly to the Arduino unless you are using a
// 3.3v board. In all other cases connect pin 4 to a 1K2 / 2K2 resistor divider
/*
 * ---- pin 6-----> |----1K2----| to HC06 RX |----2K2----| -----> GND
 *
 * See my video at www.youtube.com/c/RalphBacon for more details.
 */
SoftwareSerial BTserial(2, 3); // RX , TX

// Our output indicator (could drive an opto-isolated relay)
byte LEDpin = 5;

// -----------------------------------------------------------------------------------
// SET UP   SET UP   SET UP   SET UP   SET UP   SET UP   SET UP   SET UP   SET UP
// -----------------------------------------------------------------------------------
void setup() {

	// LED output pin
 for  (int i = 4; i<=13; i++){
    byte pin = (byte) i;
    pinMode(pin, OUTPUT);
  }

	// Serial Windows stuff
	Serial.begin(9600);
	Serial.println("Set up complete");

	// Set baud rate of HC-06 that you set up using the FTDI USB-to-Serial module
	BTserial.begin(9600);
}

// -----------------------------------------------------------------------------------
// MAIN LOOP     MAIN LOOP     MAIN LOOP     MAIN LOOP     MAIN LOOP     MAIN LOOP
// -----------------------------------------------------------------------------------
void loop() {

	// If the HC-06 has some data (single char) for us, get it
	if (BTserial.available() > 0) {

		// Get the char
		char data = (char) BTserial.read();

		// Depending on value turn LED ON or OFF (or error message)
		switch (data) {

    case 'a': SetPinOn(true, 4); break;
    case 'A': SetPinOn(false, 4); break;
    
    case 'b': SetPinOn(true, 5); break;
    case 'B': SetPinOn(false, 5); break;
    
    case 'c': SetPinOn(true, 6); break;
    case 'C': SetPinOn(false, 6); break;
    
    case 'd': SetPinOn(true, 7); break;
    case 'D': SetPinOn(false, 7); break;
    
    case 'e': SetPinOn(true, 8); break;
    case 'E': SetPinOn(false, 8); break;
    
    case 'f': SetPinOn(true, 9); break;
    case 'F': SetPinOn(false, 9); break;
    
    case 'g': SetPinOn(true, 10); break;
    case 'G': SetPinOn(false, 10); break;
    
    case 'h': SetPinOn(true, 11); break;
    case 'H': SetPinOn(false, 11); break;
    
    case 'i': SetPinOn(true, 12); break;
    case 'I': SetPinOn(false, 12); break;
    
    case 'j': SetPinOn(true, 13); break;
    case 'J': SetPinOn(false, 13); break;
    
    case 'k': SetPinOn(true, 14); break;
    case 'K': SetPinOn(false, 14); break;
    
    

		default:
			Serial.print("NOT RECOGNISED: ");
			Serial.println(data);
			BTserial.print("Error!");
		}
	}
}

void SetPinOn(bool on, byte pin){

if(on){
      Serial.println("ON");
      digitalWrite(pin, HIGH);
      BTserial.write("ON");
  }else{
      Serial.println("OFF");
      digitalWrite(pin, LOW);
      BTserial.write("OFF");
    
    }


}
