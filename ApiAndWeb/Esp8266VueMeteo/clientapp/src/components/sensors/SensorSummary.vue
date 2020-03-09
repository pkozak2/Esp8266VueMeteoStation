<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" class="title text-center">{{ sensorName }}</v-col>
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
    <v-row>
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
      <v-col cols="12" class="text-center">
        Poziom zanieczyszczenia:
        <v-chip class="indexchip" large>
          wysoki
        </v-chip>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12" lg="8" offset-lg="2">
        <sensor-datatable
          :items="averageMeasurements"
          :loading="averageMeasurementsLoding"
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
import moment from "moment";
import StatsCard from "@/components/newmaterial/StatsCard.vue";
import ChartsCard from "@/components/newmaterial/ChartsCard.vue";
import SensorDatatable from "@/components/newmaterial/SensorDatatable.vue";
export default {
  name: "SensorSummary",
  components: { ChartsCard, StatsCard, SensorDatatable },
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
    },
    averageMeasurementsLoding: {
      type: Boolean
    },
    averageMeasurements: {
      type: Array,
      default: function() {
        return [
          { name: "PM", subname: "2.5", value: 50, percent: 10, index: 0 },
          { name: "PM", subname: "10", value: 50, percent: 10, index: 1 }
        ];
      }
    }
  },
  data() {
    return {
      averageFrom: 1
    };
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
