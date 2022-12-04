<template>
  <div>
    <app-bar>
        <v-toolbar-title>Pagrindinis puslapis</v-toolbar-title>
    </app-bar>
    <nav-bar/>
  </div>
</template>

<script>

import AppBar from '../components/app-bar'
import NavBar from '../components/nav-bar'

export default { 
  components: { AppBar, NavBar },
  data: () => ({
    
  }),
  created() {
    
  },
  methods: {
    
  }
};
</script>
