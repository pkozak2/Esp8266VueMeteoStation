<template>
  <div>
    <charts-card v-bind="graphAttrs" :options="graphOptions"></charts-card>
    <pre>{{chartData}}</pre>
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
          suggestedMin: 0,
          suggestedMax: 75
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
    chartData() {
      this.graphOptions.annotation.annotations = [];
      if ((this.chartData || {}).pm25Limit) {
        this.GenerateAnnotation(
          (this.chartData || {}).pm25Limit,
          "PM₂₅ limit",
          "purple"
        );
      }
      if ((this.chartData || {}).pm10Limit) {
        this.GenerateAnnotation(
          (this.chartData || {}).pm10Limit,
          "PM₁₀ limit",
          "orange"
        );
      }
    }
  },
  methods: {
    GenerateAnnotation(value, text, color) {
      this.graphOptions.annotation.annotations.push({
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
      });
    }
  }
};
</script>