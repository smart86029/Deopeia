import { fileURLToPath, URL } from 'node:url';

import vue from '@vitejs/plugin-vue';
import externalGlobals from 'rollup-plugin-external-globals';
import AutoImport from 'unplugin-auto-import/vite';
import Components from 'unplugin-vue-components/vite';
import { defineConfig } from 'vite';
import { createHtmlPlugin } from 'vite-plugin-html';
import vueDevTools from 'vite-plugin-vue-devtools';
import packageJson from './package.json' with { type: 'json' };

const getVersion = (
  name: 'vue' | 'vue-i18n' | 'vue-router' | 'axios' | 'element-plus' | 'pinia',
) => packageJson.dependencies[name].replace(/^[^0-9]*/, '');

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools(),
    AutoImport({
      imports: ['vue', 'vue-i18n', 'vue-router', 'pinia', '@vueuse/core'],
      dts: './src/auto-imports.d.ts',
    }),
    Components({
      dts: './src/components.d.ts',
    }),
    createHtmlPlugin({
      inject: {
        data: {
          css: [
            `//cdn.jsdelivr.net/npm/element-plus@${getVersion('element-plus')}/dist/index.min.css`,
          ],
          js: [
            `//cdn.jsdelivr.net/npm/vue@${getVersion('vue')}/dist/vue.global.prod.js`,
            '//cdn.jsdelivr.net/npm/vue-demi@0.14.10/lib/index.iife.min.js',
            `//cdn.jsdelivr.net/npm/vue-i18n@${getVersion('vue-i18n')}/dist/vue-i18n.global.prod.js`,
            `//cdn.jsdelivr.net/npm/vue-router@${getVersion('vue-router')}/dist/vue-router.global.min.js`,
            `//cdn.jsdelivr.net/npm/axios@${getVersion('axios')}/dist/axios.min.js`,
            `//cdn.jsdelivr.net/npm/element-plus@${getVersion('element-plus')}/dist/index.full.min.js`,
            `//cdn.jsdelivr.net/npm/pinia@${getVersion('pinia')}/dist/pinia.iife.prod.js`,
          ],
        },
      },
    }),
    {
      ...externalGlobals({
        vue: 'Vue',
        'vue-demi': 'VueDemi',
        'vue-i18n': 'VueI18n',
        'vue-router': 'VueRouter',
        axios: 'axios',
        'element-plus': 'ElementPlus',
        pinia: 'Pinia',
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
        target: 'https://localhost:7160/',
        changeOrigin: true,
        secure: false,
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
  css: {
    preprocessorOptions: {
      scss: {
        api: 'modern-compiler',
      },
    },
  },
});
