<template>
 <k-dialog
        title="Pridėti naują ilgalaikį turtą"
        :value=show
        width="900"
        persistent
        scrollable
        @input="$emit('close')"
>
    <v-text-field
        label="Kodas"
        :rules="[rules.required, rules.numeric]"
        counter="6"
        maxlength="6"
        v-model="code"
    />   
    <v-select
        label="Kategorija"
        v-model="category"
        :rules="[rules.required]"
        :items="categories"
    />
    <v-text-field
        label="Aprašymas"
        :rules="[rules.required]"
        v-model="description"
    />    
    <v-text-field
        label="Serijinis numeris"
        :rules="[rules.required]"
        v-model="serial"
    />    

    <template #actions>
        <v-spacer />
        <v-btn text color="red" @click="$emit('close')">Atšaukti</v-btn>
        <v-btn text color="blue" @click="if(code != '' && description != '' && serial != ''){ $emit('add', data); $emit('close')}">Pridėti</v-btn>
    </template>
 </k-dialog>

</template>

<script>
import KDialog from './base/dialog'
import auth from '../auth'

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
            code: '',
            description: '',
            category: '',
            serial: '',
            rules:{
                required: v => !!v || "Laukas privalomas",
                numeric: (v) => !isNaN(v) || "Galima vesti tik skaičius",
            },
            categories: ['MOB', 'KOMPIUT', 'SPAUSD']
        }
    },
    computed:{
        data(){
            return {code: this.code, class: this.category, description: this.description, serialNumber: this.serial, managedBy: auth.getUsername()}
        }
    }
}
</script>

