<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" class="title text-center"
        >{{ deviceName }}
        <v-progress-circular
          indeterminate
          v-if="!deviceName"
          color="primary"
        ></v-progress-circular>
      </v-col>
    </v-row>
    <v-row justify="center">
      <v-col cols="12" sm="5" md="6" lg="3" xl="2">
        <stats-card
          color="green"
          title="Temperatura"
          icon="fa-temperature-low"
          :value="currentMeasurements.temperature"
          smallValue="&deg;C"
        ></stats-card>
      </v-col>
      <v-col cols="12" sm="5" md="6" lg="3" xl="2">
        <stats-card
          icon-size="35"
          color="orange"
          title="Ciśnienie"
          icon="fa-tachometer-alt"
          :value="currentMeasurements.pressure"
          smallValue="hPa"
        ></stats-card>
      </v-col>
      <v-col cols="12" sm="5" md="6" lg="3" xl="2">
        <stats-card
          color="blue"
          title="Wilgotność"
          icon="fa-percent"
          :value="currentMeasurements.humidity"
          smallValue="%"
        ></stats-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" class="text-center">
        Ostatnia aktualizacja:
        {{ GetDateFromDateTimeOffset(lastUpdate) }}
      </v-col>
    </v-row>
    <v-divider />
    <v-row v-if="hasAverages">
      <v-col cols="12">
        <v-row>
          <v-col class="text-center">Średnia z:</v-col>
        </v-row>
        <v-row>
          <v-col class="text-center">
            <v-btn-toggle v-model="averageFrom" mandatory color="primary">
              <v-btn :value="1">1h</v-btn>
              <v-btn :value="24">24h</v-btn>
            </v-btn-toggle>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" lg="8" offset-lg="2">
        <sensor-datatable
          :items="averageMeasurements"
          :loading="averageMeasurementsLoading"
        ></sensor-datatable>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" lg="8" offset-lg="2">
        <charts-card title="Wykres dzienny" bottom></charts-card>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
const defaultMeasurements = {
  temperature: "~",
  pressure: "~",
  humidity: "~"
};
import moment from "moment";
import StatsCard from "@/components/newmaterial/StatsCard.vue";
import ChartsCard from "@/components/newmaterial/ChartsCard.vue";
import SensorDatatable from "@/components/newmaterial/SensorDatatable.vue";
import UserSerivce from "../../services/UserService";
import DevicesService from "../../services/DevicesService";
export default {
  name: "SensorSummary",
  components: { ChartsCard, StatsCard, SensorDatatable },
  props: {
    deviceNormalizedName: {
      type: String
    },
    userName: {
      type: String
    }
  },
  data() {
    return {
      averageFrom: 1,
      deviceId: null,
      deviceName: "",
      lastUpdate: null,
      currentMeasurements: defaultMeasurements,
      averageMeasurementsLoading: false,
      averageObject: {}
    };
  },
  computed: {
    hasAverages() {
      return !!this.averageMeasurements && this.averageMeasurements[0];
    },
    averageMeasurements() {
      if (this.averageFrom == 1) {
        return (this.averageObject || {}).average_1h;
      } else if (this.averageFrom == 24) {
        return (this.averageObject || {}).average_24h;
      }
      return [];
    }
  },
  methods: {
    GetDeviceDetails(value) {
      this.deviceName = "";
      UserSerivce.GetUserDevice(this.userName, value).then(response => {
        var data = response.data;
        this.deviceName = data.deviceName;
        this.deviceId = data.deviceId;
      });
    },
    GetDateFromDateTimeOffset(data) {
      return data === null
        ? "Brak"
        : moment(data).format("DD-MM-YYYY HH:mm:ss");
    },
    GetCurrentMeasurements() {
      this.currentMeasurements = defaultMeasurements;
      this.lastUpdate = null;
      DevicesService.GetCurrentMeasurements(this.deviceId).then(response => {
        var data = response.data;
        this.currentMeasurements = {
          temperature: data.temperature || "~",
          pressure: data.pressureHpa || "~",
          humidity: data.humidity || "~"
        };

        this.lastUpdate = data.insertDate;
      });
    },
    GetAverages() {
      this.averageMeasurementsLoading = true;
      this.averageObject = {};
      DevicesService.GetAverages(this.deviceId).then(response => {
        var data = response.data;
        this.averageObject = data;
        this.averageMeasurementsLoading = false;
      });
    }
  },
  mounted() {
    this.GetDeviceDetails(this.deviceNormalizedName);
  },
  watch: {
    deviceNormalizedName(val) {
      console.log("vv: ", val);
      this.GetDeviceDetails(val);
    },
    deviceId() {
      this.GetCurrentMeasurements();
      this.GetAverages();
    }
  }
};
</script>
<style lang="scss" scoped>
.v-btn-toggle {
  min-width: 70px;
}
</style>
