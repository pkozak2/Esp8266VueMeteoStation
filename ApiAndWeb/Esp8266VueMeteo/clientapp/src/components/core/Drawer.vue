<template>
  <v-navigation-drawer
    v-model="inputValue"
    app
    floating
    persistent
    mobile-break-point="991"
    width="260"
  >
    <v-img :src="image" height="100%">
      <v-layout row>
        <v-img :src="logo" height="64" contain />
      </v-layout>
      <v-divider />
      <v-text-field
        v-if="responsive"
        class="purple-input search-input"
        label="Search..."
        color="purple"
      />
      <v-layout fill-height fluid tag="v-list" column>
        <v-list dense v-for="(link, i) in links" :key="i" :name="link.to" :to="link.to">
          <v-list-item link>
            <v-list-item-action>
              <v-icon>{{link.icon}}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>{{link.text}}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-layout>
    </v-img>
  </v-navigation-drawer>
</template>

<script>
// Utilities
import { mapMutations, mapState } from "vuex";

export default {
  data: () => ({
    logo: require("@/assets/img/redditicon.png"),
    links: [
      {
        to: "/",
        icon: "mdi-view-dashboard",
        text: "Dashboard"
      },
      {
        to: "/dashboard/user-profile",
        icon: "fa-cat",
        text: "User Profile"
      },
      {
        to: "/dashboard/table-list",
        icon: "mdi-clipboard-outline",
        text: "Table List"
      },
      {
        to: "/dashboard/user-tables",
        icon: "mdi-table-edit",
        text: "Users Table"
      },
      {
        to: "/dashboard/typography",
        icon: "mdi-format-font",
        text: "Typography"
      },
      {
        to: "/dashboard/icons",
        icon: "mdi-chart-bubble",
        text: "Icons"
      },
      {
        to: "/dashboard/maps",
        icon: "mdi-map-marker",
        text: "Maps"
      },
      {
        to: "/dashboard/notifications",
        icon: "mdi-bell",
        text: "Notifications"
      }
    ],
    responsive: false
  }),
  computed: {
    ...mapState("app", ["image", "color"]),
    inputValue: {
      get() {
        return this.$store.state.app.drawer;
      },
      set(val) {
        this.setDrawer(val);
      }
    },
    items() {
      return this.$t("Layout.View.items");
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
    onResponsiveInverted() {
      if (window.innerWidth < 991) {
        this.responsive = true;
      } else {
        this.responsive = false;
      }
    }
  }
};
</script>

<style lang="scss">
#app-drawer {
  .v-list__tile {
    border-radius: 4px;

    &--buy {
      margin-top: auto;
      margin-bottom: 17px;
    }
  }

  .v-image__image--contain {
    top: 9px;
    height: 60%;
  }

  .search-input {
    margin-bottom: 30px !important;
    padding-left: 15px;
    padding-right: 15px;
  }
}
</style>
