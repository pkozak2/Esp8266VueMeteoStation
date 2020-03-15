<script>
import { Line, mixins } from "vue-chartjs";
// eslint-disable-next-line
import chartjsPluginAnnotation from "chartjs-plugin-annotation";

export default {
  extends: Line,
  mixins: [mixins.reactiveProp],
  props: {
    chartdata: {
      type: Object,
      default: null
    },
    options: {
      type: Object,
      default: null
    }
  },
  mounted() {
    this.renderChart(this.chartdata, this.options);
  }
};
</script>
