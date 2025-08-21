<template>
    <div class="container">
      <div class="card">
        <h2 class="pb-4 text-3xl font-medium">Products</h2>
        <div v-if="!userId">
          <p>Please register (or paste a userId) to load products.</p>
        </div>
  
        <div v-if="userId">
          <div style="display:flex;gap:8px;align-items:center;margin-bottom:12px;">
            <InputText v-model="productSchema.name" placeholder="New product name" />
            <InputText v-model="productSchema.price" placeholder="New product price" />
            <Button label="Create" @click="handleCreate" />
          </div>
  
          <DataTable :value="productsStore.products" responsiveLayout="scroll">
            <Column field="id" header="Id" />
            <Column field="name" header="Name" />
            <Column field="price" header="price" />
          </DataTable>
        </div>
  
        <div v-if="productsStore.error" style="color:var(--p-error-color);margin-top:1rem">{{ productsStore.error }}</div>
      </div>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../stores/auth'
import { useProductsStore } from '../stores/ProductsStore'

const auth = useAuthStore()
const productsStore = useProductsStore()
const userId = auth.userId
const productSchema = ref({
  name: '',
  price: ''
})

const handleCreate = async () => {
  await productsStore.createProduct(userId!, productSchema.value)
  productSchema.value = { name: '', price: '' }
}

onMounted(() => {
  if (userId) {
    productsStore.fetchProducts(userId)
  }
})
</script>
