<template>
  <v-snackbar v-model="show" top :color="error ? 'red' : 'green'" timeout="30000">
    <div v-html="$t(messageType)" />
    {{ details }}
    <template v-slot:action="{ attrs }">
      <v-btn text v-bind="attrs" @click="show = false">
        {{ $t('close') }}
      </v-btn>
    </template>
  </v-snackbar>
</template>

<script>
import { eventBus } from "../main"


export const MessageTypes = {
  BadUsernameOrPassword: 'badUsernameOrPassword',
  MustChangePassword: 'mustChangePassword',
  UnknownError: 'unknownError',
  ServerError: 'serverError',
  AccountDisabled: 'accountDisabled',
  PasswordPolicyError: 'passwordPolicyError',
  NewPasswordsNotSame: 'newPasswordsNotSame',
  NewPasswordEmpty: 'newPasswordEmpty',
  NewPasswordTooShort: 'newPasswordTooShort',
  PasswordChangedSuccessfully: 'passwordChangedSuccessfully',
  UsernameEmpty: 'usernameEmpty',
  PasswordEmpty: 'passwordEmpty',
  ResetTokenNotFound: 'resetTokenNotFound',
  ResetTokenExpired: 'resetTokenExpired',
  CaptchaVerifyError: 'captchaVerifyError',
  RecaptchaExpired: 'recaptchaExpired',
  AccountLockedOut: 'accountLockedOut',
}

export default {
  data: () => ({
    show: false,
    messageType: null,
    details: null,
    error: false,
  }),
  created() {
    eventBus.$on('show-error', (type, details) => {
      this.error = true
      this.messageType = type
      this.details = details
      this.show = true
    })
    eventBus.$on('show-success', (type) => {
      this.error = false
      this.messageType = type
      this.show = true
    })
  }

};
</script>

<i18n>
{
  "en": {
    "badUsernameOrPassword": "Bad username or password",
    "close": "Close",
    "mustChangePassword": "Password expired",
    "unknownError": "Unknown error",
    "serverError": "Authentication server error",
    "accountDisabled": "Account disabled",
    "passwordPolicyError": "New password does not meet password policy requirements",
    "newPasswordsNotSame": "New password does not match",
    "newPasswordEmpty": "Enter new password",
    "newPasswordTooShort": "New password's length must be at least 10 symbols",
    "passwordChangedSuccessfully": "Password changed successfully",
    "usernameEmpty": "Please enter username",
    "passwordEmpty": "Please enter password",
    "resetTokenNotFound": "Password reset link is incorrect, expired or generated for another user",
    "resetTokenExpired": "Password reset link is expired",
    "captchaVerifyError": "Please check that you are not a robot",
    "recaptchaExpired": "Please check that you are not a robot again",
    "accountLockedOut": "Your account has been locked out. Possibly after too much incorrect login attempts. Please inform IT department."
  },
  "lt": {
    "badUsernameOrPassword": "Blogas prisijungimo vardas arba slapta??odis",
    "close": "U??daryti",
    "mustChangePassword": "Reikia pasikeisti slapta??od??",
    "unknownError": "Ne??inoma klaida",
    "serverError": "Autentikacijos serverio klaida arba serveris nepasiekiamas",
    "accountDisabled": "Paskyra u??blokuota",
    "passwordPolicyError": "Naujas slapta??odis neatitinka saugumo reikalavim??",
    "newPasswordsNotSame": "Nauji slapta??od??iai turi sutapti",
    "newPasswordEmpty": "??veskite nauj?? slapta??od??",
    "newPasswordTooShort": "Naujas slapta??odis turi b??ti bent 10 simboli??",
    "passwordChangedSuccessfully": "Slapta??odis pakeistas s??kmingai",
    "usernameEmpty": "??veskite prisijungimo vard??",
    "passwordEmpty": "??veskite slapta??od??",
    "resetTokenNotFound": "Slapta??od??io keitimo nuoroda neteisinga, nebegalioja arba yra skirta kitam prisijungimo vardui",
    "resetTokenExpired": "Slapta??od??io keitimo nuoroda nebegalioja",
    "captchaVerifyError": "Pra??ome pa??ym??ti varnel?? prie 'A?? ne robotas'",
    "recaptchaExpired": "Pra??ome i?? naujo u??d??ti varnel?? prie 'A?? ne robotas'",
    "accountLockedOut": "J??s?? paskyra yra laikinai blokuojama. Galimai d??l per didelio neteising?? prisijungim?? kiekio. Pra??ome informuoti IT skyri??."
  },
  "ru": {
    "badUsernameOrPassword": "???? ???????????????????? ?????????? ?????? ????????????",
    "close": "??????????????",
    "mustChangePassword": "?????????? ???????????????? ????????????",
    "unknownError": "???????????? ??????????????",
    "serverError": "???????????? ??????????????",
    "accountDisabled": "???????????? ????????????????",
    "passwordPolicyError": "?????????? ???????????? ???? ???????????????????????? ????????????????",
    "cancel": "????????????????",
    "newPasswordsNotSame": "?????????? ???????????? ?????????????? ???????? ??????????????????????",
    "newPasswordEmpty": "?????????????? ?????????? ????????????",
    "newPasswordTooShort": "?????????? ???????????? ???????????? ???????? ???? ?????????? 10 ????????????????",
    "passwordChangedSuccessfully": "???????????? ?????????????? ??????????????.",
    "usernameEmpty": "?????????????? ?????? ??????????????????????????",
    "passwordEmpty": "?????????????? ????????????",
    "resetTokenNotFound": "???????????? ?????? ???????????? ???????????? ????????????????????????, ???????????????? ?????? ?????????????????????????? ?????? ?????????????? ??????????",
    "resetTokenExpired": "???????????? ?????? ???????????? ???????????? ???????????? ???? ??????????????????????????",
    "captchaVerifyError": "????????????????????, ?????????????? '?? ???? ??????????'",
    "recaptchaExpired": "????????????????????, ???????????????? ?????????????? '?? ???? ??????????'",
    "accountLockedOut": "?????? ???????????? ???????????????? ????????????????????????. ???????????????? ???? ???? ?????????????????? ?????????????????????? ????????????????. ???????????? ???????????????? ?? IT ??????????????."
  }
}
</i18n>