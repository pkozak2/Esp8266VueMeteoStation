import Vue from "vue";
import Vuetify from "vuetify/lib";
import pl from "vuetify/es5/locale/pl";

Vue.use(Vuetify);

const CUSTOM_ICONS = {
  clear: "fas fa-times"
};

export default new Vuetify({
  theme: {
    options: {
      customProperties: true
    },
    themes: {
      light: {
        primary: "#3f51b5",
        secondary: "#ff9800",
        accent: "#607d8b",
        error: "#f44336",
        warning: "#ffc107",
        info: "#00bcd4",
        success: "#4caf50"
      }
    }
  },
  lang: {
    locales: { pl },
    current: "pl"
  },
  icons: {
    iconfont: "fa",
    values: CUSTOM_ICONS
  }
});
