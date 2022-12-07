<template>
    <k-dialog
           :title=title
           :value=show
           width="900"
           persistent
           scrollable
           @input="$emit('close')"
   >
   <v-text-field
          v-model="search"
          autofocus
          clearable
          label="Paieška"
          name="search-field"
    />
    <v-data-table
            v-model="selected"
            name="employee-select-table"
            show-select
            :single-select=true
            :items="employees"
            :headers="headers"
            :loading="loading"
            :search="search"
            sort-by="name"
            item-key="username"
           @click:row="selectedEmployee"
        >
    </v-data-table>
   
    <template #actions>
        <v-spacer />
        <v-btn text color="red" @click="$emit('close')">Atšaukti</v-btn>
        <v-btn text color="blue" @click="{ $emit('selected', selected); $emit('close')}">Pasirinkti</v-btn>
    </template>
    </k-dialog>
</template>
   
   <script>
   import KDialog from './base/dialog'
   
   export default {
       components: { KDialog },
       props: {
           show: {
               type: Boolean,
               required: true,
               default: false,
           },
           title: {
               type: String,
               required: true,
               default: "Pasirinkite darbuotoją",
           },
       },
       data() {
           return {
               selected: {},
               employees: [],
               search: null,
               headers: [
                { text: 'Vartotojo vardas', value: 'username' },
                { text: 'Vardas', value: 'name' },
                { text: 'Pavardė', value: 'surname' },
                { text: 'Skyrius', value: 'department' },
            ],
               rules:{
                   required: v => !!v || "Laukas privalomas"
               },
           }
       },
       created(){
            this.getEmployees()
       },
       methods: {
            async getEmployees(){
                this.loading = true
                this.employees = (await this.$axios.get('/employees/')).data
                this.loading = false
            },
            selectedEmployee(employee) {
                if (this.show === false) {
                    this.$emit('selected', employee)
                    this.$emit('close')
                }
            },
       },
   }
   </script>
   
   