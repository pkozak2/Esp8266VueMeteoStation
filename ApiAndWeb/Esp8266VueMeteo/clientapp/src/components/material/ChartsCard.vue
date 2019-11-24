<template>
  <BaseCard v-bind="$attrs" class="v-card--material-chart" v-on="$listeners">
    <v-card slot="header" :class="`elevation-${elevation}`" :color="color" class="pa-4" dark>
      <v-icon size="40">fa-cat</v-icon>
    </v-card>
    <!-- <chartist
      slot="header"
      :data="data"
      :event-handlers="eventHandlers"
      :options="options"
      :ratio="ratio"
      :responsive-options="responsiveOptions"
      :type="type"
    >
    </chartist>-->

    <slot></slot>

    <slot slot="actions" name="actions"></slot>
  </BaseCard>
</template>
<script>
import BaseCard from "./BaseCard";
export default {
  components: { BaseCard },
  name: "ChartCard",
  inheritAttrs: false,

  props: {
    data: {
      type: Object,
      default: () => ({})
    },
    eventHandlers: {
      type: Array,
      default: () => []
    },
    options: {
      type: Object,
      default: () => ({})
    },
    ratio: {
      type: String,
      default: "ct-golden-section"
    },
    responsiveOptions: {
      type: Array,
      default: () => []
    },
    type: {
      type: String,
      required: true,
      validator: v => ["Bar", "Line", "Pie"].includes(v)
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