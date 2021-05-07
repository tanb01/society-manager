<template>
  <div class="page">
    <h1>Welcome to Society Manager</h1>
    <v-container>
      <v-carousel v-model="model" hide-delimiters>
        <v-carousel-item v-for="action in actions" :key="action">
          <v-sheet color="primary" height="100%" tile>
            <v-row class="fill-height" align="center" justify="center">
              <div class="display-2">{{ action }}</div>
            </v-row>
          </v-sheet>
        </v-carousel-item>
      </v-carousel>
    </v-container>
  </div>
</template>

<script>
export default {
  name: 'home',
  data: () => ({
    model: 0,
    colors: ['primary', 'secondary', 'yellow darken-2', 'red', 'orange'],
    actions: [
      'Managing a society has never been easier',
      'Seamlessly add members and events',
      'View your societies and upcoming events'
    ]
  })
}
</script>
