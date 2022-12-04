<template>
    <div>
        <app-bar>
            <v-toolbar-title>Asmenys atsakingi už ilgalaikį turtą</v-toolbar-title>
            <v-spacer/>
            <v-btn icon @click="(showAddForm = true)">
                <v-icon>mdi-plus</v-icon>
            </v-btn>
        </app-bar>
        <nav-bar/>
        <v-text-field
          v-model="search"
          label="Paieška (pagal vartotojo vardą)"
          clearable
          solo
          style="max-width: 400px"
          append-outer-icon="mdi-magnify"
          validate-on-blur
          @click:append-outer="getFAManager()"
        />   
        <v-data-table
            :headers="headers"
            :items="employees"
            :items-per-page="50"
            :loading="loading"
            class="elevation-1"
        >
            <template #[`item.actions`]="{ item }">
                <v-btn icon @click="showUpdateForm = true; setManagerValues(item.username, item.faCategory)">
                    <v-icon>mdi-account-edit</v-icon>
                </v-btn>
                <v-btn icon @click="deleteFAManager(item.faCategory, item.username)">
                    <v-icon>mdi-close</v-icon>
                </v-btn>
            </template>
        </v-data-table>
        <FAManagerFormAdd
            :show=showAddForm
            @close="showAddForm = false"
            @add="addFAManager($event)"
        />
        <FAManagerFormUpdate
            :username="username"
            :category="category"
            :oldCategory="category"
            :show=showUpdateForm
            @close="showUpdateForm = false"
            @update="updateFAManager($event)"
        />
    </div>
  </template>
  
  <script>
  
  import auth from '../auth'
  import AppBar from '../components/app-bar'
  import NavBar from '../components/nav-bar'
  import FAManagerFormAdd from '../components/FAManagerFormAdd'
  import FAManagerFormUpdate from '../components/FAManagerFormUpdate'
  
  export default {
    components: { AppBar, NavBar, FAManagerFormAdd, FAManagerFormUpdate },
    data: () => ({
      isAdmin: auth.isAdmin(),
      username: auth.getUsername(),
      employees: [],
      loading: false,
      showAddForm: false,
      showUpdateForm: false,
      search: null,
      username: null,
      category: null,
      headers: [
         { text: 'Kategorija', value: 'faCategory' },
         { text: 'Priskirta', value: 'username' },
         { text: 'Veiksmai', value: 'actions'},
      ],
    }),
    async created() {
      if (this.isAdmin){
        this.getFAManagers()
      }
    },
    methods: {
        async getFAManagers(){
        this.loading = true
        this.employees = (await this.$axios.get('/managers')).data
        this.loading = false
      },
      async deleteFAManager(category, username){
        if (confirm("Ar tikrai norite ištrinti?")) {
            this.loading = true
            await this.$axios.delete('/manager/remove', {data: {faCategory: category, username: username}})
            this.employees = (await this.$axios.get('/managers')).data
            this.loading = false
        }
      },
      async getFAManager(){
        this.loading = true
        this.employees = (await this.$axios.get('/manager/' + this.search)).data
        this.loading = false
      },
      async addFAManager(data){      
            this.loading = true
            await this.$axios.post('/manager/add', data)
            this.getFAManagers()
            this.loading = false
        },   
    async updateFAManager(data){      
            this.loading = true
            await this.$axios.put('fixed-asset/manager/update', data)
            this.getFAManagers()
            this.loading = false
        },   
        setManagerValues(incomingUsername, incomingCategory){
            this.username = incomingUsername
            this.category = incomingCategory
        }
    },
  }
  </script>
  