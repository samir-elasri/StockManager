import {fileURLToPath, URL} from 'node:url'
import {defineConfig} from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'
import Components from 'unplugin-vue-components/vite'

// Using async IIFE to handle dynamic imports
export default defineConfig(async () => {
  const {PrimeVueResolver} = await import('@primevue/auto-import-resolver')

  return {
    plugins: [
      vue(),
      vueDevTools({
        componentInspector: false
      }),
      Components({
        resolvers: [PrimeVueResolver()],
        dirs: ['src/components', 'src/modules/**/components'],
        deep: true
      })
    ],
    resolve: {
      alias: {
        '@': fileURLToPath(new URL('./src', import.meta.url))
      }
    },
    server: {
      host: true,
      port: 5173
    }
  }
})
