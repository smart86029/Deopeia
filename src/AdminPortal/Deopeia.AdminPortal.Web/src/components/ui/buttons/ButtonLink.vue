<template>
  <router-link custom :to="to" v-slot="{ navigate }">
    <el-button v-bind="$attrs" :type="type ?? 'primary'" link @click="navigate">
      {{ text ?? $t('action.edit') }}
    </el-button>
  </router-link>
</template>

<script setup lang="ts">
import type { RouteLocationRaw } from 'vue-router';

defineProps<{
  to: RouteLocationRaw;
  text?: string;
  type?: 'primary' | 'success' | 'warning' | 'danger' | 'info' | 'default';
}>();
</script>
