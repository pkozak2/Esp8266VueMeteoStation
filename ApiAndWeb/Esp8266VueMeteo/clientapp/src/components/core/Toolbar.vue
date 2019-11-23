<template>
  <v-app-bar app>
    <v-toolbar-title>
      <v-btn v-if="responsive" class="default v-btn--simple" icon @click.stop="onClickBtn">
        <v-icon>fa-bars</v-icon>
      </v-btn>
      {{ title }}
    </v-toolbar-title>
    <v-spacer />
    <v-toolbar-items>
      <v-flex align-center layout py-2>
        <v-text-field
          v-if="responsiveInput"
          class="mr-4 mt-2 purple-input"
          label="Search..."
          hide-details
          color="purple"
        />
        <v-btn icon to="/">
          <v-icon>fa-home</v-icon>
        </v-btn>
        <v-menu left bottom open-on-hover>
          <template v-slot:activator="{ on }">
            <v-badge color="error" overlap>
              <template slot="badge">{{ notifications.length }}</template>
              <v-btn icon v-on="on" to="/dashboard/notifications">
                <v-icon>fa-bell</v-icon>
              </v-btn>
            </v-badge>
          </template>

          <v-list dense>
            <v-list-item v-for="notification in notifications" :key="notification" @click="onClick">
              <v-list-item-title>{{ notification }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <v-btn icon to="/dashboard/user-profile">
          <v-icon>fa-address-card</v-icon>
        </v-btn>

        <v-btn icon @click="logout">
          <v-icon>fa-door-open</v-icon>
        </v-btn>
      </v-flex>
    </v-toolbar-items>
  </v-app-bar>
</template>

<script>
import { mapMutations, mapGetters } from "vuex";

export default {
  data: () => ({
    notifications: [
      "Mike, Thanos is coming",
      "5 new avengers joined the team",
      "You're now friends with Capt",
      "Another Notification",
      "Another One - Dj Khalid voice"
    ],
    title: "I got a digital dash -Future Hendrixx",
    responsive: false,
    responsiveInput: false
  }),

  computed: {
    ...mapGetters(["authorized"])
  },

  watch: {
    $route(val) {
      this.title = val.meta.name;
    }
  },

  mounted() {
    this.onResponsiveInverted();
    window.addEventListener("resize", this.onResponsiveInverted);
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.onResponsiveInverted);
  },

  methods: {
    ...mapMutations("app", ["setDrawer", "toggleDrawer"]),
    onClickBtn() {
      this.setDrawer(!this.$store.state.app.drawer);
    },
    onClick() {
      //
    },
    onResponsiveInverted() {
      if (window.innerWidth < 991) {
        this.responsive = true;
        this.responsiveInput = false;
      } else {
        this.responsive = false;
        this.responsiveInput = true;
      }
    },
    logout: function() {
      this.$store.dispatch("logout").then(() => {
        this.$router.push("");
      });
    }
  }
};
</script>

<style>
#core-toolbar a {
  text-decoration: none;
}
</style>
