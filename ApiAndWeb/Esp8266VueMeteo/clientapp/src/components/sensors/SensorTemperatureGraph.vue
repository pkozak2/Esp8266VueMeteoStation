<template>
  <div>
    <charts-card
      :data-loading="loading"
      v-bind="graphAttrs"
      :options="graphOptions1"
      :chartData="dataSets"
    ></charts-card>
  </div>
</template>
<script>
import moment from "moment";
import ChartsCard from "../../components/newmaterial/ChartsCard";
import graphMixin from "../../mixins/graphMixin";
export default {
  name: "SensorTemperatureGraph",
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
      dataSets: { datasets: [] },
      graphOptions1: this.generateChartOption()
    };
  },
  computed: {
    graphAttrs() {
      const { title, bottom, dataLoading } = this.$attrs;
      return { title, bottom, dataLoading };
    }
  },
  watch: {
    chartData(val) {
      this.loading = true;
      if (Object.keys(val).length === 0) {
        this.dataSets = { datasets: [] };
        return;
      }
      var localDatasets = [];
      debugger;
      if (val.temperatureData[0]) {
        localDatasets.push(
          this.GenerateDataset(
            val.temperatureData,
            "Temperatura (°C)",
            "rgb(255, 99, 132)"
          )
        );
      }
      if (val.heaterData[0]) {
        localDatasets.push(
          this.GenerateDataset(
            val.heaterData,
            "Temperatura detektora (°C)",
            "rgba(255, 99, 132, 0.5)",
            true
          )
        );
      }
      this.dataSets = {
        datasets: localDatasets
      };

      this.loading = false;
    }
  },
  methods: {
    GenerateDataset(values, label, borderColor, hidden) {
      return {
        borderColor: borderColor,
        label: label,
        data: this.MapToChartTimeSeries(values),
        borderWidth: 2,
        fill: false,
        hidden: hidden || false
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
