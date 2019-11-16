#include <ESP8266WebServer.h>
#include <OneWire.h>
#include <DallasTemperature.h>
#include <base64.h>

#include "config.h"

// Data wire is plugged into port D2 on the ESP8266
#define ONE_WIRE_BUS D2

// Setup a oneWire instance to communicate with any OneWire devices
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature.
DallasTemperature sensors(&oneWire);

float tempSensor1;

uint8_t sensor1[8] = {0x10, 0xF4, 0x5B, 0x8A, 0x02, 0x08, 0x00, 0x4B};

String esp_chipid;
String basic_auth_custom = "";
const char data_first_part[] PROGMEM = "{\"esp8266id\": \"{v}\", \"sensordatavalues\":[";

//ESP8266WebServer server(80);

// void setup()
// {
//   Serial.begin(115200);
//   delay(100);

//   sensors.begin();

//   Serial.println("Connecting to ");
//   Serial.println(ssid);

//   //connect to your local wi-fi network
//   WiFi.begin(ssid, password);

//   //check wi-fi is connected to wi-fi network
//   while (WiFi.status() != WL_CONNECTED)
//   {
//     delay(1000);
//     Serial.print(".");
//   }
//   Serial.println("");
//   Serial.println("WiFi connected..!");
//   Serial.print("Got IP: ");
//   Serial.println(WiFi.localIP());

//   server.on("/", handle_OnConnect);
//   server.onNotFound(handle_NotFound);

//   server.begin();
//   Serial.println("HTTP server started");
// }
// void loop()
// {
//   server.handleClient();
// }

// void handle_OnConnect()
// {
//   sensors.requestTemperatures();
//   tempSensor1 = sensors.getTempC(sensor1); // Gets the values of the temperature
//   server.send(200, "text/html", SendHTML(tempSensor1));
// }

// void handle_NotFound()
// {
//   server.send(404, "text/plain", "Not found");
// }

// String SendHTML(float tempSensor1)
// {
//   String ptr = "<!DOCTYPE html> <html>\n";
//   ptr += "<head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, user-scalable=no\">\n";
//   ptr += "<title>ESP8266 Temperature Monitor</title>\n";
//   ptr += "<style>html { font-family: Helvetica; display: inline-block; margin: 0px auto; text-align: center;}\n";
//   ptr += "body{margin-top: 50px;} h1 {color: #444444;margin: 50px auto 30px;}\n";
//   ptr += "p {font-size: 24px;color: #444444;margin-bottom: 10px;}\n";
//   ptr += "</style>\n";
//   ptr += "</head>\n";
//   ptr += "<body>\n";
//   ptr += "<div id=\"webpage\">\n";
//   ptr += "<h1>ESP8266 Temperature Monitor</h1>\n";
//   ptr += "<p>Living Room: ";
//   ptr += tempSensor1;
//   ptr += "&deg;C</p>";
//   ptr += "</div>\n";
//   ptr += "</body>\n";
//   ptr += "</html>\n";
//   return ptr;
// }

void setup()
{
  Serial.begin(115200);

  esp_chipid = String(ESP.getChipId());
  // pinMode(LED_BUILTIN, OUTPUT);

  Serial.println("ChipId:");
  Serial.println(String(esp_chipid));

  sensors.begin();

  logonToRouter();

  create_basic_auth_strings();

  String sensorsData = readSensorsData();
  printToSerialPort(sensorsData);
  printToApi(sensorsData);

  // all done, now go to sleep
  blinkLED(3); // notify the user
  enterSleep(SLEEP_INTERVAL);
} //setup()

void loop()
{
  // there is nothing to do here
  // everything is done in setup()
} // loop()

/*****************************************************************
 * convert value to json string                                  *
 *****************************************************************/
String Value2Json(const String &type, const String &value)
{
  String s = F("{\"value_type\":\"{t}\",\"value\":\"{v}\"},");
  s.replace("{t}", type);
  s.replace("{v}", value);
  return s;
}
/*****************************************************************
 * convert float to string with a                                *
 * precision of two (or a given number of) decimal places        *
 *****************************************************************/
