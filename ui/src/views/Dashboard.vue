<template>
  <div>
    <app-bar>
        <v-toolbar-title>Pagrindinis puslapis</v-toolbar-title>
    </app-bar>
    <nav-bar/>
    <footer-bar/>
  </div>
</template>

<script>

import AppBar from '../components/app-bar'
import NavBar from '../components/nav-bar'
import FooterBar from '../components/footer-bar'

export default { 
  components: { AppBar, NavBar, FooterBar },
  data: () => ({
    
  }),
  created() {
    
  },
  methods: {
    
  }
};
</script>
