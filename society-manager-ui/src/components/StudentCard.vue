<template>
  <v-container>
    <div class="student-card -shadow">
      <router-link
        class="student-link"
        :to="{ name: 'student-show', params: { id: student.studentId } }"
      >
        <div>
          <h3 class="title">{{ student.firstName }} {{ student.lastName }}</h3>
          <p class="eyebrow">Email: {{ this.student.email }}</p>
        </div>
      </router-link>
      <div class="button-group">
        <router-link
          :to="{ name: 'student-edit', params: { id: student.studentId } }"
        >
          <v-btn text color="primary"
            ><v-icon left>mdi-pencil</v-icon>
            <span>Edit</span>
          </v-btn>
        </router-link>
        <v-btn text color="red" @click="deleteStudent()"
          ><v-icon left>mdi-delete</v-icon>
          <span>Delete</span>
        </v-btn>
        <ConfirmDialog ref="confirm" />
      </div>
    </div>
  </v-container>
</template>

<script>
import ConfirmDialog from '@/components/ConfirmDialog'

export default {
  props: {
    student: Object
  },
  components: {
    ConfirmDialog: ConfirmDialog
  },
  methods: {
    async deleteStudent() {
      if (
        await this.$refs.confirm.open(
          'Confirm',
          'Are you sure you want to delete this student?'
        )
      ) {
        this.$store.dispatch('student/deleteStudent', this.student)
      }
    }
  }
}
</script>

<style scoped>
.student-card {
  margin: 0 auto;
  max-width: 700px;
  padding: 20px;
  transition: all 0.2s linear;
  cursor: pointer;
}
.student-card:hover {
  transform: scale(1.01);
  box-shadow: 0 3px 12px 0 rgba(0, 0, 0, 0.2), 0 1px 15px 0 rgba(0, 0, 0, 0.19);
}
.student-card > .title {
  margin: 0;
}
.student-link {
  color: black;
  text-decoration: none;
  font-weight: 100;
}
.button-group {
  text-align: right;
}
</style>
