<template>
  <v-text-field
    ref="passwordField"
    :value="value"
    :label="label"
    prepend-icon="mdi-lock"
    @keyup.enter="$emit('submit')"
    :readonly="readonly"
    :type="visiblePassword ? 'text' : 'password'"
    :append-icon="visiblePassword ? 'mdi-eye-off' : 'mdi-eye'"
    @click:append="visiblePassword = !visiblePassword"
    @input="$emit('input', $event)"
  />
</template>

<script>
export default {
  props: {
    value: String,
    readonly: Boolean,
    label: String,
  },
  data: () => ({
    visiblePassword: false,
  }),
  methods: {
    focus() {
      this.$refs.passwordField.focus()
    }
  },
};
</script>

<style scoped>

</style>