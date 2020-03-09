<template>
  <v-container>
    UName: {{ userName }}
    <sensor-summary></sensor-summary>
  </v-container>
</template>
<script>
import SensorSummary from "../../components/sensors/SensorSummary.vue";
export default {
  name: "newDashboard",
  components: { SensorSummary },
  props: {
    userName: { type: String }
  },
  created() {
    if (this.userName == "dashboard") {
      this.$router.push({ path: "/pkozak/smogowy" });
    }
  }
};
</script>
