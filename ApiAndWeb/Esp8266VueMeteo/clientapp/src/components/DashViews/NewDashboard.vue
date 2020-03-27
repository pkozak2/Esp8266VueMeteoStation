<template>
  <v-container>
    <sensor-summary
      :userName="userName"
      :deviceNormalizedName="deviceNormalizedName"
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
    deviceNormalizedName: { type: String }
  },
  data: () => {
    return {};
  }
};
</script>
