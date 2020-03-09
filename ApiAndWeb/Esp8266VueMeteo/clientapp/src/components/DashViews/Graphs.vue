<template>
  <div>Here will be graphs for {{ deviceName }} with {{ userName }}</div>
</template>
<script>
export default {
  name: "GraphsDashView",
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
