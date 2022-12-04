<template>
    <v-toolbar
      :color="'green darken-3'"
      :dark="true"
      flat
    >
      <template v-if="!!$slots.extension" #extension>
        <slot name="extension" />
      </template>
      <slot />
    </v-toolbar>
  </template>
  
  <script>
  export default {}
  </script>
  