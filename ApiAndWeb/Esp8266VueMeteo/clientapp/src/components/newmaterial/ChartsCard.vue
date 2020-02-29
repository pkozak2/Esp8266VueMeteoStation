<template>
  <div v-if="chartHasData">
    <OffsetCard v-bind="$attrs">
      <LineChart :chartData="chartData" :options="options" />
    </OffsetCard>
  </div>
</template>
<script>
import LineChart from "@/components/charts/Chart.js";
import OffsetCard from "./OffsetCard.vue";
export default {
  name: "ChartsCard",
  components: { OffsetCard, LineChart },
  chartData: {
    type: Object,
    default: null
  },
  options: {
    type: Object,
    default: null
  },
  computed: {
    chartHasData() {
      return (
        !!(this.chartData || {}).labels && !!(this.chartData || {}).datasets
      );
    }
  }
};
</script>