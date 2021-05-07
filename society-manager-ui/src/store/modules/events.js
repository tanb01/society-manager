import EventService from '@/services/EventService'
export const namespaced = true

export const state = {
  events: [],
  event: {}
}

export const mutations = {
  CREATE_EVENT(state, event) {
    state.events.push(event)
  },
  SET_EVENTS(state, events) {
    state.events = events
  },
  SET_EVENT(state, event) {
    state.event = event
  },
  DELETE_EVENT(state, event) {
    var index = state.events.findIndex((e) => e.eventId == event.eventId)
    state.events.splice(index, 1)
  }
}

export const actions = {
  createEvent({ commit }, event) {
    return EventService.postEvent(event)
      .then((response) => {
        commit('CREATE_EVENT', event)
        const notification = {
          type: 'success',
          message: 'Your event has been created!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        return response.data
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message: 'There was a problem creating your event: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  deleteEvent({ commit }, event) {
    return EventService.deleteEvent(event.eventId)
      .then(() => {
        commit('DELETE_EVENT', event)
        const notification = {
          type: 'success',
          message: 'Your event has been successfully deleted!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message: 'There was a problem deleting your event: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  updateEvent({ commit }, event) {
    return EventService.putEvent(event)
      .then(() => {
        commit('SET_EVENT', event)
        const notification = {
          type: 'success',
          message: 'Your event has been modified!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message: 'There was a problem modifying your event: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  fetchEvents({ commit }) {
    EventService.getEvents()
      .then((response) => {
        commit('SET_EVENTS', response.data)
      })
      .catch((error) => {
        console.log('There was an error:', error.response)
      })
  },
  fetchEvent({ commit, getters }, id) {
    var event = getters.getEventById(id)
    if (event) {
      commit('SET_EVENT', event)
    } else {
      EventService.getEvent(id)
        .then((response) => {
          commit('SET_EVENT', response.data)
        })
        .catch((error) => {
          console.log('There was an error:', error.response)
        })
    }
  }
}

export const getters = {
  getEventById: (state) => (id) => {
    return state.events.find((event) => event.eventId === id)
  }
}
