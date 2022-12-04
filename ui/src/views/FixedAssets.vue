<template>
  <div>
    <app-bar>
        <v-toolbar-title>Ilgalaikis turtas</v-toolbar-title>
        <v-spacer/>
        <v-btn v-if="isFAManager" icon @click="(showAddForm = true)">
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
      class="elevation-1"
    >
      <template #[`item.actions`]="{ item }">
        <v-btn icon @click="deleteFixedAsset(item.code)">
            <v-icon>mdi-close</v-icon>
        </v-btn>
      </template>
    </v-data-table>

    <FixedAssetFormAdd
        :show=showAddForm
        @close="showAddForm = false"
        @add="addFixedAsset($event)"
    />
  </div>
</template>

<script>

import auth from '../auth'
import AppBar from '../components/app-bar'
import NavBar from '../components/nav-bar'
import FixedAssetFormAdd from '../components/FixedAssetFormAdd'

export default {
  components: { AppBar, NavBar, FixedAssetFormAdd },
  data: () => ({
    isFAManager: auth.isFAManager(),
    isEmployee: auth.isEmployee(),
    username: auth.getUsername(),
    fixedAssets: [],
    loading: false,
    search: null,
    showAddForm: false,
    managerHeaders: [
       { text: 'Kodas', value: 'code' },
       { text: 'Aprašymas', value: 'description' },
       { text: 'Serijinis numeris', value: 'serialNumber' },
       { text: 'Priskirta', value: 'assignedTo' },
       { text: 'Veiksmai', value: 'actions' },
    ],
    employeeHeaders: [
       { text: 'Kodas', value: 'code' },
       { text: 'Aprašymas', value: 'description' },
       { text: 'Serijinis numeris', value: 'serialNumber' },
       { text: 'Priskirė', value: 'assignedBy' },
       { text: 'Veiksmai', value: 'actions' },
    ],
    rules:{
          required: v => !!v || "Laukas privalomas",
          numeric: (v) => !isNaN(v) || "Galima vesti tik skaičius",
    },
  }),
  async created() {
    if (this.isEmployee){
      this.getAssignedFixedAssets()
    }
      
    if (this.isFAManager){
      this.getManagedFixedAssets()
    }
  },
  methods: {
    async getManagedFixedAssets(){
      this.loading = true
      this.fixedAssets = (await this.$axios.get('/managed/' + this.username + '/fixed-assets')).data
      this.loading = false
    },
    async getAssignedFixedAssets(){
      this.loading = true
      this.fixedAssets = (await this.$axios.get('/assigned/' + this.username + '/fixed-assets')).data
      this.loading = false
    },
    async getFixedAsset(){
      this.loading = true
        this.fixedAssets = (await this.$axios.get('/fixed-assets/get/' + this.search)).data
        this.loading = false
    },
    async addFixedAsset(data){
        this.loading = true
        await this.$axios.post('/fixed-asset', data)
        this.getManagedFixedAssets()
        this.loading = false
    },
    async deleteFixedAsset(code){
      if (confirm("Ar tikrai norite ištrinti ilgalaikį turtą, nr. " + code)) {
        this.loading = true
        await this.$axios.delete('/fixed-asset/remove/' + code)
        this.getManagedFixedAssets()
        this.loading = false
      }
    }
  }
}
</script>
