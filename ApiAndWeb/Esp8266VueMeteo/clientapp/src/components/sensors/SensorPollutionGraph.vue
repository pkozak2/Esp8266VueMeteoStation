<template>
  <div>
    <charts-card
      v-bind="graphAttrs"
      :options="graphOptions"
      :chartData="dataSets"
    ></charts-card>
  </div>
</template>
<script>
import moment from "moment";
import ChartsCard from "../../components/newmaterial/ChartsCard";
import graphMixin from "../../mixins/graphMixin";
export default {
  name: "SensorPollutionGraph",
  components: { ChartsCard },
  mixins: [graphMixin],
  props: {
    chartData: {
      type: Object
    }
  },
  data() {
    return {
      loading: true,
      dataSets: { datasets: [] }
    };
  },
  computed: {
    graphAttrs() {
      const { title, bottom, dataLoading } = this.$attrs;
      return { title, bottom, dataLoading };
    },
    graphOptions() {
      return this.generateChartOption("pm");
    }
  },
  watch: {
    chartData(val) {
      if (Object.keys(val).length === 0) {
        this.dataSets = { datasets: [] };
        return;
      }
      var localDatasets = [];
      if (val.pm25Data[0]) {
        localDatasets.push(
          this.GenerateDataset(
            val.pm25Data,
            "PM₂₅ (µg/m³)",
            "rgb(153, 102, 255)",
            "rgb(255, 99, 132)"
          )
        );
      }
      if (val.pm10Data[0]) {
        localDatasets.push(
          this.GenerateDataset(
            val.pm10Data,
            "PM₁₀ (µg/m³)",
            "rgb(255, 159, 64)",
            "rgb(255, 99, 132)"
          )
        );
      }
      this.dataSets = {
        datasets: localDatasets
      };
    }
  },
  methods: {
    GenerateDataset(values, label, color, borderColor) {
      return {
        backgroundColor: color,
        borderColor: borderColor,
        label: label,
        data: this.MapToChartTimeSeries(values),
        borderWidth: 1
      };
    },
    MapToChartTimeSeries(data) {
      return data.map(item => {
        return { t: moment(item.dateTime).toDate(), y: item.value };
      });
    }
  }
};
</script>
