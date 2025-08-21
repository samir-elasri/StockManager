import { defineStore } from 'pinia'
import { ref } from 'vue'
import { apiFetch } from '../services/api'

export const useProductsStore = defineStore('products', () => {
  const products = ref<any[]>([])
  const error = ref<string | null>(null)

  const fetchProducts = async (userId: string) => {
    if (!userId) return
    try {
      products.value = await apiFetch(`/Products/${userId}`)
      error.value = null
    } catch (ex: any) {
      error.value = ex.message
    }
  }

  const createProduct = async (userId: string, product: { name: string, price: string }) => {
    if (!userId) {
      error.value = 'You must register first.'
      return
    }
    if (!product.name || !product.price) {
      error.value = 'Product name and price are required'
      return
    }
    try {
      await apiFetch('/Products', {
        method: 'POST',
        body: JSON.stringify({ ...product, userId })
      })
      error.value = null
      await fetchProducts(userId)
    } catch (ex: any) {
      error.value = ex.message
    }
  }

  return {
    products,
    error,
    fetchProducts,
    createProduct
  }
})
