<template>
  <v-container>
    <sensor-summary></sensor-summary>
  </v-container>
</template>
<script>
import SensorSummary from "../../components/sensors/SensorSummary.vue";
export default {
  name: "newDashboard",
  components: { SensorSummary }
};
</script>