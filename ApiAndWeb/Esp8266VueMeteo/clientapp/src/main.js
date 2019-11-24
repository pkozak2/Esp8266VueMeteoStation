import Vue from "vue";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import "roboto-fontface/css/roboto/roboto-fontface.css";
import "@fortawesome/fontawesome-free/css/all.css";

import router from "./router";
import store from "./store";

// Sync router with store
import { sync } from "vuex-router-sync";

// Sync store with router
sync(store, router);

Vue.config.productionTip = false;

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App)
}).$mount("#app");
