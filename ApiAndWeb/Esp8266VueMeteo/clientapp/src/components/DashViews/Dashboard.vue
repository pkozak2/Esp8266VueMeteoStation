<template>
  <v-container>
    
    <!-- <v-layout>
      <v-flex sm12 class="text-sm-center">test</v-flex>
    </v-layout>-->
    <v-layout v-for="(sensor,i) in sensors" :key="i" column>
      <v-flex sm12 text-sm-center>{{sensor.sensorName}}</v-flex>
      <v-flex sm12 text-sm-center>Ostatnia aktualizacja: {{GetDateFromDateTimeOffset(sensor.measurements.insertDate)}}</v-flex>
      <!-- {{sensor.measurements}} -->
      <v-layout wrap>
        
      <v-flex sm6 xs12 md6 lg3 pa-2 v-if="sensor.measurements.temperature">
        <StatsCard
          color="green"
          icon="fa-temperature-low"
          title="Temperatura"
          :value="sensor.measurements.temperature"
          smallValue="&deg;C"
        />
      </v-flex>
      <v-flex sm6 xs12 md6 lg3 pa-2 v-if="sensor.measurements.temperatureF">
        <StatsCard
          color="green"
          icon="fa-temperature-low"
          title="Temperatura"
          :value="sensor.measurements.temperatureF"
          smallValue="&deg;F"
        /></v-flex>
        <v-flex sm6 xs12 md6 lg3 pa-2 v-if="sensor.measurements.pm25">
        <StatsCard
          color="red"
          icon="fa-industry"
          title="PM2.5"
          :value="sensor.measurements.pm25"
          smallValue="&micro;g/m<sup>3</sup>"
        /></v-flex>
        <v-flex sm6 xs12 md6 lg3 pa-2 v-if="sensor.measurements.pm10">
        <StatsCard
          color="red"
          icon="fa-industry"
          title="PM10"
          :value="sensor.measurements.pm10"
          smallValue="&micro;g/m<sup>3</sup>"
        /></v-flex>
        <v-flex sm6 xs12 md6 lg3 pa-2 v-if="sensor.measurements.wifiRssi">
        <StatsCard
          color="blue"
          icon="fa-wifi"
          title="WiFi RSSI"
          :value="sensor.measurements.wifiRssi"
          smallValue="dBm"
        /></v-flex>
        
        </v-layout>
    </v-layout>
    <v-layout>
      <v-flex sm6 xs12 md6 lg3 pa-2>
        <ChartsCard color="warning" type="Line">
          <h3 class="title font-weight-light">Completed Tasks</h3>
          <p class="category d-inline-flex font-weight-light">Last Last Campaign Performance</p>

          <template slot="actions">
            <v-icon class="mr-2" small>fa-cat</v-icon>
            <span class="caption grey--text font-weight-light">campaign sent 26 minutes ago</span>
          </template>
        </ChartsCard>
      </v-flex>
      <v-flex sm6 xs12 md6 lg3 pa-2>
        <ChartsCard color="warning" type="Line">
          <h3 class="title font-weight-light">Completed Tasks</h3>
          <p class="category d-inline-flex font-weight-light">Last Last Campaign Performance</p>

          <template slot="actions">
            <v-icon class="mr-2" small>fa-cat</v-icon>
            <span class="caption grey--text font-weight-light">campaign sent 26 minutes ago</span>
          </template>
        </ChartsCard>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import StatsCard from "@/components/material/StatsCard.vue";
import ChartsCard from "@/components/material/ChartsCard.vue";
import measurementsService from "@/services/MeasuementsService.js";
import moment from 'moment';
export default {
  name: "",
  components: { StatsCard, ChartsCard },
  data() {
    return {
      sensors: [],
      sensorsMeasurements: [],
    };
  },
  mounted(){
    measurementsService.GetUserDevices("pkozak").then(response => {
      this.sensors = response.data;
    });
  },
   methods:{
GetDateFromDateTimeOffset(data){
  
  return moment(data).format("DD-MM-YYYY HH:mm:ss");
}
  }
};
</script>