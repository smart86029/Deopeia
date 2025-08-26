import 'element-plus/dist/index.css';
import 'element-plus/theme-chalk/dark/css-vars.css';
import './assets/main.css';

import { VueQueryPlugin } from '@tanstack/vue-query';
import ElementPlus from 'element-plus';

import App from './App.vue';
import i18n from './plugins/i18n';
import { vueQueryPluginOptions } from './plugins/vue-query';
import router from './router';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(i18n);
app.use(VueQueryPlugin, vueQueryPluginOptions);
app.use(ElementPlus);

app.mount('#app');
