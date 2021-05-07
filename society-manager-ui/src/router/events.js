import { authGuard } from '@/auth/authGuard'
import EventList from '@/views/events/EventList'
import EventShow from '@/views/events/EventShow'
import EventCreate from '@/views/events/EventCreate'
import EventEdit from '@/views/events/EventEdit'

export default [
  {
    path: '/events',
    name: 'event-list',
    component: EventList
  },
  {
    path: '/events/:id',
    name: 'event-show',
    component: EventShow,
    props: true,
    beforeEnter: authGuard
  },
  {
    path: '/event/create',
    name: 'event-create',
    component: EventCreate,
    beforeEnter: authGuard
  },
  {
    path: '/event/:id/edit',
    name: 'event-edit',
    component: EventEdit,
    props: true,
    beforeEnter: authGuard
  }
]
