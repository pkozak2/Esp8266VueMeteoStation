<template>
  <BaseCard v-bind="$attrs" class="v-card--stats" v-on="$listeners">
    <v-card slot="offset" :class="`elevation-${elevation}`" :color="color" class="pa-4" dark>
      <v-icon size="40">{{ icon }}</v-icon>
    </v-card>
    <div class="text-right" pt-4>
      <p class="category font-weight-light" v-text="title" />
      <h3 class="title display-1 font-weight-light">
        {{ value }}
        <small v-html="smallValue"></small>
      </h3>
    </div>
    <template slot="actions">
      <v-icon :color="subIconColor" size="20" class="mr-2">{{ subIcon }}</v-icon>
      <span :class="subTextColor" class="caption font-weight-light" v-text="subText" />
    </template>
  </BaseCard>
</template>
<script>
import BaseCard from "./BaseCard";
export default {
  components: { BaseCard },
  name: "StatsCard",
  props: {
    ...BaseCard.props,
    icon: {
      type: String,
      required: true
    },
    subIcon: {
      type: String,
      default: undefined
    },
    subIconColor: {
      type: String,
      default: undefined
    },
    subTextColor: {
      type: String,
      default: undefined
    },
    subText: {
      type: String,
      default: undefined
    },
    title: {
      type: String,
      default: undefined
    },
    value: {
      type: [String, Number],
      default: undefined
    },
    smallValue: {
      type: String,
      default: undefined
    }
  }
};
</script>
<style lang="scss">
.v-card--stats {
  display: flex;
  flex-wrap: wrap;
  position: relative;

  .v-offset {
    display: inline-block;
    flex: 0 1;
    margin-top: 0;
    margin-left: 0;
    margin-right: auto;
    margin-bottom: 0 !important;
    max-width: auto;
    padding: 0 16px 0;
  }

  .v-card {
    border-radius: 4px;
    flex: 0 1 auto;
  }

  .v-card__text {
    display: inline-block;
    position: absolute;
    top: 0;
    right: 0;
    width: 100%;
  }

  .category {
    margin: 0;
  }

  .v-card__actions {
    flex: 1 0 100%;
  }
}
</style>
