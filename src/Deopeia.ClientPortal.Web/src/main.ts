import 'element-plus/dist/index.css';
import 'element-plus/theme-chalk/dark/css-vars.css';
import './assets/main.css';

import ElementPlus from 'element-plus';
import { createPinia } from 'pinia';
import { createApp } from 'vue';

import App from './App.vue';
import i18n from './plugins/i18n';
import router from './router';

const app = createApp(App);

app.use(createPinia());
app.use(ElementPlus);
app.use(router);
app.use(i18n);

app.mount('#app');
