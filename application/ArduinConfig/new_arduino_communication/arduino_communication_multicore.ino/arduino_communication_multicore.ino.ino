// shared variables for all cores

StartOfInitialised_LMURam_Variables
EndOfInitialised_LMURam_Variables

// unused core 0 , but must be declared
void setup() {
  SerialASC.begin(2000000); //  2000000
}

void loop() {}


StartOfUninitialised_CPU2_Variables
 static int VADCcontainer[5200]; // container for measured data - will be send to UI
EndOfUninitialised_CPU2_Variables

// CPU1 Initialised Data 
StartOfInitialised_CPU2_Variables
 const int PIN_TO_SPECTROMETER = 13; // pin which allow ions to transfer into specrometer
 uint8_t gate = 4;  // positive number of POINTS to keep the gate for ions open 
 uint8_t  sampling = 25; // mikroseconds , spece between POINTS (positive number)
 const int MAX_TIME = 20000; // maximum one measurement
 const int POINTS = 5000; // maximum measurements
EndOfInitialised_CPU2_Variables


void setup2() {  
  pinMode(PIN_TO_SPECTROMETER, OUTPUT);
  pinMode(12, OUTPUT);
  pinMode(11, OUTPUT);
  analogReadResolution(12);
   
 }


void loop2() {
  delay(5); 

  if(SerialASC.available() > 0){
      char incoming = SerialASC.read();
      gate = (incoming - '0') * 2 ;
      
      incoming = SerialASC.read();
      sampling =  (incoming - '0') * 5;
     
      // reset
      for (int i = 0; i < POINTS; i++){
        VADCcontainer[i] = 0;
      }
  } 


   // time exceed 20 000us, so less then 4096 will be mesaured, based on time
  if( sampling * (POINTS + 1)  > MAX_TIME){
    spectrometerCommunicationMicroseconds();
  }else{
    spectrometerCommunicationPOINTS();
  }
  
  sendDataToUI();
}



void setup1() {}
void loop1() {}


/** time for measurement is less than 20 000, so measure
 *  maximum 4000 POINTS and send to GUI */
void spectrometerCommunicationPOINTS(){
  int gateCycleOpen = 0; // how much time to open gate for ions
  double lastMicroseconds = 0; // temporary variable for calculation
  
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
  double currentMicroseconds = micros();  // current time
  int gateCycleOpen = 0; // how much time to open gate for ions
  double lastMicroseconds = 0; // already passed time
  int px = 0;
  
  do{
    lastMicroseconds = spectrometerMeasurement(lastMicroseconds, gateCycleOpen, px);
    VADCcontainer[px] = ReadAD0(); // read data from spectrometer
    
    gateCycleOpen++;
    px++ ; 
    
  }while((lastMicroseconds - currentMicroseconds) <  MAX_TIME);
}

// wait sampling time, open gate for ions and return current time
double spectrometerMeasurement(double lastMicroseconds,  int gateCycleOpen, int px){
  double passedMicroseconds = 0;
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
    if(i - 10 < POINTS && VADCcontainer[i] == 0 && VADCcontainer[i + 1] == 0 && VADCcontainer[i + 2] == 0){
      break;
    }
     SerialASC.println(String(VADCcontainer[i]));
  }

  SerialASC.println("END");
}






