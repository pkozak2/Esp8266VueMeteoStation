<template>
  <div>
    Here will be graphs for {{ deviceNormalizedName }} with {{ userName }}
  </div>
</template>
<script>
export default {
  name: "GraphsDashView",
  props: {
    userName: {
      type: String,
      required: true
    },
    deviceNormalizedName: {
      type: String,
      required: true
    }
  }
};
</script>
