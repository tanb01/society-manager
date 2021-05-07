<template>
  <v-app id="app">
    <NavBar />
    <NotificationContainer />
    <router-view :key="$route.fullPath" />
  </v-app>
</template>

<script>
import NavBar from '@/components/NavBar'
import NotificationContainer from '@/components/NotificationContainer.vue'

export default {
  name: 'App',
  components: {
    NavBar,
    NotificationContainer
  },
  data: () => ({
    //
  })
}
</script>

<style>
html {
  -webkit-text-size-adjust: 100%;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
body {
  font-family: 'Open Sans', sans-serif;
  font-size: 16px;
  line-height: 1.5;
}
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
.page {
  margin-top: 6rem;
  margin-bottom: 6rem;
}
a {
  text-decoration: none;
}
</style>
