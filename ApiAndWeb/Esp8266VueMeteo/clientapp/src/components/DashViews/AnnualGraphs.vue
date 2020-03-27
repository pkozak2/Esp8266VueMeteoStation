<template>
  <div>
    Annulal graphs for: {{ deviceNormalizedName }} and user: {{ userName }}
  </div>
</template>
<script>
export default {
  name: "AnnulalGraphsDashView",
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
