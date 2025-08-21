<template>
    <div class="container">
      <div class="card" style="max-width:420px;margin:1rem auto;">
        <h2 class="font-medium pb-2 border-b">Register / Sign up</h2>
        <div class="p-fluid pt-4">
          <label for="email">Email</label>
          <InputText id="email" v-model="email" />
          <label for="password">Password</label>
          <InputText id="password" type="password" v-model="password" />
          <Button label="Register" @click="handleRegister" style="margin-top: 1em" class="!mt-4" />
        </div>
        <div v-if="auth.userId" class="mt-5">
          <b>Welcome back user:</b> {{ auth.userId }}
          <div class="mt-2">
            <Button label="Go to Products" @click="gotoProducts" />
          </div>
        </div>
        <div v-if="auth.error" style="color:var(--p-error-color);margin-top:1rem">{{ auth.error }}</div>
      </div>
    </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../stores/auth'

const email = ref('')
const password = ref('')
const router = useRouter()
const auth = useAuthStore()

const handleRegister = async () => {
  try {
    await auth.register(email.value, password.value)
  } catch {
    console.error('Registration failed')
  }
}

const gotoProducts = () => router.push('/Products')

onMounted(() => {
  if (auth.userId) {
    console.log('User already created:', auth.userId)
  }
})
</script>
