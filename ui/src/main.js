import Vue from 'vue'
import './plugins/axios'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import i18n from './i18n'
import router from './router'
import auth from './auth'

Vue.config.productionTip = false

export const eventBus = new Vue()

new Vue({
  vuetify,
  i18n,
  router,
  auth,
  render: h => h(App)
}).$mount('#app')