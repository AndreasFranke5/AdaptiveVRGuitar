#define S1_PIN A0
#define S2_PIN A1
#define S3_PIN A2

void setup() {
  Serial.begin(9600);
}

void loop() {
  int s1 = analogRead(S1_PIN); delay(5);
  int s2 = analogRead(S2_PIN); delay(5);
  int s3 = analogRead(S3_PIN); delay(5);

  // Just send raw sensor values clearly
  Serial.print(s1); Serial.print(",");
  Serial.print(s2); Serial.print(",");
  Serial.println(s3);

  delay(200); 
}
