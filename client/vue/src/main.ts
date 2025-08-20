import { createApp } from 'vue'
import { createPinia } from 'pinia'
import {definePreset} from '@primevue/themes'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'
import 'primeicons/primeicons.css'
import App from './App.vue'
import router from './router'
import axiosInstance from './plugins/axios'
import './assets/main.css'

const app = createApp(App)
const pinia = createPinia()

const StockManagerPreset = definePreset(Aura, {
    semantic: {
        primary: {
            50: '{blue.50}',
            100: '{blue.100}',
            200: '{blue.200}',
            300: '{blue.300}',
            400: '{blue.400}',
            500: '{blue.500}',
            600: '{blue.600}',
            700: '{blue.700}',
            800: '{blue.800}',
            900: '{blue.900}',
            950: '{blue.950}'
        }
    }
})

app.use(pinia)
app.use(router)
app.use(PrimeVue, {
    theme: {
        preset: StockManagerPreset,
        options: {
            darkModeSelector: 'false',
            cssLayer: {
                name: 'primevue',
                order: 'tailwind-base, primevue, tailwind-utilities'
            }
        }
    }
})
app.provide('axios', axiosInstance)

app.mount('#app')
