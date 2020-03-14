<template>
  <v-container>
    <Toolbar @toogle-drawer="drawer = !drawer" :userName="userName" />

    <Drawer v-model="drawer" />
    <v-content>
      <v-fade-transition mode="out-in">
        <router-view />
      </v-fade-transition>
    </v-content>
    <CFooter v-if="$route.name !== 'Maps'" />
  </v-container>
</template>

<script>
import Toolbar from "@/components/core/Toolbar";
import Drawer from "@/components/core/Drawer";
import CFooter from "@/components/core/Footer";
export default {
  name: "DashboardView",
  components: { Toolbar, Drawer, CFooter },
  props: {
    userName: String
  },
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
