<template>
  <div class="page">
    <h1>Editing Student</h1>
    <h3>Change the name of your student</h3>
    <v-container>
      <v-row
        ><v-col>
          <form @submit.prevent="updateStudent">
            <div class="field">
              <v-text-field
                label="First Name"
                v-model="student.firstName"
                type="text"
                required
                clearable
              />
            </div>
            <div class="field">
              <v-text-field
                label="Last Name"
                v-model="student.lastName"
                type="text"
                required
                clearable
              />
            </div>
            <div class="field">
              <v-label>Date of Birth</v-label>

              <v-menu
                ref="menu"
                v-model="menu"
                :close-on-content-click="false"
                transition="scale-transition"
                offset-y
                min-width="auto"
              >
                <template v-slot:activator="{ on, attrs }">
                  <v-text-field
                    v-model="date"
                    label="Date of birth"
                    prepend-icon="mdi-calendar"
                    readonly
                    required
                    v-bind="attrs"
                    v-on="on"
                  ></v-text-field>
                </template>
                <v-date-picker
                  ref="picker"
                  v-model="date"
                  :max="new Date().toISOString().substr(0, 10)"
                  min="1950-01-01"
                  @change="save"
                ></v-date-picker>
              </v-menu>
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
  data() {
    return {
      date: null,
      menu: false
    }
  },
  created() {
    this.$store.dispatch('student/fetchStudent', this.id)
  },
  watch: {
    menu(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
    }
  },
  methods: {
    updateStudent() {
      this.student.birthDate = this.date
      this.$store.dispatch('student/updateStudent', this.student)
    },

    save(date) {
      this.$refs.menu.save(date)
    }
  },
  computed: mapState({
    student: (state) => state.student.student
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
