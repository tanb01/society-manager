<template>
  <div class="page">
    <v-container>
      <v-card class="profile">
        <v-card-text>
          <v-flex>
            <v-avatar>
              <img :src="$auth.user.picture" alt="User's profile picture" />
            </v-avatar>
          </v-flex>
          <v-text-field
            v-model="$auth.user.name"
            label="First Name"
          ></v-text-field>
          <v-text-field
            v-model="$auth.user.email"
            label="Email Address"
          ></v-text-field>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<style scoped>
.profile {
  max-width: 600px;
  margin: auto;
}
</style>
