<template>
  <v-toolbar color="green" dark flat>
    <v-tooltip v-if="withNavigateBack" bottom>
      <template #activator="{ attrs, on }">
        <v-btn icon v-bind="attrs" @click="$router.go(-1)" v-on="on">
          <v-icon>mdi-keyboard-backspace</v-icon>
        </v-btn>
      </template>
      <span>{{ $t('back') }}</span>
    </v-tooltip>
    <v-toolbar-title>{{ title }}</v-toolbar-title>
    <v-spacer />
    <v-btn-toggle
      v-model="$i18n.locale"
      shaped
      mandatory
      light
      color="green"
    >
      <!-- <v-btn value="lt">
        LT
      </v-btn>

      <v-btn value="ru">
        RU
      </v-btn>

      <v-btn value="en">
        EN
      </v-btn> -->
    </v-btn-toggle>
  </v-toolbar>
</template>

<script>
export default {
  props: {
    withNavigateBack: {
      type: Boolean,
      required: false,
      default: false
    },
    title: {
      type: String,
      required: true,
    }
  }
}
</script>