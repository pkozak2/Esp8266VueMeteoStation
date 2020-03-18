<template>
  <div>
    <charts-card
      v-bind="graphAttrs"
      :options="graphOptions"
      :chartData="dataSets"
    ></charts-card>
    datasets: {{ dataSets }}
    <pre>{{ chartData }}</pre>
  </div>
</template>
<script>
import moment from "moment";
import ChartsCard from "../../components/newmaterial/ChartsCard";
import graphMixin from "../../mixins/graphMixin";
export default {
  name: "SensorPollutionGraph",
  components: { ChartsCard },
  mixins: [graphMixin],
  props: {
    chartData: {
      type: Object
    }
  },
  data() {
    return {
      loading: true,
      dataSets: { datasets: [] }
    };
  },
  computed: {
    graphAttrs() {
      const { title, bottom, dataLoading } = this.$attrs;
      return { title, bottom, dataLoading };
    },
    graphOptions() {
      return this.generateChartOption("pm");
    }
  },
  watch: {
    chartData(val) {
      if (Object.keys(val).length === 0) return;
      debugger;
      this.fillData();
      // if (val.pm25Data[0]) {
      //   this.dataSets.datasets.push(
      //     this.GenerateDataset(val.pm25Data, "PM₂₅ (µg/m³)", "purple", "red")
      //   );
      // }
    }
  },
  methods: {
    fillData() {
      this.dataSets = {
        labels: [this.getRandomInt(), this.getRandomInt()],
        datasets: [
          {
            label: "Data One",
            backgroundColor: "#f87979",
            data: [this.getRandomInt(), this.getRandomInt()]
          },
          {
            label: "Data One",
            backgroundColor: "#f87979",
            data: [this.getRandomInt(), this.getRandomInt()]
          }
        ]
      };
    },
    getRandomInt() {
      return Math.floor(Math.random() * (50 - 5 + 1)) + 5;
    },
    GenerateDataset(values, label, color, borderColor) {
      console.log(values, label, color, borderColor);
      return {
        backgroundColor: "purple",
        borderColor: "red",
        label: "PM₂₅ (µg/m³)",
        data: [this.getRandomInt(), this.getRandomInt()], //this.MapToChartTimeSeries(values),
        borderWidth: 1
      };
    },
    MapToChartTimeSeries(data) {
      console.log(data);
      console.log("moment: ", moment());
      return { t: moment(), y: 10 };
      // return data.map(item => {
      //   return { t: moment(item.dateTime), y: item.value };
      // });
    }
  }
};
</script>
