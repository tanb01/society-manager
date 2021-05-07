<template>
  <div class="page">
    <h1>Students</h1>
    <v-btn :to="{ name: 'student-create' }" text color="primary"
      ><v-icon left>mdi-plus-circle-outline</v-icon>
      <span>Create a Student</span>
    </v-btn>
    <StudentCard
      v-for="student in students"
      :key="student.studentId"
      :student="student"
    />
  </div>
</template>

<script>
import StudentCard from '@/components/StudentCard.vue'
import { mapState } from 'vuex'

export default {
  components: {
    StudentCard
  },
  created() {
    this.$store.dispatch('student/fetchStudents')
  },
  computed: mapState({
    students: (state) => state.student.students
  })
}
</script>
