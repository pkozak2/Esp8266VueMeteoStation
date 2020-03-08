<template>
  <v-card class="mt-4 mx-auto">
    <v-sheet
      :style="styles"
      class="mx-auto"
      :color="color"
      :elevation="elevation"
      v-if="!bottom"
    >
      <slot></slot>
    </v-sheet>
    <v-card-text :class="bottom ? 'pt-5' : 'pt-0'">
      <div class="title font-weight-light mb-2 text-center">{{ title }}</div>
      <div class="subheading font-weight-light grey--text">{{ subtitle }}</div>
      <v-divider class="my-2" v-if="showDivider"></v-divider>
      <v-icon class="mr-2" v-if="icon" small>{{ icon }}</v-icon>
      <span class="caption grey--text font-weight-light" v-if="description">
        {{ description }}
      </span>
    </v-card-text>
    <v-sheet
      :style="styles"
      class="mx-auto"
      :color="color"
      :elevation="elevation"
      v-if="bottom"
    >
      <slot></slot>
    </v-sheet>
  </v-card>
</template>
<script>
export default {
  name: "OffsetCard",
  props: {
    bottom: {
      type: Boolean,
      default: false
    },
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
      var position = this.bottom
        ? { bottom: `-${this.offset}px` }
        : { top: `-${this.offset}px` };
      return Object.assign(
        {
          position: "relative",
          "max-width": this.fullWidth ? "100%" : "calc(100% - 32px)",
          "min-height": `${this.offset * 2}px`
        },
        position
      );
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
