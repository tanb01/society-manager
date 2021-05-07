<template>
  <div class="page">
    <h1>Create an Event</h1>
    <form @submit.prevent="createEvent">
      <h3>Name and date your event</h3>
      <div class="field">
        <v-text-field label="Name" v-model="event.name" type="text" required />
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
              label="Date of event"
              prepend-icon="mdi-calendar"
              readonly
              v-bind="attrs"
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker v-model="date" @input="menu = false"></v-date-picker>
        </v-menu>
      </div>
      <v-btn type="submit" class="blue white--text">Submit</v-btn>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      event: this.createEventObject(),
      date: null,
      menu: false
    }
  },
  methods: {
    createEvent() {
      this.event.eventDate = this.date
      this.$store.dispatch('event/createEvent', this.event).then((response) => {
        this.$router.push({
          name: 'event-show',
          params: { id: response.eventId }
        })
        this.event = this.createEventObject()
      })
    },
    createEventObject() {
      return {
        name: '',
        eventDate: ''
      }
    }
  }
}
</script>

<style scoped>
.field {
  max-width: 300px;
  margin-bottom: 24px;
  margin: 0 auto;
}
</style>
