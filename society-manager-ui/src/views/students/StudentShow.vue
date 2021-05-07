<template>
  <div class="page">
    <h1>Showing student #{{ student.studentId }}</h1>
    <v-container>
      <v-card elevation="2" outlined>
        <v-row>
          <v-col>
            <v-card-title class="display-1 text--primary"
              >{{ student.firstName }} {{ student.lastName }}</v-card-title
            ></v-col
          >
          <v-col>
            <div class="button-group">
              <v-btn
                :to="{
                  name: 'student-edit',
                  params: { id: student.studentId }
                }"
                text
                color="primary"
                ><v-icon left>mdi-pencil</v-icon>
                <span>Edit</span>
              </v-btn>
              <v-btn text color="red" @click="deleteStudent()"
                ><v-icon left>mdi-delete</v-icon>
                <span>Delete</span>
              </v-btn>
              <ConfirmDialog ref="confirm" />
            </div>
          </v-col>
          <v-card-text>
            <p class="headline text--primary">
              Student Id: {{ student.studentId }}
            </p>
            <p class="headline text--primary">
              Date of birth: {{ student.birthDate | formatDate }}
            </p>
            <p class="headline text--primary">Email: {{ student.email }}</p>
          </v-card-text>
        </v-row>
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
  created() {
    dayjs.extend(calendar)
    dayjs.extend(updateLocale)
    dayjs.updateLocale('en', {
      calendar: {
        sameElse: 'DD/MM/YYYY'
      }
    })
    this.$store.dispatch('student/fetchStudent', this.id)
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
    async deleteStudent() {
      if (
        await this.$refs.confirm.open(
          'Confirm',
          'Are you sure you want to delete this student?'
        )
      ) {
        this.$store.dispatch('student/deleteStudent', this.student).then(() => {
          this.$router.push({
            name: 'student-list'
          })
        })
      }
    }
  },
  computed: mapState({
    student: (state) => state.student.student
  })
}
</script>

<style scoped>
.button-group {
  padding: 10px;
  text-align: right;
}
</style>
