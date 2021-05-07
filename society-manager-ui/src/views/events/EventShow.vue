<template>
  <div class="page">
    <h1>Showing event #{{ event.eventId }}</h1>
    <v-container>
      <v-card elevation="2" outlined>
        <v-row>
          <v-col>
            <v-card-title class="display-1 text--primary">{{
              event.name
            }}</v-card-title></v-col
          >
          <v-col>
            <div class="button-group">
              <v-btn
                :to="{ name: 'event-edit', params: { id: event.eventId } }"
                text
                color="primary"
                ><v-icon left>mdi-pencil</v-icon>
                <span>Edit</span>
              </v-btn>
              <v-btn text color="red" @click="deleteEvent()"
                ><v-icon left>mdi-delete</v-icon>
                <span>Delete</span>
              </v-btn>
              <ConfirmDialog ref="confirm" />
            </div>
          </v-col>
        </v-row>
        <v-card-text>
          <div class="headline text--primary">
            Date of event: {{ event.eventDate | formatDate }}
          </div>
          <!-- <v-date-picker v-model="picker" readonly></v-date-picker> -->
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>
<script>
import { mapState } from 'vuex'
import dayjs from 'dayjs'
import calendar from 'dayjs/plugin/calendar'
import updateLocale from 'dayjs/plugin/updateLocale'
import ConfirmDialog from '@/components/ConfirmDialog'

export default {
  props: ['id'],
  components: {
    ConfirmDialog: ConfirmDialog
  },

  data() {
    return {
      picker: '' // this.picker = this.event.eventDate.substr(0, 10)
    }
  },
  created() {
    this.$store.dispatch('event/fetchEvent', this.id)
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
        this.$store.dispatch('event/deleteEvent', this.event).then(() => {
          this.$router.push({
            name: 'event-list'
          })
        })
      }
    }
  },
  computed: mapState({
    event: (state) => state.event.event
  })
}
</script>

<style scoped>
.button-group {
  padding: 10px;
  text-align: right;
}
</style>
