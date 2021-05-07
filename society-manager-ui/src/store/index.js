import Vue from 'vue'
import Vuex from 'vuex'
import * as society from '@/store/modules/societies.js'
import * as event from '@/store/modules/events.js'
import * as student from '@/store/modules/students.js'
import * as notification from '@/store/modules/notification.js'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    society,
    event,
    student,
    notification
  }
})
