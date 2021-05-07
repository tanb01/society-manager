import { authGuard } from '@/auth/authGuard'
import SocietyList from '@/views/societies/SocietyList'
import SocietyShow from '@/views/societies/SocietyShow'
import SocietyCreate from '@/views/societies/SocietyCreate'
import SocietyEdit from '@/views/societies/SocietyEdit'
import MySocieties from '@/views/societies/MySocieties'

export default [
  {
    path: '/societies',
    name: 'society-list',
    component: SocietyList
  },
  {
    path: '/societies/:id',
    name: 'society-show',
    component: SocietyShow,
    props: true
  },
  {
    path: '/society/create',
    name: 'society-create',
    component: SocietyCreate,
    beforeEnter: authGuard
  },
  {
    path: '/society/:id/edit/',
    name: 'society-edit',
    component: SocietyEdit,
    props: true,
    beforeEnter: authGuard
  },
  {
    path: '/my-societies',
    name: 'my-societies-list',
    component: MySocieties,
    beforeEnter: authGuard
  }
]
