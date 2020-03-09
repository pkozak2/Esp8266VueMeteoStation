<template>
  <v-app>
    <transition mode="out-in">
      <router-view />
    </transition>
  </v-app>
</template>
<script>
export default {
  name: "App",

  data: () => ({
    //
  })
};
</script>
