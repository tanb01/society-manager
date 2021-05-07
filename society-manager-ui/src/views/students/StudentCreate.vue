<template>
  <div class="page">
    <h1>Create a Student</h1>
    <form @submit.prevent="createStudent">
      <h3>Provide student details</h3>
      <div class="field">
        <v-text-field
          label="First name"
          v-model="student.firstName"
          type="text"
          required
        />
      </div>
      <div class="field">
        <v-text-field
          label="Last name"
          v-model="student.lastName"
          type="text"
        />
      </div>
      <div class="field">
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
      <div class="field">
        <v-text-field label="Email" v-model="student.email" type="text" />
      </div>
      <v-btn type="submit" class="blue white--text">Submit</v-btn>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      student: this.createStudentObject(),
      date: null,
      menu: false
    }
  },
  watch: {
    menu(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
    }
  },
  methods: {
    createStudent() {
      this.student.birthDate = this.date
      this.$store
        .dispatch('student/createStudent', this.student)
        .then((response) => {
          this.$router.push({
            name: 'student-show',
            params: { id: response.studentId }
          })
          this.student = this.createStudentObject()
        })
    },
    createStudentObject() {
      return {
        firstName: '',
        lastName: '',
        birthDate: '',
        email: ''
      }
    },
    save(date) {
      this.$refs.menu.save(date)
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
