import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import auth from './auth';
import exchange from './exchange';
import { setLocale } from './i18n';
import screener from './screener';
import symbol from './symbol';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/:locale?',
      name: 'home',
      beforeEnter: setLocale,
      component: HomeView,
      children: [...auth, ...exchange, ...screener, ...symbol],
    },
  ],
});

export default router;
