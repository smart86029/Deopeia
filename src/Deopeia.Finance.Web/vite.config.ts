import { fileURLToPath, URL } from 'node:url';

import vue from '@vitejs/plugin-vue';
import externalGlobals from 'rollup-plugin-external-globals';
import AutoImport from 'unplugin-auto-import/vite';
import Components from 'unplugin-vue-components/vite';
import { defineConfig } from 'vite';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    AutoImport({
      imports: ['vue', 'vue-i18n', 'vue-router', 'pinia', '@vueuse/core'],
      dts: './src/auto-imports.d.ts',
    }),
    Components({
      dts: './src/components.d.ts',
    }),
    {
      ...externalGlobals({
        vue: 'Vue',
        'vue-demi': 'VueDemi',
        'vue-i18n': 'VueI18n',
        'vue-router': 'VueRouter',
        axios: 'axios',
        'element-plus': 'ElementPlus',
      }),
      enforce: 'post',
      apply: 'build',
    },
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5019/',
        changeOrigin: true,
      },
      '/hub': {
        target: 'https://localhost:7211/',
        changeOrigin: true,
        secure: false,
        ws: true,
      },
    },
  },
  build: {
    rollupOptions: {
      external: [
        'vue',
        'vue-demi',
        'vue-i18n',
        'vue-router',
        'axios',
        'element-plus',
      ],
    },
  },
});
