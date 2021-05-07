import StudentList from '@/views/students/StudentList'
import StudentShow from '@/views/students/StudentShow'
import StudentCreate from '@/views/students/StudentCreate'
import StudentEdit from '@/views/students/StudentEdit'

export default [
  {
    path: '/students',
    name: 'student-list',
    component: StudentList
  },
  {
    path: '/students/:id',
    name: 'student-show',
    component: StudentShow,
    props: true
  },
  {
    path: '/student/create',
    name: 'student-create',
    component: StudentCreate
  },
  {
    path: '/student/:id/edit/',
    name: 'student-edit',
    component: StudentEdit,
    props: true
  }
]
