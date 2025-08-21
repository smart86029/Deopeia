<template>
  <router-link
    custom
    :to="{ name: 'home', params: { locale: locale.key } }"
    #default="{ navigate }"
  >
    <h1 @click="navigate">
      {{ $t('common.title') }}
    </h1>
  </router-link>
</template>

<script setup lang="ts">
import { usePreferencesStore } from '@/stores/preferences';

const { locale } = storeToRefs(usePreferencesStore());
</script>

<style lang="scss" scoped>
h1 {
  cursor: pointer;
}
</style>
