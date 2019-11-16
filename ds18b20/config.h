// Plik konfiguracyjny config.h.

// *******************************************************
// ********************* DANE WI-FI **********************
// *******************************************************

// Nazwa sieci Wi-Fi 2,4 GHz (SSID).
// Uwaga! Wemos D1 Pro nie połączy się z siecią 5 GHz.
const char *WIFI_SSID = "__";

// Hasło do sieci Wi-Fi
const char *WIFI_PASSWORD = "__";

// *******************************************************
// ****************** USTAWIENIA STACJI ******************
// *******************************************************

// Wprowadź napięcie mierzone na woltomierzu cyfrowym oraz
// napięcie zgłaszane przez Wemos D1 Pro.
// Jeśli nie wykonałeś procedury kalibracji, nie zmieniaj
// poniższych wartości.
const float dmmVoltage = 4.45;
const float adcVoltage = 4.45;

// Okres aktualizacji danych w sekundach.
// Musi być dłuższy niż 15 sekund.
// Sugerowane 360, 600 lub 900 sekund.
const long SLEEP_INTERVAL = 300; // np.: 360

// *******************************************************
// ****************** API Server *******************
// *******************************************************

// Adres api do aktualizacji danych
const char *IOT_SERVER = "__";

// Port HTTP
const char IOT_SERVER_PORT = 80;

// Path
const char *IOT_PATH = "__";

// httpUsername
const char *httpUsername = "__";

// httpPassword
const char *httpPassword = "__";
