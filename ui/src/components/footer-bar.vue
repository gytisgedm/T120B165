<template>
  <v-footer app bottom fixed padless color="green darken-3">
    <v-container>
      <v-row
          justify="center"
          no-gutters
        >
        <v-col class="green darken-3 text-center white--text">
            <v-btn
              color="white"
              text
              rounded
              class="my-2"
              to="/fixed-assets"
            >
              Pagrindinis puslapis
            </v-btn>
        </v-col>
        <v-col
          class="green darken-3 text-center white--text"
          cols="12"
        >
          {{ new Date().getFullYear() }} — <strong>Ilgalaikio turto sistema</strong>
        </v-col>
      </v-row>
    </v-container>
  </v-footer>
</template>