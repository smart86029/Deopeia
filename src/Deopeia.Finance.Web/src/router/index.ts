import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import auth from './auth';
import { setLocale } from './i18n';
import identity from './identity';
import market from './market';
import quote from './quote';
import setting from './setting';
import trading from './trading';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/:locale?',
      name: 'home',
      beforeEnter: setLocale,
      component: HomeView,
      redirect: { name: 'dashboard.default' },
      children: [
        {
          path: '',
          name: 'dashboard.default',
          component: () => import('../views/dashboard/DashboardView.vue'),
        },
        ...auth,
        ...identity,
        ...market,
        ...quote,
        ...setting,
        ...trading,
      ],
    },
  ],
});

export default router;
