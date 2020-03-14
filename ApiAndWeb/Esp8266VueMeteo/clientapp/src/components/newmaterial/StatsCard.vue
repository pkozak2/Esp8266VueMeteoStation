<template>
  <v-card class="mt-4 mx-auto">
    <v-sheet :style="styles" class="ml-4" :color="color" :elevation="elevation">
      <v-icon :size="iconSize" class="pa-4" :color="iconColor">
        {{ icon }}
      </v-icon>
    </v-sheet>
    <v-card-text class="pt-2">
      <div class="values text-right pt-3 pr-3">
        <div class="font-weight-light mb-1">{{ title }}</div>
        <h3 class="title display-1 font-weight-light" v-if="value">
          {{ value }}
          <small v-html="smallValue"></small>
        </h3>
      </div>
      <div>
        <v-divider class="my-2" v-if="showDivider"></v-divider>
        <span class="caption grey--text font-weight-light" v-if="description">
          {{ description }}
        </span>
      </div>
    </v-card-text>
  </v-card>
</template>
<script>
export default {
  name: "OffsetCard",
  props: {
    color: {
      type: String,
      default: "accent"
    },
    elevation: {
      type: Number,
      default: 12
    },
    offset: {
      type: [Number, String],
      default: 24
    },
    maxWidth: {
      type: Number,
      default: 70
    },
    title: {
      type: String
    },
    value: {
      type: [String, Number]
    },
    smallValue: {
      type: [String, Number]
    },
    description: {
      type: String
    },
    icon: {
      type: String
    },
    iconSize: {
      type: [Number, String],
      default: 40
    },
    iconColor: {
      type: String,
      default: "white"
    }
  },
  computed: {
    styles() {
      return {
        top: `-${this.offset}px`,
        position: "relative",
        "max-width": `${this.maxWidth}px`,
        "min-height": `${this.maxWidth}px`
      };
    },
    showDivider() {
      return !!this.description;
    }
  }
};
</script>
<style scoped>
.v-card__text .values {
  display: inline-block;
  position: absolute;
  top: 0;
  right: 0;
  width: 100%;
}
</style>
