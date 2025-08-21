import { defineStore } from 'pinia'
import { ref } from 'vue'
import { apiFetch, setUserId as setUserIdSvc } from '../services/api'

export const useAuthStore = defineStore('auth', () => {
  const userId = ref<string | null>(localStorage.getItem('userId'))
  const error = ref<string | null>(null)

  function setUserId(id: string | null) {
    userId.value = id
    if (id) {
      localStorage.setItem('userId', id)
      setUserIdSvc(id)
    } else {
      localStorage.removeItem('userId')
      setUserIdSvc(null)
    }
  }

  async function register(email: string, password: string) {
    error.value = null
    if (!email || !password) {
      error.value = 'Email and password are required'
      return null
    }
    try {
      const r = await apiFetch('/Auth/register', {
        method: 'POST',
        body: JSON.stringify({ email, passwordHash: password })
      })
      const id = r?.userId ?? r?.UserId ?? r?.UserId ?? r
      setUserId(id)
      return id
    } catch (ex: any) {
      error.value = ex.message
      throw ex
    }
  }

  return { 
    userId, 
    error,
    setUserId, 
    register 
  }
})
