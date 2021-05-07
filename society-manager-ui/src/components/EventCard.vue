<template>
  <v-container>
    <div class="event-card -shadow">
      <router-link
        class="event-link"
        :to="{ name: 'event-show', params: { id: event.eventId } }"
      >
        <div>
          <h3 class="title">{{ event.name }}</h3>
          <span class="eyebrow"
            >Date: {{ this.event.eventDate | formatDate }}</span
          >
        </div>
      </router-link>
      <div class="button-group">
        <router-link
          :to="{ name: 'event-edit', params: { id: event.eventId } }"
        >
          <v-btn text color="primary"
            ><v-icon left>mdi-pencil</v-icon>
            <span>Edit</span>
          </v-btn>
        </router-link>
        <v-btn text color="red" @click="deleteEvent()"
          ><v-icon left>mdi-delete</v-icon>
          <span>Delete</span>
        </v-btn>
        <ConfirmDialog ref="confirm" />
      </div>
    </div>
  </v-container>
</template>

<script>
import dayjs from 'dayjs'
import calendar from 'dayjs/plugin/calendar'
import updateLocale from 'dayjs/plugin/updateLocale'
import ConfirmDialog from '@/components/ConfirmDialog'

export default {
  props: {
    event: Object
  },
  components: {
    ConfirmDialog: ConfirmDialog
  },
  created() {
    dayjs.extend(calendar)
    dayjs.extend(updateLocale)
    dayjs.updateLocale('en', {
      calendar: {
        sameElse: 'DD/MM/YYYY hh:mm A'
      }
    })
  },
  filters: {
    formatDate: (date) => {
      if (!date) {
        return null
      }
      return dayjs(date).calendar()
    }
  },
  methods: {
    async deleteEvent() {
      if (
        await this.$refs.confirm.open(
          'Confirm',
          'Are you sure you want to delete this event?'
        )
      ) {
        this.$store.dispatch('event/deleteEvent', this.event)
      }
    }
  }
}
</script>

<style scoped>
.event-card {
  margin: 0 auto;
  max-width: 700px;
  padding: 20px;
  transition: all 0.2s linear;
  cursor: pointer;
}
.event-card:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2), 0 1px 15px 0 rgba(0, 0, 0, 0.19);
}
.event-card > .title {
  margin: 0;
}
.event-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
.button-group {
  text-align: right;
}
</style>
