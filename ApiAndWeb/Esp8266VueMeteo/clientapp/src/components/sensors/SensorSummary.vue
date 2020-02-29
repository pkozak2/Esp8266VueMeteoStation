<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" class="title text-center">{{sensorName}}</v-col>
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
    <v-row>
      <v-col cols="12">datatab</v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <charts-card title="Wykres dzienny"></charts-card>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import moment from "moment";
import StatsCard from "../newmaterial/StatsCard.vue";
import ChartsCard from "@/components/newmaterial/ChartsCard.vue";
export default {
  name: "SensorSummary",
  components: { ChartsCard, StatsCard },
  props: {
    sensorName: {
      type: String,
      default: "Noname"
    },
    lastUpdate: {
      type: String,
      default: null
    },
    currentMeasurements: {
      type: Object,
      default: function() {
        return {
          temperature: "~",
          pressure: "~",
          humidity: "~"
        };
      }
    }
  },
  methods: {
    GetDateFromDateTimeOffset(data) {
      return data === null
        ? "Brak"
        : moment(data).format("DD-MM-YYYY HH:mm:ss");
    }
  }
};
</script>