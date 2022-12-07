<template>
 <k-dialog
        title="Pakeisti asmens atsakingo už IT kategoriją"
        :value=show
        width="900"
        persistent
        scrollable
        @input="$emit('close')"
>
    <v-select
        label="Kategorija"
        v-model="category"
        :rules="[rules.required]"
        :items="categories"
    />

    <template #actions>
        <v-spacer />
        <v-btn text color="red" @click="$emit('close')">Atšaukti</v-btn>
        <v-btn text color="blue" @click="if(username != '' && username != ''){ $emit('update', formData); $emit('close')}">Atnaujinti</v-btn>
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
        username: {
            type: String,
            required: false,
            default: '',
        },
        oldCategory: {
            type: String,
            required: false,
            default: '',
        },
        category: {
            type: String,
            required: false,
            default: '',
        },
    },
    data() {
        return {
            savedCategory: null,
            rules:{
                required: v => !!v || "Laukas privalomas"
            },
            categories: ['MOB', 'KOMPIUT', 'SPAUSD']
        }
    },
    computed:{
        formData(){
            return {currentCategory: this.oldCategory, newCategory: this.category, username: this.username}
        }
    }
}
</script>

