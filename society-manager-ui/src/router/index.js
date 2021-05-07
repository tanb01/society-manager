import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '@/views/Home.vue'
import Profile from '@/views/Profile.vue'
import { authGuard } from '@/auth/authGuard'
import NotFoundPage from '@/views/NotFoundPage'
import societiesRoutes from './societies'
import eventsRoutes from './events'
import studentsRoutes from './students'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/profile',
    name: 'Profile',
    component: Profile,
    beforeEnter: authGuard
  },
  ...societiesRoutes,
  ...eventsRoutes,
  ...studentsRoutes,
  {
    path: '*',
    component: NotFoundPage
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
