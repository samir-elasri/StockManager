<template>
    <div class="container">
      <div class="card">
        <h2>Categories</h2>
        <div v-if="!userId">
          <p>Please register (or paste a userId) to load categories.</p>
        </div>
  
        <div v-if="userId">
          <div style="display:flex;gap:8px;align-items:center;margin-bottom:12px;">
            <InputText v-model="newName" placeholder="New category name" />
            <Button label="Create" @click="createCategory" />
          </div>
  
          <DataTable :value="categories" responsiveLayout="scroll">
            <Column field="id" header="Id" />
            <Column field="name" header="Name" />
          </DataTable>
        </div>
  
        <div v-if="error" style="color:var(--p-error-color);margin-top:1rem">{{ error }}</div>
      </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../stores/auth'
import { apiFetch } from '../services/api'

const auth = useAuthStore()
const categories = ref<any[]>([])
const newName = ref('')
const error = ref<string | null>(null)
const userId = auth.userId

const load = async () => {
  if (!userId) return
  try {
    const r = await apiFetch(`/Categories/${userId}`)
    categories.value = r
  } catch (ex: any) {
    error.value = ex.message
  }
}

const createCategory = async () => {
  if (!userId) {
    error.value = 'You must register first.'
    return
  }
  try {
    await apiFetch('/Categories', {
      method: 'POST',
      body: JSON.stringify({ name: newName.value, userId })
    })
    newName.value = ''
    await load()
  } catch (ex: any) {
    error.value = ex.message
  }
}

onMounted(load)
</script>
