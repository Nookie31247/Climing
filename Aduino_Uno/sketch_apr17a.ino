const int buttonPin = 2;
volatile unsigned long lastInterruptTime = 0;

void setup() {
  pinMode(buttonPin, INPUT);
  attachInterrupt(digitalPinToInterrupt(buttonPin), buttonPressed, CHANGE);  // CHANGE로 설정
  Serial.begin(115200);
}

void loop() {
  // do nothing
}

void buttonPressed() {
  unsigned long currentTime = millis();
  if (currentTime - lastInterruptTime > 300) {
    if (digitalRead(buttonPin) == HIGH) {  // 진짜 RISING일 때만
      Serial.println("SPACE");
    }
    lastInterruptTime = currentTime;
  }
}