<template>
  <div class="page">
    <h1>Showing society #{{ society.societyId }}</h1>
    <v-container>
      <v-card elevation="2" outlined>
        <v-row>
          <v-col>
            <v-card-title class="display-1 text--primary">{{
              society.name
            }}</v-card-title></v-col
          >
          <v-col>
            <div class="button-group">
              <v-btn
                v-if="society.societyId"
                :to="{
                  name: 'society-edit',
                  params: { id: society.societyId }
                }"
                text
                color="primary"
                ><v-icon left>mdi-pencil</v-icon>
                <span>Edit</span>
              </v-btn>
              <v-btn text color="red" @click="deleteSociety()"
                ><v-icon left>mdi-delete</v-icon>
                <span>Delete</span>
              </v-btn>
              <ConfirmDialog ref="confirm" />
            </div>
          </v-col>
        </v-row>
        <v-card-text>
          <div class="headline text--primary">
            {{ society.description }}
          </div>
        </v-card-text>
        <v-card-subtitle>
          <p v-if="society.members && society.members.length > 0">
            {{ society.members.length }} member(s)
          </p>
          <p v-if="society.events && society.events.length > 0">
            Hosting {{ society.events.length }} event(s)
          </p>
          <p>Created on: {{ society.creationDate | formatDate }}</p>
        </v-card-subtitle>
        <v-container>
          <v-row>
            <v-col
              ><div>
                <v-card-text>
                  <div class="headline text--primary">Members</div>
                </v-card-text>
                <v-text-field
                  v-model="searchMembers"
                  append-icon="mdi-magnify"
                  label="Search"
                  single-line
                  hide-details
                ></v-text-field>
                <v-data-table
                  :headers="membersHeaders"
                  :items="society.members"
                  :items-per-page="5"
                  :search="searchMembers"
                  class="elevation-1"
                >
                  <template v-slot:item.actions="{ item }">
                    <v-icon small color="red" @click="removeMember(item)">
                      mdi-delete
                    </v-icon>
                    <ConfirmDialog ref="confirm" />
                  </template>
                </v-data-table>
              </div>
              <div class="mt-4">
                <v-dialog v-model="dialogAddMember" max-width="500px">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn
                      color="primary"
                      dark
                      class="mb-2"
                      v-bind="attrs"
                      v-on="on"
                    >
                      Add member
                    </v-btn>
                  </template>
                  <v-card>
                    <v-card-title>
                      <span class="headline">Add Member</span>
                    </v-card-title>

                    <v-container>
                      <v-card elevation="1" max-width="400" class="mx-auto">
                        <v-virtual-scroll
                          :bench="benched"
                          :items="filteredMembers"
                          height="300"
                          item-height="64"
                        >
                          <template v-slot:default="{ item }">
                            <v-list-item :key="item.studentId">
                              <v-list-item-action>
                                <v-btn fab small depressed color="primary">
                                  {{ item.firstName[0] }}{{ item.lastName[0] }}
                                </v-btn>
                              </v-list-item-action>

                              <v-list-item-content>
                                <v-list-item-title>
                                  <strong
                                    >{{ item.firstName }}
                                    {{ item.lastName }}</strong
                                  >
                                </v-list-item-title>
                              </v-list-item-content>

                              <v-list-item-action>
                                <v-btn
                                  text
                                  color="blue"
                                  @click="addMember(item)"
                                >
                                  <v-icon small>
                                    mdi-plus-circle-outline
                                  </v-icon></v-btn
                                >
                              </v-list-item-action>
                            </v-list-item>

                            <v-divider></v-divider>
                          </template>
                        </v-virtual-scroll>
                      </v-card>
                    </v-container>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="blue darken-1"
                        text
                        @click="closeAddMemberDialog"
                      >
                        Close
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </div></v-col
            >
            <v-col
              ><div>
                <v-card-text>
                  <div class="headline text--primary">Events</div>
                </v-card-text>
                <v-text-field
                  v-model="searchEvents"
                  append-icon="mdi-magnify"
                  label="Search"
                  single-line
                  hide-details
                ></v-text-field>
                <v-data-table
                  :headers="eventsHeaders"
                  :items="society.events"
                  :items-per-page="5"
                  :search="searchEvents"
                  class="elevation-1"
                >
                  <template v-slot:item="{ item }">
                    <tr>
                      <td class="text-start">{{ item.name }}</td>
                      <td class="text-start">
                        {{ item.eventDate | formatDate }}
                      </td>
                      <td class="text-start">
                        <v-icon small color="red" @click="removeEvent(item)">
                          mdi-delete
                        </v-icon>
                      </td>
                    </tr>
                  </template>
                </v-data-table>
              </div>
              <div class="mt-4">
                <v-dialog v-model="dialogAddEvent" max-width="800px">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn
                      color="primary"
                      dark
                      class="mb-2"
                      v-bind="attrs"
                      v-on="on"
                    >
                      Add event
                    </v-btn>
                  </template>
                  <v-card>
                    <v-card-title>
                      <span class="headline">Add Event</span>
                    </v-card-title>

                    <v-container>
                      <v-card elevation="1" max-width="700" class="mx-auto">
                        <v-virtual-scroll
                          :bench="benched"
                          :items="filteredEvents"
                          height="300"
                          item-height="64"
                        >
                          <template v-slot:default="{ item }">
                            <v-list-item :key="item.eventId">
                              <v-list-item-content>
                                <v-list-item-title>
                                  <strong>{{ item.name }}</strong>
                                </v-list-item-title>
                              </v-list-item-content>
                              <v-list-item-content>
                                <v-list-item-title>
                                  {{ item.eventDate | formatDate }}
                                </v-list-item-title>
                              </v-list-item-content>

                              <v-list-item-action>
                                <v-btn
                                  text
                                  color="blue"
                                  @click="addEvent(item)"
                                >
                                  <v-icon small>
                                    mdi-plus-circle-outline
                                  </v-icon></v-btn
                                >
                              </v-list-item-action>
                            </v-list-item>

                            <v-divider></v-divider>
                          </template>
                        </v-virtual-scroll>
                      </v-card>
                    </v-container>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="blue darken-1"
                        text
                        @click="closeAddEventDialog"
                      >
                        Close
                      </v-btn>
                      <!-- <v-btn color="blue darken-1" text @click="save"> Add </v-btn> -->
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </div>
            </v-col>
          </v-row>
        </v-container>
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
      membersHeaders: [
        { text: 'First Name', value: 'firstName' },
        { text: 'Last Name', value: 'lastName' },
        { text: 'Email', value: 'email' },
        { text: 'Actions', value: 'actions', sortable: false }
      ],
      eventsHeaders: [
        { text: 'Name', value: 'name' },
        { text: 'Date of event', value: 'eventDate' },
        { text: 'Actions', value: 'actions', sortable: false }
      ],
      dialogAddMember: false,
      dialogAddEvent: false,
      benched: 0,
      searchMembers: '',
      searchEvents: ''
    }
  },
  created() {
    this.$store.dispatch('society/fetchSociety', this.id)
    this.$store.dispatch('student/fetchStudents')
    this.$store.dispatch('event/fetchEvents')
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
        this.$store.dispatch('society/deleteSociety', this.society).then(() => {
          this.$router.push({
            name: 'society-list'
          })
        })
      }
    },
    addMember(student) {
      this.$store.dispatch('society/addMember', {
        society: this.society,
        student: student
      })
    },
    async removeMember(student) {
      if (
        await this.$refs.confirm.open(
          'Confirm',
          'Are you sure you want to remove this member from the society?'
        )
      ) {
        this.$store.dispatch('society/removeMember', {
          society: this.society,
          student: student
        })
      }
    },
    addEvent(event) {
      this.$store.dispatch('society/addEvent', {
        society: this.society,
        event: event
      })
    },
    async removeEvent(event) {
      if (
        await this.$refs.confirm.open(
          'Confirm',
          'Are you sure you want to remove this event from the society?'
        )
      ) {
        this.$store.dispatch('society/removeEvent', {
          society: this.society,
          event: event
        })
      }
    },
    closeAddMemberDialog() {
      this.dialogAddMember = false
    },
    closeAddEventDialog() {
      this.dialogAddEvent = false
    }
  },
  computed: {
    filteredMembers() {
      return this.students.filter((st) => {
        return !this.society.members.some((m) => st.studentId === m.studentId)
      })
    },
    filteredEvents() {
      return this.events.filter((e) => {
        return !this.society.events.some((m) => e.eventId === m.eventId)
      })
    },
    ...mapState({
      society: (state) => state.society.society,
      students: (state) => state.student.students,
      events: (state) => state.event.events
    })
  }
}
</script>

<style scoped>
.button-group {
  padding: 10px;
  text-align: right;
}
</style>
