<template>
  <v-app>
    <v-main>
      <v-container class="fill-height" fluid>
        <v-row align="center" justify="center">
          <router-view />
        </v-row>
      </v-container>
      <snackbar />
    </v-main>
  </v-app>
</template>

<script>
import Snackbar from "./components/snackbar";
export default {
  components: { Snackbar },
};
</script>



