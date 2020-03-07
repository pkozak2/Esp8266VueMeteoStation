<template>
  <v-card class="mt-4 mx-auto">
    <v-sheet
      :style="styles"
      class="mx-auto"
      :color="color"
      :elevation="elevation"
    >
      <slot></slot>
    </v-sheet>
    <v-card-text class="pt-0">
      <div class="title font-weight-light mb-2">{{ title }}</div>
      <div class="subheading font-weight-light grey--text">{{ subtitle }}</div>
      <v-divider class="my-2" v-if="showDivider"></v-divider>
      <v-icon class="mr-2" small>{{ icon }}</v-icon>
      <span class="caption grey--text font-weight-light">
        {{ description }}
      </span>
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
    fullWidth: {
      type: Boolean,
      default: false
    },
    title: {
      type: String
    },
    subtitle: {
      type: String
    },
    description: {
      type: String
    },
    icon: {
      type: String
    }
  },
  computed: {
    styles() {
      return {
        top: `-${this.offset}px`,
        position: "relative",
        "max-width": this.fullWidth ? "100%" : "calc(100% - 32px)",
        "min-height": `${this.offset * 2}px`
      };
    },
    showDivider() {
      return !!this.description || !!this.icon;
    }
  }
};
</script>
<style scoped>
.v-sheet--offset {
  top: -24px;
  position: relative;
}
</style>
