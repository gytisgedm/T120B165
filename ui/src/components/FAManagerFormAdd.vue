<template>
 <k-dialog
        title="Pridėti naują asmenį atsakingą už IT "
        :value=show
        width="1100"
        persistent
        scrollable
        @input="$emit('close')"
>
    <v-text-field
        label="Vartotojo vardas"
        :rules="[rules.required]"
        v-model="username"
        readonly
        append-icon="mdi-dots-horizontal"
        @click:append="(showEmployeeDialog = true)"
    />   

    <v-select
        label="Kategorija"
        v-model="category"
        :rules="[rules.required]"
        :items="categories"
    />

    <template #actions>
        <v-spacer />
        <v-btn text color="red" @click="$emit('close')">Atšaukti</v-btn>
        <v-btn text color="blue" @click="if(username != '' && category != ''){ $emit('add', data); $emit('close')}">Pridėti</v-btn>
    </template>

    <EmployeeDialog
        :show=showEmployeeDialog
        title="Priskriti ilgalaikį turtą darbuotojui"
        @close="showEmployeeDialog = false"
        @selected="(employee = $event)"
    />
 </k-dialog>

</template>

<script>
import KDialog from './base/dialog'
import EmployeeDialog from '../components/EmployeeDialog.vue'

export default {
    components: { KDialog, EmployeeDialog },
    props: {
        show: {
            type: Boolean,
            required: true,
            default: false,
        },
    },
    data() {
        return {
            category: '',
            employee: {},
            rules:{
                required: v => !!v || "Laukas privalomas"
            },
            categories: ['MOB', 'KOMPIUT', 'SPAUSD'],
            showEmployeeDialog: false
        }
    },
    computed:{
        data(){
            return {faCategory: this.category, username: this.employee[0].username}
        },
        username(){
            if(this.employee[0]) return this.employee[0].username
            return ''
        }
    },
}
</script>

