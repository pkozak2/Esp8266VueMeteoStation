<template>
  <BaseCard
    v-bind="$attrs"
    class="v-card--material-chart"
    v-on="$listeners"
    v-if="chartHasData"
  >
    <v-card
      slot="offset"
      :class="`elevation-${elevation}`"
      :color="color"
      class="pa-4"
      dark
    >
      <LineChart :chartData="chartData" :options="options" v-if="chartData" />
    </v-card>
    <slot></slot>
    <slot slot="actions" name="actions"></slot>
  </BaseCard>
</template>
<script>
import LineChart from "@/components/charts/Chart.vue";
import BaseCard from "./BaseCard";
export default {
  components: { BaseCard, LineChart },
  name: "ChartCard",
  inheritAttrs: false,
  props: {
    ...BaseCard.props,
    chartData: {
      type: Object,
      default: null
    },
    options: {
      type: Object,
      default: null
    }
  },
  computed: {
    chartHasData() {
      return !!(this.chartData || {}).labels;
    }
  }
};
</script>

<style lang="scss">
.v-card--material-chart {
  .v-card--material__header {
    .ct-label {
      color: inherit;
      opacity: 0.7;
      font-size: 0.975rem;
      font-weight: 100;
    }

    .ct-grid {
      stroke: rgba(255, 255, 255, 0.2);
    }
    .ct-series-a .ct-point,
    .ct-series-a .ct-line,
    .ct-series-a .ct-bar,
    .ct-series-a .ct-slice-donut {
      stroke: rgba(255, 255, 255, 0.8);
    }
    .ct-series-a .ct-slice-pie,
    .ct-series-a .ct-area {
      fill: rgba(255, 255, 255, 0.4);
    }
  }
}
</style>
