<template>
  <v-content>
    <v-app id="dashboard-view" dark>
      <Toolbar @toogle-drawer="drawer = !drawer" />

      <Drawer v-model="drawer" />

      <v-fade-transition mode="out-in">
        <router-view />
      </v-fade-transition>

      <CFooter v-if="$route.name !== 'Maps'" />
    </v-app>
  </v-content>
</template>

<script>
import Toolbar from "@/components/core/Toolbar";
import Drawer from "@/components/core/Drawer";
import CFooter from "@/components/core/Footer";
export default {
  name: "DashboardView",
  components: { Toolbar, Drawer, CFooter },
  data: () => ({
    drawer: false
  }),
  metaInfo() {
    return {
      title: "Vuetify Admin Dashboard by ClintOxx"
    };
  }
};
</script>

<style></style>
