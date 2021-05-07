import StudentService from '@/services/StudentService'
export const namespaced = true

export const state = {
  students: [],
  student: {}
}

export const mutations = {
  CREATE_STUDENT(state, student) {
    state.students.push(student)
  },
  SET_STUDENTS(state, students) {
    state.students = students
  },
  SET_STUDENT(state, student) {
    state.student = student
  },
  DELETE_STUDENT(state, student) {
    var index = state.students.findIndex(
      (st) => st.studentId == student.studentId
    )
    state.students.splice(index, 1)
  }
}

export const actions = {
  createStudent({ commit }, student) {
    return StudentService.postStudent(student)
      .then((response) => {
        commit('CREATE_STUDENT', student)
        const notification = {
          type: 'success',
          message: 'Your student has been created!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        return response.data
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message: 'There was a problem creating your student: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  deleteStudent({ commit }, student) {
    return StudentService.deleteStudent(student.studentId)
      .then(() => {
        commit('DELETE_STUDENT', student)
        const notification = {
          type: 'success',
          message: 'Your student has been successfully deleted!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message: 'There was a problem deleting your student: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  updateStudent({ commit }, student) {
    return StudentService.putStudent(student)
      .then(() => {
        commit('SET_STUDENT', student)
        const notification = {
          type: 'success',
          message: 'Your student has been modified!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message:
            'There was a problem modifying your student: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  fetchStudents({ commit }) {
    StudentService.getStudents()
      .then((response) => {
        commit('SET_STUDENTS', response.data)
      })
      .catch((error) => {
        console.log('There was an error:', error.response)
      })
  },
  fetchStudent({ commit, getters }, id) {
    var society = getters.getStudentById(id)
    if (society) {
      commit('SET_STUDENT', society)
    } else {
      StudentService.getStudent(id)
        .then((response) => {
          commit('SET_STUDENT', response.data)
        })
        .catch((error) => {
          console.log('There was an error:', error.response)
        })
    }
  }
}

export const getters = {
  getStudentById: (state) => (id) => {
    return state.students.find((student) => student.studentId === id)
  }
}
