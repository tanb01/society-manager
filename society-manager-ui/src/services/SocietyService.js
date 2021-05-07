import apiClient from '@/services/apiClient'

export default {
  async getSocietiesPerPage(limit, page) {
    return await apiClient.get('/societies?limit=' + limit + '&page=' + page)
  },
  async postSociety(society) {
    return apiClient.post('/societies', society)
  },
  async getSociety(id) {
    return await apiClient.get('/societies/' + id)
  },
  async getSocietyMembers(id) {
    return await apiClient.get('/societies/' + id + '/students')
  },
  async getSocietyEvents(id) {
    return await apiClient.get('/societies/' + id + '/events')
  },
  async deleteSociety(id) {
    return apiClient.delete('/societies/' + id)
  },
  async putSociety(society) {
    return apiClient.put('/societies/' + society.societyId, society)
  },
  async addMember(societyId, studentId) {
    return apiClient.post('/societies/' + societyId + '/students/' + studentId)
  },
  async removeMember(societyId, studentId) {
    return apiClient.delete(
      '/societies/' + societyId + '/students/' + studentId
    )
  },
  async addEvent(societyId, eventId) {
    return apiClient.post('/societies/' + societyId + '/events/' + eventId)
  },
  async removeEvent(societyId, eventId) {
    return apiClient.delete('/societies/' + societyId + '/events/' + eventId)
  }
}
