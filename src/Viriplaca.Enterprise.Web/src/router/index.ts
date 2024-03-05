import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import leave from './leave';
import organization from './organization';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      children: [...leave, ...organization],
    },
  ],
});

export default router;
