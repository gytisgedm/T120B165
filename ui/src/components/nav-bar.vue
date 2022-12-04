<template>
    <v-navigation-drawer
      :mini-variant="miniVariant"
      fixed
      app
      permanent
      :color="'green darken-3'"
      :dark="true"
    >
      <v-list>
        <v-list-item dense>
           <v-list-item-action>
              <v-icon @click.stop="miniVariant = !miniVariant">mdi-menu</v-icon>
          </v-list-item-action>
        </v-list-item>
        <v-list-item class="px-2">
          <v-list-item-avatar>
            <v-avatar color="blue">
              <span class="white--text headline">
                {{ firstUsernameLetter }}
              </span>
            </v-avatar>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title class="title">
              {{ username }}
            </v-list-item-title>
          </v-list-item-content>
          </v-list-item>
      </v-list>
      <v-divider />
      <template>
        <v-list-item
            v-for="(item, i) in menuItemsByRole"
            :key="i"
            :to="item.to"
            router
          >
          <template>
            <v-icon>{{item.icon}}</v-icon>
          </template>
          <v-list-item-content>
              <v-list-item-title
                v-text="item.name"
              />
            </v-list-item-content>
        </v-list-item>
      </template>

      <v-list-item router exact @click="logout">
        <v-list-item-action>
          <v-icon>mdi-power</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>Atsijungti</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-navigation-drawer>
</template>

<script>
import auth from '../auth'

  export default{
    data: () => ({
      isFAManager: auth.isFAManager(),
      isEmployee: auth.isEmployee(),
      isAdmin: auth.isAdmin(),
      username: auth.getUsername(),
      firstUsernameLetter: auth.getUsername()[0],
      miniVariant: true
    }),

    computed:{
        items() {
          return [
                {
                  icon: 'mdi-home',
                  name: 'Pagrindinis puslapis',
                  to: '/dashboard',
                  admin: true,
                  otherUsers: true
                },
                {
                  icon: 'mdi-hand-coin-outline',
                  name: 'Ilgalaikis turtas',
                  to: '/fixed-assets',
                  admin: false,
                  otherUsers: true
                },
                {
                  icon: 'mdi-account',
                  name: 'Darbuotojai',
                  to: '/employees',
                  admin: true
                },
                {
                  icon: 'mdi-account-supervisor-circle',
                  name: 'Asmenys atsakingi už IT',
                  to: '/fa-managers',
                  admin: true
                },
            ]
        },
        menuItemsByRole() {
          if (auth.isAdmin())
            return this.items.filter((x) => x.admin === true)
          else
            return this.items.filter((x) => x.otherUsers === true)
        },
    },
    methods:{
      logout (){
        this.$router.push('/')
        auth.logout()
      }
    }
  }

</script>