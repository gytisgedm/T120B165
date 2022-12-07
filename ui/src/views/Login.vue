<template>
  <v-card class="elevation-12">
    <form-title title="Ilgalaikio turto sistema" />
    <v-card-text>
      <v-form>
        <username-field ref="username" v-model="username" :label="$t('username')" @submit="$refs.password.focus()" />
        <password-field ref="password" v-model="password" :label="$t('password')" @submit="userLogin" />
      </v-form>

      <vue-recaptcha
        v-if="showRecaptcha"
        ref="recaptcha"
        :sitekey="captchaSiteKey"
        :language="$i18n.locale"
        loadRecaptchaScript
        @verify="onVerify"
        @expired="onExpire"
        @error="onError"
      />
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn name="submit" color="green" dark @click="userLogin">
        <v-icon v-if="!loading">mdi-lock-open</v-icon>
        <v-progress-circular v-else indeterminate tiny />
        {{ $t('login') }}
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import Cookies from 'js-cookie'
import FormTitle from "../components/form-title";
import VueRecaptcha from 'vue-recaptcha'
import PasswordField from "../components/password-field";
import UsernameField from "../components/username-field";
import { MessageTypes } from "../components/snackbar";
import { eventBus } from "../main";



export default {
  components: { UsernameField, PasswordField, FormTitle, VueRecaptcha },
  data() {
    return {
      username: null,
      password: null,

      returnUrl: null,
      ghost: null,

      loading: false,

      showRecaptcha: false,
      recaptchaToken: null
    }
  },
  computed: {
    recaptchaVerified(){
      return this.recaptchaToken !== null
    },
    captchaSiteKey(){
      return process.env.VUE_APP_RECAPTCHA_SITE_KEY
    }
  },
  created() {
    this.returnUrl = this.$route.query.returnUrl
    this.ghost = this.$route.query.ghost
    this.username = this.$route.query.username || null
  },
  mounted() {
    this.$refs.username.focus()
  },
  methods: {
    async userLogin() {
      if(this.username == null || this.username.length === 0) {
        this.showError(MessageTypes.UsernameEmpty)
      } else if (this.password == null || this.password.length === 0) {
        this.showError(MessageTypes.PasswordEmpty)
      } else if(this.showRecaptcha && !this.recaptchaVerified)
        this.showError(MessageTypes.CaptchaVerifyError)
      else {
        this.loading = true
        try {
          const { data } = await this.$axios.post('/auth/login/?username=' + this.username + '&password=' + this.password)
          Cookies.set('token', data)
          this.$router.push('/fixed-assets')
        }
        catch(e) {
          this.showError(MessageTypes.ServerError)
        }
        this.loading = false
      }
    },
    showError(text, errorCode, result) {
      let details = null
      if(errorCode) details = `: ${errorCode} ${result}`
      eventBus.$emit('show-error', text, details)
    },
    // recaptcha
    onError(){
      this.showError(MessageTypes.CaptchaVerifyError)
    },
    onExpire(){
      this.recaptchaToken = null
      if(this.showRecaptcha)
        this.$refs.recaptcha.reset()
      this.showError(MessageTypes.RecaptchaExpired)
    },
    onVerify(token){
      this.recaptchaToken = token
    }
  },
};
</script>