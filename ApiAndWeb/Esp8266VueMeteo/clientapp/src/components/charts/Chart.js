import { Line, mixins } from "vue-chartjs";
// eslint-disable-next-line
import chartjsPluginAnnotation from "chartjs-plugin-annotation";

const { reactiveProp } = mixins;

export default {
  name: "chart",
  extends: Line,
  mixins: reactiveProp,
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
  },
  watch: {
    options() {
      console.log(this.options);
      this.renderChart(this.chartData, this.options);
    }
  }
};
