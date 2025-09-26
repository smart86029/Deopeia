import { createId, editId } from './props';

export default [
  {
    path: 'setting',
    name: 'setting.module',
    redirect: { name: 'setting.traders.list' },
    children: [
      {
        path: 'instrument',
        name: 'setting.instrument.list',
        redirect: { name: 'setting.instrument.default' },
        children: [
          {
            path: '',
            name: 'setting.instrument.default',
            component: () => import('../views/setting/InstrumentList.vue'),
          },
          {
            path: 'create',
            name: 'setting.instrument.create',
            component: () => import('../views/setting/InstrumentForm.vue'),
            props: createId,
          },
          {
            path: ':id',
            name: 'setting.instrument.edit',
            component: () => import('../views/setting/InstrumentForm.vue'),
            props: editId,
          },
        ],
      },
    ],
  },
];
