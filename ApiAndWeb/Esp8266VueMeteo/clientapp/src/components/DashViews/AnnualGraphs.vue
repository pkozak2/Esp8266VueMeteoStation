<template>
  <div>Annulal graphs for: {{ deviceName }} and user: {{ userName }}</div>
</template>
<script>
export default {
  name: "AnnulalGraphsDashView",
  props: {
    userName: {
      type: String,
      required: true
    },
    deviceName: {
      type: String,
      required: true
    }
  }
};
</script>
