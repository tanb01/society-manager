<template>
  <div>
    <v-app-bar absolute elevate-on-scroll>
      <v-toolbar-title>Society Manager</v-toolbar-title>
      <div id="nav" class="nav">
        <nav>
          <router-link :to="{ name: 'Home' }">Home</router-link> |
          <router-link :to="{ name: 'my-societies-list' }"
            >My Societies</router-link
          >
          |
          <router-link :to="{ name: 'society-list' }">Societies</router-link> |
          <router-link :to="{ name: 'event-list' }">Events</router-link> |
          <router-link :to="{ name: 'student-list' }">Students</router-link>
        </nav>
      </div>

      <v-spacer></v-spacer>

      <span v-if="!$auth.isAuthenticated && !$auth.loading">
        <v-btn color="blue white--text" @click.prevent="login">Login</v-btn>
      </span>
      <div v-if="$auth.isAuthenticated" class="mr-3">
        <a>
          <img
            :src="$auth.user.picture"
            alt="User's profile picture"
            width="30"
          />
        </a>
        <router-link :to="{ name: 'Profile' }">
          <div>{{ $auth.user.name }}</div>
        </router-link>
      </div>
      <span v-if="$auth.isAuthenticated">
        <v-btn color="red white--text" @click.prevent="logout"> Log out </v-btn>
      </span>
    </v-app-bar>
  </div>
</template>

<script>
export default {
  name: 'NavBar',
  methods: {
    login() {
      this.$auth.loginWithRedirect()
    },
    logout() {
      this.$auth.logout()
      this.$router.push({ path: '/' })
    }
  }
}
</script>

<style scoped>
#mobileAuthNavBar {
  min-height: 125px;
  justify-content: space-between;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
