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
        label="Kategorija"
        :rules="[rules.required]"
        v-model="category"
    />    

    <template #actions>
        <v-spacer />
        <v-btn text color="red" @click="$emit('close')">Atšaukti</v-btn>
        <v-btn text color="blue" @click="if(username != '' && category != ''){ $emit('add', data); $emit('close')}">Pridėti</v-btn>
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
            category: '',
            rules:{
                required: v => !!v || "Laukas privalomas"
            },
        }
    },
    computed:{
        data(){
            return {faCategory: this.category, username: this.username}
        }
    }
}
</script>

