<template>
  <div class="page">
    <h1>Events</h1>
    <v-btn :to="{ name: 'event-create' }" text color="blue"
      ><v-icon left>mdi-plus-circle-outline</v-icon>
      <span>Create an Event</span>
    </v-btn>
    <EventCard v-for="event in events" :key="event.eventId" :event="event" />
  </div>
</template>

<script>
import EventCard from '@/components/EventCard.vue'
import { mapState } from 'vuex'

export default {
  components: {
    EventCard
  },
  created() {
    this.$store.dispatch('event/fetchEvents')
  },
  computed: mapState({
    events: (state) => state.event.events
  })
}
</script>
