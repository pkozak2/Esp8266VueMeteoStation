import { Line, mixins } from "vue-chartjs";

export default {
  name: "chart",
  extends: Line,
  mixins: [mixins.reactiveProp],
  props: {
    chartData: {
      type: Object,
      default: null
    },
    options: {
      type: Object,
      default: function() {
        return {
          responsive: true,
          maintainAspectRatio: false
        };
      }
    }
  },
  mounted() {
    this.renderChart(this.chartData, this.options);
  }
};
