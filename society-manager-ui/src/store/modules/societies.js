import SocietyService from '@/services/SocietyService'
export const namespaced = true

export const state = {
  societies: [],
  // societiesTotal: 0,
  society: {}
}

export const mutations = {
  CREATE_SOCIETY(state, society) {
    state.societies.push(society)
  },
  SET_SOCIETIES(state, societies) {
    state.societies = societies
  },
  // SET_SOCIETIES_TOTAL(state, societiesTotal) {
  //   state.societiesTotal = societiesTotal
  // },
  SET_SOCIETY(state, society) {
    state.society = society
  },
  DELETE_SOCIETY(state, society) {
    var index = state.societies.findIndex(
      (s) => s.societyId == society.societyId
    )
    state.societies.splice(index, 1)
  },
  ADD_MEMBER(state, student) {
    state.society.members.push(student)
  },
  REMOVE_MEMBER(state, student) {
    var index = state.society.members.findIndex(
      (st) => st.studentId == student.studentId
    )
    state.society.members.splice(index, 1)
  },
  ADD_EVENT(state, event) {
    state.society.events.push(event)
  },
  REMOVE_EVENT(state, event) {
    var index = state.society.events.findIndex(
      (st) => st.eventId == event.eventId
    )
    state.society.events.splice(index, 1)
  }
}

export const actions = {
  createSociety({ commit }, society) {
    return SocietyService.postSociety(society)
      .then((response) => {
        commit('CREATE_SOCIETY', society)
        const notification = {
          type: 'success',
          message: 'Your society has been created!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        return response.data
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message: 'There was a problem creating your society: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  deleteSociety({ commit }, society) {
    return SocietyService.deleteSociety(society.societyId)
      .then(() => {
        commit('DELETE_SOCIETY', society)
        const notification = {
          type: 'success',
          message: 'Your society has been successfully deleted!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message: 'There was a problem deleting your society: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  updateSociety({ commit }, society) {
    return SocietyService.putSociety(society)
      .then(() => {
        commit('SET_SOCIETY', society)
        const notification = {
          type: 'success',
          message: 'Your society has been modified!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message:
            'There was a problem modifying your society: ' + error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  fetchSocieties({ commit }, { limit, page }) {
    // console.log('fetchlimit: ', limit)
    SocietyService.getSocietiesPerPage(limit, page)
      .then((response) => {
        // commit(
        //   'SET_SOCIETIES_TOTAL',
        //   parseInt(response.headers['x-total-count'])
        // )
        commit('SET_SOCIETIES', response.data)
      })
      .catch((error) => {
        console.log('There was an error:', error.response)
      })
  },
  fetchSociety({ commit, getters }, id) {
    var society = getters.getSocietyById(id)
    if (society) {
      commit('SET_SOCIETY', society)
    } else {
      SocietyService.getSociety(id)
        .then((response) => {
          commit('SET_SOCIETY', response.data)
        })
        .catch((error) => {
          console.log('There was an error:', error.response)
        })
    }
  },
  addMember({ commit }, { society, student }) {
    return SocietyService.addMember(society.societyId, student.studentId)
      .then(() => {
        commit('ADD_MEMBER', student)
        const notification = {
          type: 'success',
          message: 'A student has been added to the society!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message:
            'There was a problem adding a student to the society: ' +
            error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  removeMember({ commit }, { society, student }) {
    return SocietyService.removeMember(society.societyId, student.studentId)
      .then(() => {
        commit('REMOVE_MEMBER', student)
        const notification = {
          type: 'success',
          message: 'A student has been removed from the society!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message:
            'There was a problem removing the student from the society: ' +
            error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  addEvent({ commit }, { society, event }) {
    return SocietyService.addEvent(society.societyId, event.eventId)
      .then(() => {
        commit('ADD_EVENT', event)
        const notification = {
          type: 'success',
          message: 'An event has been added to the society!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message:
            'There was a problem adding an event to the society: ' +
            error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  },
  removeEvent({ commit }, { society, event }) {
    return SocietyService.removeEvent(society.societyId, event.eventId)
      .then(() => {
        commit('REMOVE_EVENT', event)
        const notification = {
          type: 'success',
          message: 'An event has been removed from the society!'
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
      })
      .catch((error) => {
        const notification = {
          type: 'error',
          message:
            'There was a problem removing the event from the society: ' +
            error.message
        }
        this.dispatch('notification/addNotification', notification, {
          root: true
        })
        throw error
      })
  }
}

export const getters = {
  getSocietyById: (state) => (id) => {
    return state.societies.find((society) => society.societyId === id)
  }
}