String Float2String(const double value)
{
  return Float2String(value, 2);
}

String Float2String(const double value, uint8_t digits)
{
  // Convert a float to String with two decimals.
  char temp[15];

  dtostrf(value, 13, digits, temp);
  String s = temp;
  s.trim();
  return s;
}

/*****************************************************************
 * Base64 encode user:password                                   *
 *****************************************************************/
void create_basic_auth_strings()
{
  basic_auth_custom = base64::encode(String(httpUsername) + ":" + String(httpPassword));
}

void logonToRouter()
{
  String exitMessage = "";
  int count = 0;
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD);
  while (WiFi.status() != WL_CONNECTED)
  {
    count++;
    if (count > 15)
    {
      switch (WiFi.status())
      {
      case 1:
        exitMessage = "Sieć Wi-Fi niedostępna lub\nStacja zbyt daleko od punktu dostępowego lub\nNiepoprawny SSID lub hasło lub\nAccess Poin nie pracuje na częstotliwości 2.4GHz.";
        break;
      case 2: // will never show this condition
        exitMessage = "Skanowanie sieci zakończone.";
        break;
      case 3: // will never show this condition
        exitMessage = "Połączono.";
        break;
      case 4:
        exitMessage = "Błąd połączenia.";
        break;
      case 5:
        exitMessage = "Utracono połączenie.";
        break;
      case 6:
        exitMessage = "Rozłączono.";
        break;
      } // switch
      Serial.print("WiFi fail: ");
      Serial.println(exitMessage);
      blinkLED(5);    // notify the user
      enterSleep(60); // retry after 1 minute
    }                 //if > 15
    // otherwise if < 15 blink LED and wait 500ms before checking connection
    blinkLED(1); // blink LED on each attempt to connect
    delay(500);  // one-half second delay between checks
    Serial.print("");
  } //while not connected
  Serial.println("");
  Serial.print("Połączono z siecią Wi-Fi. Otrzymany adres IP: ");
  Serial.println(WiFi.localIP().toString()); // is toString necessary?
} // logonToRouter

String readSensorsData()
{
  String data = "";
  data = readTemperature();
  data += readWifiRssi();
  data += readCellVoltage();

  return data;
}

// *******************************************************
// ******** READ DS18B20 into string              ********
// *******************************************************
String readTemperature()
{

  for (int i = 0; i <= 3; i++)
  {
    sensors.requestTemperatures();
    tempSensor1 = sensors.getTempC(sensor1); // Gets the values of the temperature
    delay(50);                               // Provide some delay to let sensor settle
  }

  return Value2Json(F("DS18B20_temperature"), Float2String(tempSensor1));
}

// *******************************************************
// ******** READ WIFI RSSI into string            ********
// *******************************************************
String readWifiRssi()
{
  long ssi = 0;
  for (int i = 0; i <= 3; i++)
  {
    ssi = WiFi.RSSI(); // Gets the values of the rssi
    delay(50);         // Provide some delay to let sensor settle
  }

  return Value2Json(F("wifi_rssi"), Float2String(ssi));
}

// *******************************************************
// ******** READ Cell voltage into string            ********
// *******************************************************
String readCellVoltage()
{
  float fudgeFactor = dmmVoltage / adcVoltage;
  long cv = 0;
  for (int i = 0; i <= 3; i++)
  {
    // read analog voltage from the Analog to Digital Converter
    // on D1 Mini this is 0 - 1023 for voltages 0 to 3.2V
    // the D1M-WX1 has an external resistor to extend the range to 5.0 Volts
    cv = 5.0 * analogRead(A0) * fudgeFactor / 1023.0;
    delay(50); // Provide some delay to let sensor settle
  }

  return Value2Json(F("cell_voltage"), Float2String(cv));
}

