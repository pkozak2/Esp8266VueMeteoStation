<template>
  <v-container>
    <!-- <v-layout>
      <v-flex sm12 class="text-sm-center">test</v-flex>
    </v-layout>-->
    <v-layout>
      <v-flex sm6 xs12 md6 lg3 pa-2>
        <StatsCard
          color="green"
          icon="fa-cat"
          title="Revenue"
          value="$34,245"
          smallValue="test"
          sub-icon="fa-cat"
          sub-text="Last 24 Hours"
        />
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex sm6 xs12 md6 lg3 pa-2>
        <ChartsCard color="warning" type="Line">
          <h3 class="title font-weight-light">Completed Tasks</h3>
          <p class="category d-inline-flex font-weight-light">Last Last Campaign Performance</p>

          <template slot="actions">
            <v-icon class="mr-2" small>fa-cat</v-icon>
            <span class="caption grey--text font-weight-light">campaign sent 26 minutes ago</span>
          </template>
        </ChartsCard>
      </v-flex>
      <v-flex sm6 xs12 md6 lg3 pa-2>
        <ChartsCard color="warning" type="Line">
          <h3 class="title font-weight-light">Completed Tasks</h3>
          <p class="category d-inline-flex font-weight-light">Last Last Campaign Performance</p>

          <template slot="actions">
            <v-icon class="mr-2" small>fa-cat</v-icon>
            <span class="caption grey--text font-weight-light">campaign sent 26 minutes ago</span>
          </template>
        </ChartsCard>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import StatsCard from "@/components/material/StatsCard.vue";
import ChartsCard from "@/components/material/ChartsCard.vue";
export default {
  name: "",
  components: { StatsCard, ChartsCard },
  data() {
    return {};
  }
};
</script>