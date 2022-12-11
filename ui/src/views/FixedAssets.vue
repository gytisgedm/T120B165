<template>
  <div>
    <app-bar>
        <v-toolbar-title>Ilgalaikis turtas</v-toolbar-title>
        <v-spacer/>
        <v-btn v-if="isFAManager" icon @click="(showAddFAForm = true)">
            <v-icon>mdi-plus</v-icon>
        </v-btn>
    </app-bar>
    <nav-bar/>
    <v-text-field v-if="isFAManager"
          v-model="search"
          label="Paieška (pagal kodą)"
          :rules="[rules.numeric]"
          counter="6"
          maxlength="6"
          clearable
          solo
          style="max-width: 400px"
          append-outer-icon="mdi-magnify"
          validate-on-blur
          @click:append-outer="getFixedAsset()"
    />   
    <v-data-table
      :headers="isFAManager? managerHeaders: employeeHeaders"
      :items="fixedAssets"
      :items-per-page="50"
      :loading="loading"
      :item-class="itemRowBackground"
      class="elevation-1"
    >
      <template #[`item.eventType`]="{ item }">
        {{ item.eventType? getTypeText(item.eventType): defaultTypeText }}
      </template>

      <template #[`item.actions`]="{ item }">
        <v-btn v-if="isFAManager" icon @click="(code = item.code, showEmployeeDialog = true)">
            <v-icon>mdi-account-plus</v-icon>
        </v-btn>
        <v-btn v-if="isFAManager" icon @click="storeAssignedFixedAsset(item.code)">
            <v-icon>mdi-warehouse</v-icon>
        </v-btn>
        <v-btn v-if="(isEmployeeOrAdmin && item.eventType === 1)" icon @click="acceptAssignedAsset(item.code)">
            <v-icon>mdi-check</v-icon>
        </v-btn>
        <v-btn v-if="(isEmployeeOrAdmin  && item.eventType === 1)" icon @click="rejectAssignedAsset(item.code)">
            <v-icon>mdi-close</v-icon>
        </v-btn>
        <v-btn v-if="isFAManager" icon @click="deleteFixedAsset(item.code)">
            <v-icon>mdi-delete</v-icon>
        </v-btn>
      </template>
    </v-data-table>

    <FixedAssetFormAdd
        :show=showAddFAForm
        @close="showAddFAForm = false"
        @add="addFixedAsset($event)"
    />
    <EmployeeDialog
        :show=showEmployeeDialog
        title="Priskriti ilgalaikį turtą darbuotojui"
        @close="showEmployeeDialog = false"
        @selected="(employee = $event, assignFixedAsset())"
    />
    <footer-bar/>
  </div>
</template>

<script>

import auth from '../auth'
import AppBar from '../components/app-bar'
import NavBar from '../components/nav-bar'
import FixedAssetFormAdd from '../components/FixedAssetFormAdd'
import EmployeeDialog from '../components/EmployeeDialog.vue'
import FooterBar from '../components/footer-bar'

export default {
  components: { AppBar, NavBar, FixedAssetFormAdd, EmployeeDialog, FooterBar },
  data: () => ({
    isFAManager: auth.isFAManager(),
    isEmployeeOrAdmin: auth.isEmployeeOrAdmin(),
    username: auth.getUsername(),
    fixedAssets: [],
    loading: false,
    search: null,
    showAddFAForm: false,
    showEmployeeDialog: false,
    code: null,
    employee: {},
    defaultTypeText: "Nepriskrita",
    managerHeaders: [
       { text: 'Kodas', value: 'code' },
       { text: 'Aprašymas', value: 'description' },
       { text: 'Serijinis numeris', value: 'serialNumber' },
       { text: 'Būsena', value: 'eventType' },
       { text: 'Priskirta', value: 'assignedTo' },
       { text: 'Veiksmai', value: 'actions' },
    ],
    employeeHeaders: [
       { text: 'Kodas', value: 'code' },
       { text: 'Aprašymas', value: 'description' },
       { text: 'Serijinis numeris', value: 'serialNumber' },
       { text: 'Būsena', value: 'eventType' },
       { text: 'Priskirė', value: 'assignedBy' },
       { text: 'Veiksmai', value: 'actions' },
    ],
    rules:{
          required: v => !!v || "Laukas privalomas",
          numeric: (v) => !isNaN(v) || "Galima vesti tik skaičius",
    },
  }),
  computed: {
    employeeUsername(){
        if(this.employee[0]) return this.employee[0].username
          return ''
    }
  },
  async created() {
    if (this.isEmployeeOrAdmin){
      this.getAssignedFixedAssets()
      document.title = "Ilgalaikis turtas"
    }
      
    if (this.isFAManager){
      this.getManagedFixedAssets()
    }
  },
  methods: {
    async getManagedFixedAssets(){
      this.loading = true
      this.fixedAssets = (await this.$axios.get('/managed/' + this.username)).data
      this.loading = false
    },
    async getAssignedFixedAssets(){
      this.loading = true
      this.fixedAssets = (await this.$axios.get('/assigned/' + this.username)).data
      this.loading = false
    },
    async getFixedAsset(){
      this.loading = true
        this.fixedAssets = (await this.$axios.get('/fixed-asset/' + this.search)).data
        this.loading = false
    },
    async addFixedAsset(data){
        this.loading = true
        await this.$axios.post('/fixed-asset/', data)
        this.getManagedFixedAssets()
        this.loading = false
    },
    async deleteFixedAsset(code){
      if (confirm("Ar tikrai norite ištrinti ilgalaikį turtą, nr. " + code + "?")) {
        this.loading = true
        await this.$axios.delete('/fixed-asset/' + code)
        this.getManagedFixedAssets()
        this.loading = false
      }
    },
    async assignFixedAsset(){
      this.loading = true
      await this.$axios.post('/fixed-asset/assign/', { code: this.code, assignedBy: auth.getUsername(), assignTo: this.employeeUsername })
      this.getManagedFixedAssets()
      this.loading = false
    },
    async storeAssignedFixedAsset(code){
      if (confirm("Ar tikrai norite sandėliuoti ilgalaikį turtą nr. " + code + "?")) {
        this.loading = true
        await this.$axios.post('/fixed-asset/store/', { code: code, requestedBy: auth.getUsername()})
        this.getManagedFixedAssets()
        this.loading = false
      }
    },
    async acceptAssignedAsset(code){
      if (confirm("Ar tikrai norite priimti ilgalaikį turtą nr. " + code + "?")) {
        this.loading = true
        await this.$axios.post('/fixed-asset/accept/', { code: code, requestedBy: auth.getUsername()})
        this.getAssignedFixedAssets()
        this.loading = false
      }
    },
    async rejectAssignedAsset(code){
      if (confirm("Ar tikrai norite atmesti ilgalaikį turtą nr. " + code + "?")) {
        this.loading = true
        await this.$axios.post('/fixed-asset/reject/', { code: code, requestedBy: auth.getUsername()})
        this.getAssignedFixedAssets()
        this.loading = false
      }
    },
    getTypeText(eventType) {
      switch (eventType){
        case 1:
          return "Priskrita naudotojui"
        case 2:
          return "Patvirtinta naudotojo"
        case 3:
          return "Atmesta naudotojo"
        case 5:
          return "Sandėliuojama"
      }
    },
    itemRowBackground(item) {
      switch (item.eventType){
        case 1:
          return "blue lighten-5"
        case 2:
          return "green lighten-5"
        case 3:
          return "red lighten-5"
        case 5:
          return "yellow lighten-5"
      }      
    },
  }
}
</script>
