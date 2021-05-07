export const namespaced = true

export const state = {
  notifications: []
}

let nextId = 1

export const mutations = {
  PUSH(state, notification) {
    state.notifications.push({
      ...notification,
      id: nextId++
    })
  },
  DELETE(state, notificationToRemove) {
    state.notifications = state.notifications.filter(
      (notification) => notification.id !== notificationToRemove.id
    )
  }
}
export const actions = {
  addNotification({ commit }, notification) {
    commit('PUSH', notification)
  },
  removeNotification({ commit }, notificationToRemove) {
    commit('DELETE', notificationToRemove)
  }
}
