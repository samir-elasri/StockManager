<template>
    <div class="container">
      <div class="card" style="max-width:420px;margin:1rem auto;">
        <h2>Register / Sign up</h2>
        <div class="p-fluid">
          <label for="email">Email</label>
          <InputText id="email" v-model="email" />
          <label for="password">Password</label>
          <InputText id="password" type="password" v-model="password" />
          <Button label="Register" @click="register" style="margin-top: 1em" class="!mt-4" />
        </div>
        <div v-if="createdId" class="!py-4">
          <b>User created:</b> {{ createdId }}
          <div class="!pt-4">
            <Button class="!pt-4 !mt-4" label="Go to Categories" @click="gotoCategories" />
          </div>
        </div>
        <div v-if="error" style="color:var(--p-error-color);margin-top:1rem">{{ error }}</div>
      </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { apiFetch, setUserId as setUserIdSvc } from '../services/api'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'

const email = ref('')
const password = ref('')
const createdId = ref<string | null>(null)
const error = ref<string | null>(null)
const router = useRouter()
const auth = useAuthStore()

const register = async () => {
  error.value = null
  try {
    const r = await apiFetch('/Auth/register', {
      method: 'POST',
      body: JSON.stringify({ email: email.value, passwordHash: password.value })
    })
    const id = r?.userId ?? r?.UserId ?? r?.UserId ?? r
    createdId.value = id
    auth.setUserId(id)
    setUserIdSvc(id)
  } catch (ex: any) {
    error.value = ex.message
  }
}

const gotoCategories = () => router.push('/categories')
</script>
