import { keepPreviousData, type VueQueryPluginOptions } from '@tanstack/vue-query';

const vueQueryPluginOptions: VueQueryPluginOptions = {
  queryClientConfig: {
    defaultOptions: {
      queries: {
        placeholderData: keepPreviousData,
      },
    },
  },
};

export { vueQueryPluginOptions };
