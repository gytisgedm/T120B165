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
    "badUsernameOrPassword": "Blogas prisijungimo vardas arba slaptažodis",
    "close": "Uždaryti",
    "mustChangePassword": "Reikia pasikeisti slaptažodį",
    "unknownError": "Nežinoma klaida",
    "serverError": "Autentikacijos serverio klaida arba serveris nepasiekiamas",
    "accountDisabled": "Paskyra užblokuota",
    "passwordPolicyError": "Naujas slaptažodis neatitinka saugumo reikalavimų",
    "newPasswordsNotSame": "Nauji slaptažodžiai turi sutapti",
    "newPasswordEmpty": "Įveskite naują slaptažodį",
    "newPasswordTooShort": "Naujas slaptažodis turi būti bent 10 simbolių",
    "passwordChangedSuccessfully": "Slaptažodis pakeistas sėkmingai",
    "usernameEmpty": "Įveskite prisijungimo vardą",
    "passwordEmpty": "Įveskite slaptažodį",
    "resetTokenNotFound": "Slaptažodžio keitimo nuoroda neteisinga, nebegalioja arba yra skirta kitam prisijungimo vardui",
    "resetTokenExpired": "Slaptažodžio keitimo nuoroda nebegalioja",
    "captchaVerifyError": "Prašome pažymėti varnelę prie 'Aš ne robotas'",
    "recaptchaExpired": "Prašome iš naujo uždėti varnelę prie 'Aš ne robotas'",
    "accountLockedOut": "Jūsų paskyra yra laikinai blokuojama. Galimai dėl per didelio neteisingų prisijungimų kiekio. Prašome informuoti IT skyrių."
  },
  "ru": {
    "badUsernameOrPassword": "Не правельный логин или пароль",
    "close": "Закрыть",
    "mustChangePassword": "Нужно поменять пароль",
    "unknownError": "Ошибка сервера",
    "serverError": "Ошибка сервера",
    "accountDisabled": "Доступ запретен",
    "passwordPolicyError": "Новый пароль не соотвествует правилом",
    "cancel": "Отменить",
    "newPasswordsNotSame": "Новые пароли дольжны быть одинаковами",
    "newPasswordEmpty": "Введите новый пароль",
    "newPasswordTooShort": "Новый пароль должен быть не менше 10 символов",
    "passwordChangedSuccessfully": "Пароль поменен успешно.",
    "usernameEmpty": "Введите имя присоединения",
    "passwordEmpty": "Введите пароль",
    "resetTokenNotFound": "Ссылка для сброса пароля неправельная, устарела или предназначена для другого входа",
    "resetTokenExpired": "Ссылка для сброса пароля больше не действительна",
    "captchaVerifyError": "Пожалуйста, отметте 'Я не робот'",
    "recaptchaExpired": "Пожалуйста, повторно отметте 'Я не робот'",
    "accountLockedOut": "Ваш доступ времмено заблокирован. Вероятно из за повторных неправелных попытков. Просим связатся с IT отделом."
  }
}
</i18n>