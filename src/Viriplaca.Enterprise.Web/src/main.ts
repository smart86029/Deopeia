import './assets/main.css';
import 'element-plus/dist/index.css';
import 'element-plus/theme-chalk/dark/css-vars.css';

import { createApp } from 'vue';
import { createPinia } from 'pinia';
import ElementPlus from 'element-plus';

import App from './App.vue';
import router from './router';
import i18n from './plugins/i18n';

const app = createApp(App);

app.use(createPinia());
app.use(ElementPlus);
app.use(router);
app.use(i18n);

app.mount('#app');