// *******************************************************
// ******************** enterSleep ***********************
// *******************************************************
void enterSleep(long sleep)
{
  // sleep is in seconds
  Serial.print("Wejście w tryb głębokiego snu na: ");
  Serial.print(sleep);
  Serial.println(" sekund.");
  delay(200); // delay to let things settle
  // WAKE_RF_DEFAULT wakes the ESP8266 with WiFi enabled
  ESP.deepSleep(sleep * 1000000L, WAKE_RF_DEFAULT);
} // enterSleep()

// *******************************************************
// ******************** Blink LED ************************
// *******************************************************
// this flashes the onboard LED to indicate various internal messages
void blinkLED(int flashes)
{
  // set LED_BUILTIN pin to Output mode in setup()
  for (int i = 0; i < flashes; i++)
  {
    //digitalWrite(LED_BUILTIN, LOW);  // Turn the LED *ON*
    //delay(20);                       // short flash for low energy consumption
    //digitalWrite(LED_BUILTIN, HIGH); // Turn the LED *OFF*
    //delay(250);                      // time between flashes
  }
} // blinkLED()

// *******************************************************
// ********** PRINT RAW DATA TO THE SERIAL PORT **********
// *******************************************************
void printToSerialPort(String dataJson)
{
  // '\t' is the C++ escape sequence for tab
  // header line
  Serial.println("----------------------------------------------------");
  // data line
  Serial.print("Dane\t");
  Serial.println(dataJson);
  Serial.println("----------------------------------------------------");
} // printToSerialPort()

void printToApi(String dataJson)
{
  String data = FPSTR(data_first_part);
  data.replace("{v}", String(esp_chipid));

  data += readTemperature();
  data += "]}";

  sendData(data);
}

void sendData(const String &data)
{
  Serial.println(F("Start connecting to api"));
  String request_head = F("POST ");
  request_head += String(IOT_PATH);
  request_head += F(" HTTP/1.1\r\n");
  request_head += F("Host: ");
  request_head += String(IOT_SERVER) + "\r\n";
  request_head += F("Content-Type: ");
  request_head += F("application/json\r\n");
  if (strlen(basic_auth_custom.c_str()) != 0)
  {
    request_head += F("Authorization: Basic ");
    request_head += String(basic_auth_custom) + "\r\n";
  }
  request_head += F("X-Sensor: esp8266-");
  request_head += esp_chipid + "\r\n";
  request_head += F("Content-Length: ");
  request_head += String(data.length(), DEC) + "\r\n";
  request_head += F("Connection: close\r\n\r\n");

  const auto doConnect = [=](WiFiClient *client) -> bool {
    client->setNoDelay(true);
    client->setTimeout(20000);

    if (!client->connect(IOT_SERVER, IOT_SERVER_PORT))
    {
      Serial.println(F("connection failed"));
      return false;
    }
    return true;
  };

  const auto doRequest = [=](WiFiClient *client) {
    // send request to the server
    client->print(request_head);

    client->println(data);

    Serial.println(data);

    // wait for response
    int retries = 20;
    while (client->connected() && !client->available())
    {
      delay(100);
      wdt_reset();
      if (!--retries)
        break;
    }

    // Read reply from server and print them
    while (client->available())
    {
      char c = client->read();
      Serial.print(String(c));
    }
    client->stop();
    Serial.println(F("\nclosing connection\n----\n\n"));
    //debug_out(F("\nclosing connection\n----\n\n"), DEBUG_MIN_INFO, 1);
  };

  // Use WiFiClient class to create TCP connections
  if (IOT_SERVER_PORT == 443)
  {
    WiFiClientSecure client_s;
    if (doConnect(&client_s))
    {
      doRequest(&client_s);
    }
  }
  else
  {
    WiFiClient client;
    if (doConnect(&client))
    {
      doRequest(&client);
    }
  }
  Serial.println(F("End connecting to api"));
  Serial.println(String(IOT_SERVER));

  wdt_reset(); // nodemcu is alive
  yield();
}
