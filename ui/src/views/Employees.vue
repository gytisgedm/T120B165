<template>
    <div>
      <app-bar>
          <v-toolbar-title>Darbuotojai</v-toolbar-title>
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
          @click:append-outer="getEmployee()"
        />   
      <v-data-table
        :headers="headers"
        :items="employees"
        :items-per-page="50"
        :loading="loading"
        class="elevation-1"
      >
        <template #[`item.isAdmin`]="{ item }">
            {{ item.isAdmin? "Taip" : "Ne" }}
        </template>
        <template #[`item.actions`]="{ item }">
            <v-btn icon @click="changeAdminSettings(item.username)">
                <v-icon>mdi-security</v-icon>
            </v-btn>
            <v-btn icon @click="deleteEmployee(item.username)">
                <v-icon>mdi-close</v-icon>
            </v-btn>
        </template>
        </v-data-table>
        <EmployeeFromAdd
                :show=showAddForm
                @close="showAddForm = false"
                @add="addEmployee($event)"
        />
        <footer-bar/>
    </div>
  </template>
  
  <script>
  
  import auth from '../auth'
  import AppBar from '../components/app-bar'
  import NavBar from '../components/nav-bar'
  import EmployeeFromAdd from '../components/EmployeeFormAdd'
  import FooterBar from '../components/footer-bar'
  
  export default {
    components: { AppBar, NavBar, EmployeeFromAdd, FooterBar },
    data: () => ({
      isAdmin: auth.isAdmin(),
      username: auth.getUsername(),
      employees: [],
      loading: false,
      search: null,
      showAddForm: false,
      headers: [
         { text: 'Vartotojo vardas', value: 'username' },
         { text: 'Vardas', value: 'name' },
         { text: 'Pavardė', value: 'surname' },
         { text: 'Admin', value: 'isAdmin' },
         { text: 'Skyrius', value: 'department' },
         { text: 'Veiksmai', value: 'actions', width: 150},
      ],
    }),
    async created() {
      if (this.isAdmin){
        this.getEmployees()
        document.title = "Darbuotojai"
      }
    },
    methods: {
        async getEmployees(){
            this.loading = true
            this.employees = (await this.$axios.get('/employees/')).data
            this.loading = false
        },
        async deleteEmployee(username){
        if (confirm("Ar tikrai norite ištrinti?")) {
            this.loading = true
            await this.$axios.delete('/employee/' + username)
            this.employees = (await this.$axios.get('/employees')).data
            this.loading = false
        }
      },
      async changeAdminSettings(username){
        if (confirm("Ar tikrai norite pakeisti administratoriaus teises?")) {
            this.loading = true
            await this.$axios.put('/employee/', {username: username})
            this.employees = (await this.$axios.get('/employees')).data
            this.loading = false
        }
      },
      async getEmployee(){
        this.loading = true
        this.employees = (await this.$axios.get('/employee/' + this.search)).data
        this.loading = false
      },
        async addEmployee(data){
            this.loading = true
            await this.$axios.post('/auth/register', data)
            this.loading = false
            this.getEmployees()
        },   
    }    
  }
  </script>
  