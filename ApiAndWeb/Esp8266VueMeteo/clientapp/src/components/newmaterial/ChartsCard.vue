<template>
  <div v-if="chartHasData">
    <OffsetCard v-bind="$attrs">
      <span v-if="dataLoading">
        <v-progress-linear indeterminate color="secondary"></v-progress-linear>
      </span>
      <LineChart
        class="pa-5"
        v-bind="$attrs"
        :styles="chartStyles"
        :chartData="chartData"
        :options="options"
      />
    </OffsetCard>
  </div>
</template>
<script>
import LineChart from "@/components/charts/Chart.vue";
import OffsetCard from "./OffsetCard.vue";

export default {
  name: "ChartsCard",
  components: { OffsetCard, LineChart },
  props: {
    options: {
      type: Object,
      default: function() {
        return {};
      }
    },
    chartHeight: {
      type: [String, Number],
      default: 350
    },
    chartData: {
      type: Object,
      default: function() {
        return {};
      }
    },
    dataLoading: {
      type: Boolean
    }
  },
  computed: {
    chartStyles() {
      return {
        position: "relative",
        height: `${this.chartHeight}px`
      };
    },
    chartHasData() {
      return this.dataLoading || !!(this.chartData || {}).datasets[0];
    }
  }
};
</script>
