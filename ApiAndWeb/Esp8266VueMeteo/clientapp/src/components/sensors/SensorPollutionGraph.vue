<template>
  <div>
    <charts-card
      v-if="!loading"
      v-bind="graphAttrs"
      :options="graphOptions"
      :chartData="dataSets"
    ></charts-card>
    {{ dataSets }}
    <!-- <pre>{{ chartData }}</pre> -->
  </div>
</template>
<script>
const graphOptionsDefault = {
  annotation: {
    annotations: []
  },
  scales: {
    yAxes: [
      {
        display: true,
        scaleLabel: {
          display: false
        },
        ticks: {
          fontColor: "white",
          suggestedMin: 0,
          suggestedMax: 60
        }
      }
    ]
  }
};
import ChartsCard from "../../components/newmaterial/ChartsCard";
export default {
  name: "SensorPollutionGraph",
  components: { ChartsCard },
  props: {
    chartData: {
      type: Object
    }
  },
  data() {
    return {
      loading: true,
      dataSets: {},
      graphOptions: graphOptionsDefault
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
      if (Object.keys(val).length === 0) return;
      this.loading = true;
      this.graphOptions.annotation.annotations = [];
      if ((this.chartData || {}).pm25Limit) {
        this.graphOptions.annotation.annotations.push(
          this.GenerateAnnotation(
            (this.chartData || {}).pm25Limit,
            "PM₂₅ limit",
            "purple"
          )
        );
      }
      if ((this.chartData || {}).pm10Limit) {
        this.graphOptions.annotation.annotations.push(
          this.GenerateAnnotation(
            (this.chartData || {}).pm10Limit,
            "PM₁₀ limit",
            "orange"
          )
        );
      }
      // if ((this.chartData || {}).pm10Data) {
      //   console.log("t");
      // }
      this.loading = false;
    }
  },
  methods: {
    GenerateAnnotation(value, text, color) {
      return {
        type: "line",
        mode: "horizontal",
        scaleID: "y-axis-0",
        value: value,
        borderColor: color,
        borderWidth: 1,
        label: {
          content: text,
          enabled: true,
          position: "left",
          backgroundColor: "rgba(0,0,0,0.3)"
        }
      };
    },
    GenerateDataset(values, label, color, borderColor) {
      console.log(values, label, color, borderColor);
      return {
        backgroundColor: "purple",
        borderColor: "red",
        label: "PM₂₅ (µg/m³)",
        data: values,
        borderWidth: 1
      };
    },
    MapToChartTimeSeries(data) {
      console.log(data);
      var series = [];
      return series;
    }
  }
};
</script>
