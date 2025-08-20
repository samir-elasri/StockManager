<template>
    <div class="container">
      <div class="card">
        <h2>Products</h2>
        <div v-if="!userId">
          <p>Please register (or paste a userId) to load products.</p>
        </div>
  
        <div v-if="userId">
          <div style="display:flex;gap:8px;align-items:center;margin-bottom:12px;">
            <InputText v-model="newName" placeholder="New product name" />
            <Button label="Create" @click="createProduct" />
          </div>
  
          <DataTable :value="products" responsiveLayout="scroll">
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
const products = ref<any[]>([])
const newName = ref('')
const error = ref<string | null>(null)
const userId = auth.userId

const load = async () => {
  if (!userId) return
  try {
    const r = await apiFetch(`/Products/${userId}`)
    products.value = r
  } catch (ex: any) {
    error.value = ex.message
  }
}

const createProduct = async () => {
  if (!userId) {
    error.value = 'You must register first.'
    return
  }
  try {
    await apiFetch('/Products', {
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
