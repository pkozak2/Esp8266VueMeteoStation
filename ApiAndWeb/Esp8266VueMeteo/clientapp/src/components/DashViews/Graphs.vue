<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" class="title text-center">
        {{ deviceName }}
        <v-progress-circular
          indeterminate
          v-if="!deviceName"
          color="primary"
        ></v-progress-circular>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" lg="4" offset-lg="2">
        <SensorPollutionGraph
          :dataLoading="averagePollutionGraphDataLoading"
          :chartData="averagePollutionGraphData"
        ></SensorPollutionGraph>
      </v-col>
      <v-col cols="12" lg="4">
        <SensorTemperatureGraph
          :dataLoading="averageTemperatureGraphDataLoading"
          :chartData="averageTemperatureGraphData"
        ></SensorTemperatureGraph>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import SensorTemperatureGraph from "../sensors/SensorTemperatureGraph";
import SensorPollutionGraph from "../sensors/SensorPollutionGraph";
import UserSerivce from "../../services/UserService";
import DevicesService from "../../services/DevicesService";
export default {
  name: "SensorSummary",
  components: { SensorPollutionGraph, SensorTemperatureGraph },
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
      averagePollutionGraphData: {},
      averagePollutionGraphDataLoading: false,
      averageTemperatureGraphData: {},
      averageTemperatureGraphDataLoading: false
    };
  },
  computed: {},
  methods: {
    GetDeviceDetails(value) {
      this.deviceName = "";
      UserSerivce.GetUserDevice(this.userName, value).then(response => {
        var data = response.data;
        this.deviceName = data.deviceName;
        this.deviceId = data.deviceId;
      });
    },

    GetPollutionGraphData() {
      this.averagePollutionGraphData = {};
      this.averagePollutionGraphDataLoading = true;
      var days = this.averageFrom == 1 ? 1 : 7;
      DevicesService.GetPollutionGraphData(this.deviceId, days).then(
        response => {
          var data = response.data;
          this.averagePollutionGraphData = data;
          this.averagePollutionGraphDataLoading = false;
        }
      );
    },
    GetTemperatureGraphData() {
      this.averageTemperatureGraphData = {};
      this.averageTemperatureGraphDataLoading = true;
      var days = this.averageFrom == 1 ? 1 : 7;
      DevicesService.GetTemperatureGraphData(this.deviceId, days).then(
        response => {
          var data = response.data;
          this.averageTemperatureGraphData = data;
          this.averageTemperatureGraphDataLoading = false;
        }
      );
    }
  },
  mounted() {
    this.GetDeviceDetails(this.deviceNormalizedName);
  },
  watch: {
    deviceNormalizedName(val) {
      this.GetDeviceDetails(val);
    },
    deviceId() {
      this.GetPollutionGraphData();
      this.GetTemperatureGraphData();
    },
    averageFrom() {
      this.GetAverages();
      this.GetPollutionGraphData();
    }
  }
};
</script>
<style lang="scss" scoped>
.v-btn-toggle {
  min-width: 70px;
}
</style>
