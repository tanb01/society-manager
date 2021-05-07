import apiClient from '@/services/apiClient'

export default {
  async getStudents() {
    return await apiClient.get('/students')
  },
  async getStudent(id) {
    return await apiClient.get('/students/' + id)
  },
  async deleteStudent(id) {
    return apiClient.delete('/students/' + id)
  },
  async postStudent(student) {
    return apiClient.post('/students', student)
  },
  async putStudent(student) {
    return apiClient.put('/students/' + student.studentId, student)
  }
}
