<template>
  <IconCurrencyCny v-if="currencyCode == 'CNY'" />
  <IconCurrencyEur v-else-if="currencyCode == 'EUR'" />
  <IconCurrencyGbp v-else-if="currencyCode == 'GBP'" />
  <IconCurrencyJpy v-else-if="currencyCode == 'JPY'" />
  <IconCurrencyUsd v-else-if="currencyCode == 'USD'" />
</template>

<script setup lang="ts">
const model = defineModel<string>();

const currencyCode = computed(() => model.value?.toUpperCase());
</script>
