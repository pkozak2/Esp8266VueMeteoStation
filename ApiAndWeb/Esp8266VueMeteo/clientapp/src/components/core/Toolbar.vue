<template>
  <v-app-bar
    :clipped-left="$vuetify.breakpoint.lgAndUp"
    app
    color="blue darken-3"
    dark
  >
    <v-app-bar-nav-icon @click.stop="$emit('toogle-drawer')" />
    <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
      <span class="hidden-sm-and-down">Meteo App</span>
    </v-toolbar-title>
    <v-spacer />
    <v-btn icon large>
      <v-avatar size="32px" item>
        <v-img
          src="https://cdn.vuetifyjs.com/images/logos/logo.svg"
          alt="Vuetify"
      /></v-avatar>
    </v-btn>
  </v-app-bar>
</template>
<script>
export default {
  name: "Toolbar"
};
</script>
<style scoped>
.v-toolbar.v-toolbar--collapsed {
  max-width: 55px;
}
</style>
