<template>
  <router-link custom :to="to" v-slot="{ navigate, href }">
    <el-link
      :type="type ?? 'primary'"
      :href="href"
      :disabled="disabled"
      @click="navigate"
    >
      {{ text ?? $t('operation.edit') }}
    </el-link>
  </router-link>
</template>

<script setup lang="ts">
import type { RouteLocationRaw } from 'vue-router';

defineProps<{
  to: RouteLocationRaw;
  text?: string;
  type?: 'primary' | 'success' | 'warning' | 'danger' | 'info' | 'default';
  disabled?: boolean;
}>();
</script>
