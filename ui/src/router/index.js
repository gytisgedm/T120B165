import Vue from 'vue'
import VueRouter from 'vue-router'
import auth from '../auth'
import Login from '../views/Login.vue'
import Dashboard from '../views/Dashboard.vue'
import FixedAssets from '../views/FixedAssets.vue'
import Employees from '../views/Employees.vue'
import FAManagers from '../views/FAManagers.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  // {
  //   path: '/dashboard',
  //   name: 'DashBoard',
  //   component: Dashboard
  // },
  {
    path: '/fixed-assets',
    name: 'Fixed assets',
    component: FixedAssets
  },
  {
    path: '/employees',
    name: 'Employees',
    component: Employees,
  },
    {
    path: '/fa-managers',
    name: 'Fixed assets managers',
    component: FAManagers,
  },
]

// router.beforeEach((to, from) => {
//   if (!auth.isLoggedIn()){
//     router.push('/')
//   }
// })

const router = new VueRouter({
  routes
})

export default router
