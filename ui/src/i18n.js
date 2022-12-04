import Vue from 'vue'
import VueI18n from 'vue-i18n'

Vue.use(VueI18n)

export default new VueI18n({
  locale: process.env.VUE_APP_I18N_LOCALE || 'lt',
  fallbackLocale: process.env.VUE_APP_I18N_FALLBACK_LOCALE || 'lt',
  messages: {
    lt: require('./locales/lt.json'),
    en: require('./locales/en.json'),
    ru: require('./locales/ru.json'),
  },
  silentFallbackWarn: true,
})
