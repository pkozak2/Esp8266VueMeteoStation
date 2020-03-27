<template>
  <v-app-bar
    :clipped-left="$vuetify.breakpoint.lgAndUp"
    app
    color="blue darken-3"
    dark
  >
    <!-- <v-app-bar-nav-icon @click.stop="$emit('toogle-drawer')" /> -->
    <!-- <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
      <span class="hidden-sm-and-down">Meteo App</span>
    </!-->
    <v-spacer />
    <v-btn
      text
      dark
      exact
      v-if="deviceNormalizedName"
      :to="{
        name: 'UserDeviceDash',
        params: { userName: userName, deviceName: deviceNormalizedName }
      }"
    >
      <v-icon class="hidden-md-and-up">fa-home</v-icon>
      <span class="hidden-sm-and-down">Strona główna</span>
    </v-btn>
    <v-btn
      text
      dark
      exact
      v-if="deviceNormalizedName"
      :to="{
        name: 'UserDeviceGraphs',
        params: {
          userName: userName,
          deviceNormalizedName: deviceNormalizedName
        }
      }"
    >
      <v-icon class="hidden-md-and-up">fa-chart-area</v-icon>
      <span class="hidden-sm-and-down">Wykresy</span>
    </v-btn>
    <v-btn
      text
      dark
      exact
      v-if="deviceNormalizedName"
      :to="{
        name: 'UserDeviceAnnualGraphs',
        params: {
          userName: userName,
          deviceNormalizedName: deviceNormalizedName
        }
      }"
    >
      <v-icon class="hidden-md-and-up">fa-chart-bar</v-icon>
      <span class="hidden-sm-and-down">Statystyki roczne</span>
    </v-btn>
    <v-menu offset-y v-if="userDevices[1]">
      <template v-slot:activator="{ on }">
        <v-btn text dark v-on="on">
          <v-icon class="hidden-md-and-up">fa-globe-europe</v-icon>
          <span class="hidden-sm-and-down">
            Lokalizacje
            <v-icon class="pl-2">fa-chevron-down</v-icon>
          </span>
        </v-btn>
      </template>
      <v-list>
        <v-list-item
          v-for="(item, id) in userDevices"
          :key="id"
          :to="{
            name: 'UserDeviceDash',
            params: { userName: userName, deviceNormalizedName: item.id }
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
    </v-btn>-->
  </v-app-bar>
</template>
<script>
import UserService from "@/services/UserService";
export default {
  name: "Toolbar",
  props: {
    userName: {
      type: String
    },
    deviceNormalizedName: {
      type: String,
      default: ""
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
        var defaultDevice = this.userDevices.filter(item => {
          return item.isDefault;
        });
        if (defaultDevice[0]) {
          this.$router
            .push({
              name: "UserDeviceDash",
              params: { deviceName: defaultDevice[0].id }
            })
            .catch(() => {});
        } else {
          this.$router
            .push({
              name: "UserDashAll"
            })
            .catch(() => {});
        }
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
