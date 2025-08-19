import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const userId = ref<string | null>(localStorage.getItem('userId'))
  function setUserId(id: string | null) {
    userId.value = id
    if (id) localStorage.setItem('userId', id)
    else localStorage.removeItem('userId')
  }
  return { userId, setUserId }
})
