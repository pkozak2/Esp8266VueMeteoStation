<template>
  <v-container fluid>
    <v-row v-for="(sensor,i) in sensors" :key="i">
      <v-col cols="12">
        <v-row>
          <v-col cols="12" align="center">
            <v-row>
              <v-col>{{sensor.sensorName}}</v-col>
            </v-row>
            <v-row>
              <v-col>Ostatnia aktualizacja: {{GetDateFromDateTimeOffset(sensor.measurements.insertDate)}}</v-col>
            </v-row>
          </v-col>
        </v-row>
        <v-row justify="center">
          <v-col cols="12" sm="5" md="6" lg="3" xl="3" pa-2 v-if="sensor.measurements.temperature">
            <StatsCard
              color="green"
              icon="fa-temperature-low"
              title="Temperatura"
              :value="sensor.measurements.temperature"
              smallValue="&deg;C"
            />
          </v-col>
          <!-- <v-col cols="12" sm="5" md="6" lg="3" xl="3" pa-2 v-if="sensor.measurements.temperatureF">
            <StatsCard
              color="green"
              icon="fa-temperature-low"
              title="Temperatura"
              :value="sensor.measurements.temperatureF"
              smallValue="&deg;F"
            />
          </v-col>-->
          <v-col cols="12" sm="5" md="6" lg="3" xl="3" pa-2 v-if="sensor.measurements.pm25">
            <StatsCard
              color="red"
              icon="fa-industry"
              title="PM2.5"
              :value="sensor.measurements.pm25"
              smallValue="&micro;g/m<sup>3</sup>"
            />
          </v-col>
          <v-col cols="12" sm="5" md="6" lg="3" xl="3" pa-2 v-if="sensor.measurements.pm10">
            <StatsCard
              color="red"
              icon="fa-industry"
              title="PM10"
              :value="sensor.measurements.pm10"
              smallValue="&micro;g/m<sup>3</sup>"
            />
          </v-col>
          <v-col cols="12" sm="5" md="6" lg="3" xl="3" pa-2 v-if="sensor.measurements.wifiRssi">
            <StatsCard
              color="blue"
              icon="fa-wifi"
              title="WiFi RSSI"
              :value="sensor.measurements.wifiRssi"
              smallValue="dBm"
            />
          </v-col>
          <v-col cols="12" sm="5" md="6" lg="3" xl="3" pa-2 v-if="sensor.measurements.pressureHpa">
            <StatsCard
              color="red"
              icon="fa-tachometer-alt"
              title="Ciśnienie"
              :value="sensor.measurements.pressureHpa"
              smallValue="hPa"
            />
          </v-col>
          <v-col cols="12" sm="5" md="6" lg="3" xl="3" pa-2 v-if="sensor.measurements.humidity">
            <StatsCard
              color="orange"
              icon="fa-percent"
              title="Wilgotność"
              :value="sensor.measurements.humidity"
              smallValue="%"
            />
          </v-col>
        </v-row>
        <v-row :v-if="IsGraphsDataContainsDevice(sensor.sensorId)">
          <v-col cols="12">
            <v-row justify="center">
              <v-btn-toggle v-model="toggle_exclusive" mandatory>
                <v-btn :value="12">12h</v-btn>
                <v-btn :value="24">24h</v-btn>
              </v-btn-toggle>
            </v-row>
            <v-row justify="center">
              <v-col cols="12" sm="5" md="6" lg="4" xl="4" pa-2>
                <ChartsCard
                  :chartData="GetChartData(sensor.sensorId, 'temperature', 'Temperatura (°C)')"
                  :options="chartOptions"
                >
                  <h3 class="title font-weight-light">Temperatura</h3>
                  <p
                    class="category d-inline-flex font-weight-light"
                  >Z ostatnich {{sensorHistoryHours}} godzin</p>

                  <!-- <template slot="actions">
                <v-icon class="mr-2" small>fa-cat</v-icon>
                <span class="caption grey--text font-weight-light">campaign sent 26 minutes ago</span>
                  </template>-->
                </ChartsCard>
              </v-col>
              <v-col cols="12" sm="5" md="6" lg="4" xl="4" pa-2>
                <ChartsCard
                  :chartData="GetChartData(sensor.sensorId, 'wifiRssi', 'WiFi RSSI')"
                  :options="chartOptions"
                >
                  <h3 class="title font-weight-light">WiFi RSSI</h3>
                  <p
                    class="category d-inline-flex font-weight-light"
                  >Z ostatnich {{sensorHistoryHours}} godzin</p>
                </ChartsCard>
              </v-col>
              <v-col cols="12" sm="5" md="6" lg="4" xl="4" pa-2>
                <ChartsCard
                  :chartData="GetChartData(sensor.sensorId, 'pressureHpa', 'Ciśnienie hPa')"
                  :options="chartOptions"
                >
                  <h3 class="title font-weight-light">Ciśnienie hPa</h3>
                  <p
                    class="category d-inline-flex font-weight-light"
                  >Z ostatnich {{sensorHistoryHours}} godzin</p>
                </ChartsCard>
              </v-col>
              <v-col cols="12" sm="5" md="6" lg="4" xl="4" pa-2>
                <ChartsCard
                  :chartData="GetChartData(sensor.sensorId, 'humidity', 'Wilgotność')"
                  :options="chartOptions"
                >
                  <h3 class="title font-weight-light">Wilgotność</h3>
                  <p
                    class="category d-inline-flex font-weight-light"
                  >Z ostatnich {{sensorHistoryHours}} godzin</p>
                </ChartsCard>
              </v-col>
            </v-row>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import Vue from "vue";
