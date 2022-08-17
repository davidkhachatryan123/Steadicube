#include <Wire.h>
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27, 16, 2);

String serial_data;

const int BUFFER_SIZE = 8;
char buf[BUFFER_SIZE];

void setup() {
  Serial.begin(9600);
  while (!Serial) {}

  lcd.begin();
  lcd.backlight();

  lcd.setCursor(0, 0);
  lcd.print("A=");

  lcd.setCursor(8, 0);
  lcd.print("B=");

  lcd.setCursor(0, 1);
  lcd.print("C=");

  lcd.setCursor(8, 1);
  lcd.print("D=");
}

void loop() {
  if (Serial.available() > 0)
  {
    Serial.readBytesUntil('\n', buf, BUFFER_SIZE);

    serial_data = "";

    for (int i = 0; i < BUFFER_SIZE; i++)
      serial_data += buf[i];

    if (serial_data.indexOf('A') != -1)
      PrintValue(&serial_data.substring(3, serial_data.length()), 2, 0);
    else if (serial_data.indexOf('B') != -1)
      PrintValue(&serial_data.substring(3, serial_data.length()), 10, 0);
    else if (serial_data.indexOf('C') != -1)
      PrintValue(&serial_data.substring(3, serial_data.length()), 2, 1);
    else if (serial_data.indexOf('D') != -1)
      PrintValue(&serial_data.substring(3, serial_data.length()), 10, 1);

    for (int i = 0; i < BUFFER_SIZE; i++)
      buf[i] = 0;
  }
}

void PrintValue(String* a, int x, int y)
{
  lcd.setCursor(x, y);
  lcd.print("      ");
  lcd.setCursor(x, y);
  lcd.print(*a);
}
