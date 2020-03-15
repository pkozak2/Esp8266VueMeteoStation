<template>
  <div v-if="chartHasData">
    <OffsetCard v-bind="$attrs">
      <LineChart
        v-if="!dataLoading"
        class="pa-5"
        v-bind="$attrs"
        :styles="chartStyles"
        :chartData="chartData"
        :options="optionsLocal"
      />
      <span v-if="dataLoading">
        <v-progress-linear indeterminate height="15" color="secondary"></v-progress-linear>
      </span>
    </OffsetCard>
    {{optionsLocal}}
  </div>
</template>
<script>
import LineChart from "@/components/charts/Chart.js";
import OffsetCard from "./OffsetCard.vue";
const defaultChartOptions = {
  spanGaps: false,
  aspectRatio: 2,
  responsive: true,
  maintainAspectRatio: false,
  tooltips: {
    mode: "index",
    intersect: false
  },
  legend: {
    labels: {
      fontColor: "white"
    }
  },
  scales: {
    xAxes: [
      {
        display: true,
        type: "time",
        time: {
          displayFormats: {
            millisecond: "HH:mm:ss.SSS",
            second: "HH:mm:ss",
            minute: "HH:mm",
            hour: "HH"
          },
          tooltipFormat: "DD-MM HH:mm"
        },
        ticks: { fontColor: "white" }
      }
    ],
    yAxes: [
      {
        ticks: { fontColor: "white" }
      }
    ]
  },
  elements: {
    point: {
      radius: 0
    }
  },
  scaleLabel: {
    display: false
  }
};
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
      type: Array
    },
    dataLoading: {
      type: Boolean
    }
  },
  computed: {
    optionsLocal() {
      return {
        ...this.options,
        ...defaultChartOptions
      };
    },
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