import StatsCard from "@/components/material/StatsCard.vue";
import ChartsCard from "@/components/material/ChartsCard.vue";
import measurementsService from "@/services/MeasuementsService.js";
import moment from "moment";
export default {
  name: "",
  components: { StatsCard, ChartsCard },
  data() {
    return {
      toggle_exclusive: 12,
      sensorHistoryHours: 12,
      sensors: [],
      graphsData: [],
      chartOptions: {
        spanGaps: false,
        responsive: true,
        aspectRatio: 2,
        tooltips: {
          mode: "index",
          intersect: false
        },
        legend: {
          labels: { fontColor: "white" }
        },
        scales: {
          xAxes: [
            {
              display: true,
              type: "time",
              time: {
                displayFormats: {
                  millisecond: "HH:mm:ss.SSS",
                  second: "HH:mm:ss",
                  minute: "HH:mm",
                  hour: "HH"
                },
                tooltipFormat: "DD-MM HH:mm"
              },
              ticks: { fontColor: "white" }
            }
          ],
          yAxes: [
            {
              ticks: { fontColor: "white" }
            }
          ]
        },
        elements: {
          point: {
            radius: 0
          }
        },
        scaleLabel: {
          display: false
        }
      }
    };
  },
  mounted() {
    measurementsService.GetUserDevices("pkozak").then(response => {
      this.sensors = response.data;
    });
  },
  methods: {
    GetDateFromDateTimeOffset(data) {
      return moment(data).format("DD-MM-YYYY HH:mm:ss");
    },
    GetSensorHistoryFromHours(deviceId) {
      measurementsService
        .GetMeasurementsFromLastHours(deviceId, this.sensorHistoryHours)
        .then(response => {
          this.AddOrReplaceSensorData(deviceId, response.data);
        });
    },
    AddOrReplaceSensorData(deviceId, data) {
      if (this.IsGraphsDataContainsDevice(deviceId)) {
        Vue.set(
          this.graphsData,
          this.graphsData.findIndex(f => f.sensorId === deviceId),
          { sensorId: deviceId, data: data }
        );
      } else {
        this.graphsData.push({ sensorId: deviceId, data: data });
      }
    },
    IsGraphsDataContainsDevice(deviceId) {
      for (var i = 0; i < this.graphsData.length; i++) {
        if (this.graphsData[i].sensorId == deviceId) {
          return true;
        }
      }
      return false;
    },
    GetChartData(deviceId, chartType, title) {
      var index = this.graphsData.findIndex(function(val) {
        return val.sensorId == deviceId;
      });
      if (index == -1) return;

      var data1 = this.graphsData[index].data.map(function(val) {
        return val[chartType];
      });

      var hours = this.graphsData[index].data.map(function(val) {
        return val["insertDate"];
      });

      var chartData = {
        labels: hours,
        datasets: [
          {
            label: title, //"Temperatura (°C)",
            backgroundColor: "#f87979",
            data: data1,
            fill: false,
            borderColor: "#f87979"
          }
        ]
      };

      return chartData;
    }
  },
  watch: {
    sensors(val) {
      val.forEach(element => {
        this.GetSensorHistoryFromHours(element.sensorId);
      });
    },
    toggle_exclusive(val) {
      this.sensorHistoryHours = val;
    },
    sensorHistoryHours() {
      this.sensors.forEach(element => {
        this.GetSensorHistoryFromHours(element.sensorId);
      });
    }
  }
};
</script>