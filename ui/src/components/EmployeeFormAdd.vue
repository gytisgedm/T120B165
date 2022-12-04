<template>
    <k-dialog
           title="Pridėti naują asmenį atsakingą už IT "
           :value=show
           width="900"
           persistent
           scrollable
           @input="$emit('close')"
   >
       <v-text-field
           label="Vartotojo vardas"
           :rules="[rules.required]"
           v-model="username"
       />   
       <v-text-field
           label="Slaptažodis"
           :rules="[rules.required]"
           v-model="password"
       />    
       <v-text-field
           label="Vardas"
           :rules="[rules.required]"
           v-model="name"
       />    
       <v-text-field
           label="Pavardė"
           :rules="[rules.required]"
           v-model="surname"
       />    
       <v-text-field
           label="Administratorius"
           v-model="isAdmin"
       />    
       <v-text-field
           label="Departamentas"
           v-model="department"
       />    

       <template #actions>
           <v-spacer />
           <v-btn text color="red" @click="$emit('close')">Atšaukti</v-btn>
           <v-btn text color="blue" @click="if(username != '' && name != '' && surname != ''){$emit('add', data); $emit('close')}">Pridėti</v-btn>
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
       },
       data() {
           return {
               username: '',
               name: '',
               surname: '',
               isAdmin: false,
               department: '',
               password: '',
               rules:{
                   required: v => !!v || "Laukas privalomas"
               },
           }
       },
       computed:{
            data(){
                return{
                    username: this.username,
                    password: this.password,
                    name: this.name,
                    surname: this.surname,
                    isAdmin: this.isAdmin,
                    department: this.department,
                }
            }
       }
   }
   
   </script>
   
   