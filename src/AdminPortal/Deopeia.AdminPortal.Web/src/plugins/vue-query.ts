import { type VueQueryPluginOptions, keepPreviousData } from '@tanstack/vue-query';

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
