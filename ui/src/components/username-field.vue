<template>
  <v-text-field
    ref="usernameField"
    :value="value"
    :label="label"
    :readonly="readonly"
    name="login"
    prepend-icon="mdi-account"
    type="text"
    @input="$emit('input', $event)"
    @keyup.enter="$emit('submit')"
  />
</template>

<script>
export default {
  props: {
    label: {
      type: String,
      required: false,
      default: 'Login'
    },
    value: {
      type: String,
      required: false,
      default: null
    },
    readonly: {
      type: Boolean,
      required: false,
      default: false,
    }
  },
  methods: {
    focus() {
      this.$refs.usernameField.focus()
    }
  },
}
</script>

<style scoped>

</style>