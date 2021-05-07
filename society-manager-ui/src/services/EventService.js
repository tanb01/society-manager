import apiClient from '@/services/apiClient'

export default {
  async getEvents() {
    return await apiClient.get('/events')
  },
  async getEvent(id) {
    return await apiClient.get('/events/' + id)
  },
  async deleteEvent(id) {
    return apiClient.delete('/events/' + id)
  },

  async postEvent(event) {
    return apiClient.post('/events', event)
  },
  async putEvent(event) {
    return apiClient.put('/events/' + event.eventId, event)
  }
}
