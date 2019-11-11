// shared variables for all cores
//#include <Arduino.h>
StartOfInitialised_LMURam_Variables
 static uint8_t gate = 2;  // positive number of POINTS to keep the gate for ions open 
 static uint8_t  sampling = 5; // mikroseconds , spece between POINTS (positive number)
EndOfInitialised_LMURam_Variables

// unused core 0 , but must be declared
void setup() {}
void loop() {}




// CPU1 Initialised Data 
StartOfInitialised_CPU2_Variables
  String incoming = ""; 
EndOfInitialised_CPU2_Variables

void setup2() {
  SerialASC.begin(2000000);
}

// CORE 0 only read user input -  expected income: "15 12" .. 15 = gate, 12 = sampling
void loop2() {
  if (SerialASC.available() > 0){
    incoming = SerialASC.readString();
    gate = incoming.substring(0 , incoming.indexOf(' ')).toInt();
    sampling = incoming.substring(incoming.indexOf(' '), incoming.length() - 1).toInt();
  }
}






StartOfUninitialised_CPU1_Variables
 static int VADCcontainer[8000]; // container for measured data - will be send to UI
EndOfUninitialised_CPU1_Variables

// CPU1 Initialised Data 
StartOfInitialised_CPU1_Variables
  const int PIN_TO_SPECTROMETER = 13; // pin which allow ions to transfer into specrometer
  const int MAX_TIME = 20480; // 20 480 mikroseconds, maximum one measurement
  const int POINTS = 8000; // maximum 4096 measurements
EndOfInitialised_CPU1_Variables


void setup1() {
  pinMode(PIN_TO_SPECTROMETER, OUTPUT);
  analogReadResolution(12);

}

// 1.) keep the gate open for ion throughput into spectrometer
// 2.) measure data from spectrometer
// 3.) send all data to UI
void loop1() {
  
  // time exceed 20 480us, so less then 4096 will be mesaured, based on time
  if( sampling * (POINTS + 1)  > MAX_TIME){
    spectrometerCommunicationMicroseconds();
  }else{
    spectrometerCommunicationPOINTS();
  }
  
  sendDataToUI();
}



/** time for measurement is less than 20 000, so measure
 *  maximum 4096 POINTS and send to GUI */
void spectrometerCommunicationPOINTS(){
  int gateCycleOpen = 0; // how much time to open gate for ions
  int lastMicroseconds = 0; // temporary variable for calculation
  
  for (int i = 0; i < POINTS; i++){
    lastMicroseconds = spectrometerMeasurement(lastMicroseconds, gateCycleOpen, i);
    gateCycleOpen++;
    VADCcontainer[i] = ReadAD0(); // read data from spectrometer
  }
}


/** time for measurement exceed 20 000 microseconds
 *  so measure maximum 20 000 microsends (i. e. 2000 POINTS
 *  and send it to GUI. */
void spectrometerCommunicationMicroseconds(){
  int currentMicroseconds = micros();  // current time
  int gateCycleOpen = 0; // how much time to open gate for ions
  int lastMicroseconds = 0; // already passed time
  int px = 0;
  
  do{
    lastMicroseconds = spectrometerMeasurement(lastMicroseconds, gateCycleOpen, px);
    VADCcontainer[px] = ReadAD0(); // read data from spectrometer
    
    gateCycleOpen++;
    px++ ; 
    
  }while((lastMicroseconds - currentMicroseconds) <  MAX_TIME);
}

// wait sampling time, open gate for ions and return current time
int spectrometerMeasurement(int lastMicroseconds,  int gateCycleOpen, int px){
  int passedMicroseconds = 0;
  do {
      passedMicroseconds = micros();
    } while (passedMicroseconds - lastMicroseconds < sampling);

    // open the gate for ions
    if(gateCycleOpen < gate){
      digitalWrite(PIN_TO_SPECTROMETER, 1);
    }else{
      digitalWrite(PIN_TO_SPECTROMETER, 0); // Fast_digitalWrite
    }
  
    return passedMicroseconds;
}

void sendDataToUI(){
  SerialASC.println("START");

  for (int i = 0; i < POINTS; i++){
      SerialASC.println(String(VADCcontainer[i]));
  }

  SerialASC.println("END");
}





