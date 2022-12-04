<template>
    <v-app-bar
      fixed
      app
      elevate-on-scroll
      :color="'green darken-3'"
      :dark ="true"
    >
      <v-tooltip v-if="withNavigateBack" bottom>
        <template #activator="{ attrs, on }">
          <v-btn icon v-bind="attrs" @click="$router.go(-1)" v-on="on">
            <v-icon>mdi-keyboard-backspace</v-icon>
          </v-btn>
        </template>
        <span>{{ $t('back') }}</span>
      </v-tooltip>
      <template v-if="loading">
        <v-overlay>
          <v-progress-circular indeterminate />
        </v-overlay>
      </template>
      <template v-else>
        <slot />
      </template>
      <template v-if="hasExtensionSlot" #extension>
        <slot name="extension" />
      </template>
    </v-app-bar>
  </template>
  
  <script>
  export default {
    props: {
      withNavigateBack: {
        type: Boolean,
        required: false,
        default: false,
      },
      loading: {
        type: Boolean,
        required: false,
        default: false,
      },
    },
    computed: {
      hasExtensionSlot() {
        return !!this.$slots.extension
      },
    },
  }
  </script>
  