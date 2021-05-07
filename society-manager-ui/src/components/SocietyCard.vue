<template>
  <v-container>
    <div class="society-card -shadow">
      <router-link
        class="society-link"
        :to="{ name: 'society-show', params: { id: society.societyId } }"
      >
        <div>
          <h3 class="title">{{ society.name }}</h3>
          <p class="eyebrow">{{ society.description }}</p>
          <span>{{ society.members.length }} member(s)</span>
          <p>Hosting {{ society.events.length }} event(s)</p>
          <span>Created: {{ this.society.creationDate | formatDate }}</span>
        </div>
      </router-link>
      <div class="button-group">
        <router-link
          :to="{ name: 'society-edit', params: { id: society.societyId } }"
        >
          <v-btn text color="primary"
            ><v-icon left>mdi-pencil</v-icon>
            <span>Edit</span>
          </v-btn>
        </router-link>
        <v-btn text color="red" @click="deleteSociety()"
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
    society: Object
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
    async deleteSociety() {
      if (
        await this.$refs.confirm.open(
          'Confirm',
          'Are you sure you want to delete this society?'
        )
      ) {
        this.$store.dispatch('society/deleteSociety', this.society)
      }
    }
  }
}
</script>

<style scoped>
.society-card {
  margin: 0 auto;
  max-width: 700px;
  padding: 20px;
  transition: all 0.2s linear;
  cursor: pointer;
}
.society-card:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2), 0 1px 15px 0 rgba(0, 0, 0, 0.19);
}
.society-card > .title {
  margin: 0;
}
.society-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
.button-group {
  text-align: right;
}
</style>
