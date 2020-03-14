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
    <v-menu offset-y v-if="userDevices[1]">
      <template v-slot:activator="{ on }">
        <v-btn text dark v-on="on">
          Lokalizacje <v-icon class="pl-2">fa-chevron-down</v-icon>
        </v-btn>
      </template>
      <v-list>
        <v-list-item
          v-for="(item, id) in userDevices"
          :key="id"
          :to="{
            name: 'UserDeviceDash',
            params: { userName: userName, deviceName: item.id }
          }"
        >
          <v-list-item-title>{{ item.text }}</v-list-item-title>
        </v-list-item>
        <v-divider></v-divider>
        <v-list-item
          :to="{
            name: 'UserDashAll',
            params: { userName: userName }
          }"
        >
          <v-list-item-title>Wszystkie</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>
    <!-- <v-btn icon large>
      <v-avatar size="32px" item>
        <v-img
          src="https://cdn.vuetifyjs.com/images/logos/logo.svg"
          alt="Vuetify"
      /></v-avatar>
    </v-btn> -->
  </v-app-bar>
</template>
<script>
import UserService from "@/services/UserService";
export default {
  name: "Toolbar",
  props: {
    userName: {
      type: String
    }
  },
  data: () => {
    return {
      userDevices: []
    };
  },
  mounted() {
    this.GetUserDevices();
  },
  methods: {
    GetUserDevices() {
      UserService.GetUserDevices(this.userName).then(r => {
        this.userDevices = r.data;
        this.TryNavigate();
      });
    },
    TryNavigate() {
      if (
        this.$router.currentRoute.name !== "UserDeviceDash" &&
        this.$router.currentRoute.name !== "UserDashAll"
      ) {
        this.$router
          .push({
            name: "UserDeviceDash",
            params: { deviceName: this.userDevices[0].id }
          })
          .catch(() => {});
      }
    }
  }
};
</script>
<style scoped>
.v-toolbar.v-toolbar--collapsed {
  max-width: 55px;
}
</style>
