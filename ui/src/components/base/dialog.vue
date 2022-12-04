<template>
    <v-dialog
      v-if="value"
      :value="value"
      persistent
      scrollable
      :width="width"
      @input="$emit('input', $event)"
    >
      <v-card>
        <toolbar>
          <v-toolbar-title>{{ title }}</v-toolbar-title>
          <v-spacer />
          <slot name="title" />
          <v-btn icon @click.stop="$emit('input', false)">
            <v-icon>mdi-close</v-icon>
          </v-btn>
          <template v-if="!!$slots.extension" #extension>
            <slot name="extension" />
          </template>
        </toolbar>
        <v-card-text>
          <slot />
          <v-overlay :value="saving || loading" absolute>
            <v-progress-circular indeterminate size="64" />
          </v-overlay>
        </v-card-text>
        <v-divider v-if="$slots['actions']" />
        <v-card-actions v-if="$slots['actions']">
          <v-spacer />
          <slot name="actions" />
        </v-card-actions>
      </v-card>
    </v-dialog>
  </template>
  
  <script>
  import Toolbar from '@/components/base/toolbar'
  export default {
    components: { Toolbar },
    props: {
      value: { type: Boolean, required: false, default: true },
      title: { type: String, required: true },
      width: { type: [Number, String], required: false, default: 700 },
      loading: { type: Boolean, required: false, default: false },
      saving: { type: Boolean, required: false, default: false },
    },
  }
  </script>
  