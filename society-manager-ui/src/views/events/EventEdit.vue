<template>
  <div class="page">
    <h1>Editing Event</h1>
    <h3>Change the name and date of your event</h3>
    <v-container>
      <v-row>
        <v-col>
          <form @submit.prevent="updateEvent">
            <div class="field">
              <v-text-field
                label="Name"
                v-model="event.name"
                type="text"
                required
                clearable
              />
            </div>
            <div class="field">
              <v-label>Date of Event</v-label>
              <v-menu
                v-model="menu"
                :close-on-content-click="false"
                :nudge-right="40"
                transition="scale-transition"
                offset-y
                min-width="auto"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="date"
                    label="Date"
                    prepend-icon="mdi-calendar"
                    readonly
                    required
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="date"
                  @input="menu = false"
                ></v-date-picker>
              </v-menu>
            </div>
            <v-btn type="submit" class="blue white--text">Save Changes</v-btn>
          </form>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  props: ['id'],
  data() {
    return {
      date: null,
      menu: false
    }
  },
  created() {
    this.$store.dispatch('event/fetchEvent', this.id)
  },
  methods: {
    updateEvent() {
      this.event.eventDate = this.date
      this.$store.dispatch('event/updateEvent', this.event)
    }
  },
  computed: mapState({
    event: (state) => state.event.event
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
