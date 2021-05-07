<template>
  <div class="page">
    <h1>Editing Society</h1>
    <h3>Change the name and description of your society</h3>
    <v-container>
      <v-row>
        <v-col>
          <form @submit.prevent="updateSociety">
            <div class="field">
              <v-text-field
                label="Name"
                v-model="society.name"
                type="text"
                required
                clearable
              />
            </div>
            <div class="field">
              <v-textarea
                label="Description"
                v-model="society.description"
                type="text"
                required
                clearable
              />
            </div>
            <v-btn type="submit" class="blue white--text">Save Changes</v-btn>
          </form></v-col
        >
      </v-row>
    </v-container>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  props: ['id'],
  created() {
    this.$store.dispatch('society/fetchSociety', this.id)
  },
  methods: {
    updateSociety() {
      this.$store.dispatch('society/updateSociety', this.society)
    }
  },
  computed: mapState({
    society: (state) => state.society.society
  })
}
</script>

<style scoped>
.field {
  max-width: 300px;
  margin-bottom: 24px;
  margin: 0 auto;
}
</style>
