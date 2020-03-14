<template>
  <v-container>
    <sensor-summary
      :userName="userName"
      :deviceNormalizedName="deviceName"
    ></sensor-summary>
  </v-container>
</template>
<script>
import SensorSummary from "@/components/sensors/SensorSummary.vue";
export default {
  name: "newDashboard",
  components: { SensorSummary },
  props: {
    userName: { type: String },
    deviceName: { type: String }
  },
  data: () => {
    return {};
  }
};
</script>
