/****************************************************
 * Arduino Sketch: Shape-Based Chord Detection + RC Filter
 * - Reads 3 SoftPot sensors on A0, A1, A2 (with resistor+cap)
 * - Sorts them in descending order to detect chord shape (A, D, E)
 * - Outputs "ChordName|val1,val2,val3" over Serial at 9600 baud
 *
 * 2025-03-xx
 ****************************************************/

#define S1_PIN A0
#define S2_PIN A1
#define S3_PIN A2

// Toggle this to test shape detection without real sensors
#define SIMULATION_MODE true 
#define CYCLE_INTERVAL 3000  // ms to switch chord in simulation

// Threshold for ignoring no-touch floating (~500)
#define NO_TOUCH_MIN 400
#define NO_TOUCH_MAX 600

// Minimal difference between High and Low for shape detection
#define MIN_DIFF 10

// For simulation
unsigned long lastSwitchTime = 0;
int currentFakeChord = 0; // cycles 0=A, 1=D, 2=E

void setup() {
  Serial.begin(9600);
}

void loop() {
  int s1, s2, s3;

  if (SIMULATION_MODE) {
    // Switch chord every 3 seconds
    if (millis() - lastSwitchTime > CYCLE_INTERVAL) {
      lastSwitchTime = millis();
      currentFakeChord = (currentFakeChord + 1) % 3;
    }
    // Fake sensor values for chord shapes:
switch (currentFakeChord) {
  case 0: s1 = 800; s2 = 600; s3 = 400; break;  // A
  case 1: s1 = 700; s2 = 200; s3 = 600; break;  // D
  case 2: s1 = 200; s2 = 300; s3 = 800; break;  // E
}

  } else {
    // Real sensor reading:
    s1 = analogRead(S1_PIN);
    delay(5); // let RC settle
    s2 = analogRead(S2_PIN);
    delay(5);
    s3 = analogRead(S3_PIN);
    delay(5);
  }

  // Convert raw readings into a chord name
  String chordName = detectChordShape(s1, s2, s3);

  // Output e.g. "A|60,50,55"
  Serial.print(chordName);
  Serial.print("|");
  Serial.print(s1); Serial.print(",");
  Serial.print(s2); Serial.print(",");
  Serial.println(s3);

  delay(200); 
}

/*****************************************************
 * detectChordShape():
 *  - Filters out 'no-touch' readings
 *  - Sorts in descending order
 *  - Matches pattern for A, D, E
 *****************************************************/
String detectChordShape(int raw1, int raw2, int raw3) {
  int s1 = filterNoTouch(raw1);
  int s2 = filterNoTouch(raw2);
  int s3 = filterNoTouch(raw3);

  // If all -1 => no real touches
  if (s1 == -1 && s2 == -1 && s3 == -1) {
    return "None";
  }

  // Sort them descending
  int arr[3] = {s1, s2, s3};
  int idx[3] = {1, 2, 3};

  for(int i=0; i<2; i++) {
    for(int j=0; j<2-i; j++){
      if (arr[j] < arr[j+1]) {
        int tempVal = arr[j];
        arr[j] = arr[j+1];
        arr[j+1] = tempVal;

        int tempIdx = idx[j];
        idx[j] = idx[j+1];
        idx[j+1] = tempIdx;
      }
    }
  }

  // Check difference high-low
  int diffHL = arr[0] - arr[2];
   if (diffHL < MIN_DIFF) {
   return "None";
  }

  // Pattern matching
  // Example A shape => (S1=High, S2=Mid, S3=Low) => index[0]==1, index[1]==2, index[2]==3
  if (idx[0] == 1 && idx[1] == 2 && idx[2] == 3) {
    return "A";
  }
  // D shape => (S1=High, S3=Mid, S2=Low) => index[0]==1, index[1]==3, index[2]==2
  if (idx[0] == 1 && idx[1] == 3 && idx[2] == 2) {
    return "D";
  }
  // E shape => (S3=High, S2=Mid, S1=Low) => index[0]==3, index[1]==2, index[2]==1
  if (idx[0] == 3 && idx[1] == 2 && idx[2] == 1) {
    return "E";
  }

  return "None";
}

/*****************************************************
 * filterNoTouch():
 *  Returns -1 if sensor is around 500 => no press
 *  Otherwise returns rawVal
 *****************************************************/
int filterNoTouch(int val) {
  if (val >= NO_TOUCH_MIN && val <= NO_TOUCH_MAX) {
    return -1; // no-touch
  }
  return val;
}

