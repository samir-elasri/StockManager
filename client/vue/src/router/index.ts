import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import Products from '../views/Products.vue'

const routes = [
  { path: '/', component: Login },
  { path: '/products', component: Products }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
