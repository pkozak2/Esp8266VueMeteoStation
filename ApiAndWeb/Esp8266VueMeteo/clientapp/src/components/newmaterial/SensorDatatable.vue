<template>
  <div>
    <v-col v-if="items[0] || loading">
      <v-col cols="12" class="text-center">
        Poziom zanieczyszczenia:
        <v-chip
          large
          :color="getColor(maxPollutionLevel)"
          :dark="maxPollutionLevel === 0 || maxPollutionLevel === 4"
        >
          <span v-if="!loading">{{ getIndexName(maxPollutionLevel) }}</span>
          <v-progress-circular v-if="loading" color="primary" indeterminate></v-progress-circular>
        </v-chip>
      </v-col>
    </v-col>
    <v-card class="mt-4 mx-auto" v-if="items[0] || loading">
      <v-data-table
        hide-default-footer
        :headers="headers"
        :items="items"
        disable-filtering
        :loading="loading"
      >
        <template v-slot:item.name="{ item }">
          {{ item.name }}
          <sub>{{ item.subName }}</sub>
        </template>
        <template v-slot:item.value="{ item }">
          <v-chip :color="getColor(item.index)" :dark="item.index === 0 || item.index === 4">
            {{ item.value }}
            <small>
              &nbsp;µg/m
              <sup>3</sup>
            </small>
          </v-chip>
        </template>
        <template v-slot:item.percent="{ item }">{{ item.percent }}%</template>
        <template v-slot:item.index="{ item }">{{ getIndexName(item.index) }}</template>
      </v-data-table>
    </v-card>
  </div>
</template>
<script>
export default {
  name: "SensorDatatable",
  props: {
    elevation: {
      type: Number,
      default: 12
    },
    loading: {
      type: Boolean
    },
    items: {
      type: Array,
      default: function() {
        return [];
      }
    }
  },
  data() {
    return {
      headers: [
        {
          text: "Nazwa",
          align: "center",
          value: "name",
          sortable: false
        },
        {
          text: "Wartość",
          value: "value",
          align: "center",
          sortable: false
        },
        { text: "%", value: "percent", align: "center", sortable: false },
        { text: "Indeks", value: "index", align: "center", sortable: false }
      ]
    };
  },
  computed: {
    maxPollutionLevel() {
      if (!this.items[0]) return -1;
      return this.items.reduce((prev, current) =>
        prev.y > current.y ? prev : current
      ).index;
    }
  },
  methods: {
    getColor(index) {
      switch (index) {
        case 0:
          return "#57b108";
        case 1:
          return "#b0dd10";
        case 2:
          return "#ffd911";
        case 3:
          return "#e58100";
        case 4:
          return "#990000";
        default:
          "white";
      }
    },
    getIndexName(index) {
      switch (index) {
        case 0:
          return "Bardzo niski";
        case 1:
          return "Niski";
        case 2:
          return "Średni";
        case 3:
          return "Wysoki";
        case 4:
          return "Bardzo wysoki";
        default:
          "";
      }
    }
  }
};
</script>
