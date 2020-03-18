<template>
  <div v-if="chartHasData">
    <OffsetCard v-bind="$attrs">
      <LineChart
        v-if="!dataLoading"
        class="pa-5"
        v-bind="$attrs"
        :styles="chartStyles"
        :chartData="chartData"
        :options="options"
      />
      <span v-if="dataLoading">
        <v-progress-linear
          indeterminate
          height="15"
          color="secondary"
        ></v-progress-linear>
      </span>
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
      return true;
      // return (
      //   !!(this.chartData || {}).labels && !!(this.chartData || {}).datasets
      // );
    }
  }
};
</script>
