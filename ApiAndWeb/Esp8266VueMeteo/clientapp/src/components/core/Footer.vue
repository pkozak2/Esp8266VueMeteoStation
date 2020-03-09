<template>
  <v-footer color="primary" padless absolute app>
    <v-row justify="center" no-gutters>
      <div v-for="link in links" :key="link.link">
        <v-btn
          v-if="!external(link.link)"
          color="white"
          text
          rounded
          class="my-2"
          :to="link.link"
        >
          {{ link.name }}
        </v-btn>
        <v-btn
          v-if="external(link.link)"
          color="white"
          text
          rounded
          class="my-2"
          :href="link.link"
          target="_blank"
        >
          {{ link.name }}
        </v-btn>
      </div>
      <v-col class="primary lighten-1 py-4 text-center white--text" cols="12">
        &copy; {{ new Date().getFullYear() }}
      </v-col>
    </v-row>
  </v-footer>
</template>

<script>
export default {
  data: () => ({
    links: [
      { name: "Home", link: "/dashboard" },
      // {
      //   name: "Demo",
      //   Link: "https://github.com/pkozak2"
      // },
      { name: "GitHub", link: "https://github.com/pkozak2" }
    ]
  }),
  methods: {
    external(link) {
      if (!link) return false;
      return link.startsWith("http://") || link.startsWith("https://");
    }
  }
};
</script>

<style>
#core-footer {
  z-index: 0;
}
</style>
