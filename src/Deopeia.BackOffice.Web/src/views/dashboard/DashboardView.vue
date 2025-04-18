<template>
  <div class="flex">
    <div class="statistic-card">
      <el-statistic :value="deposit" :title="$t('fund.deposit')" />
    </div>
    <div class="statistic-card">
      <el-statistic :value="withdrawal" :title="$t('fund.withdrawal')" />
    </div>
  </div>
</template>

<script setup lang="ts">
const deposit = 10000;
const withdrawal = 10000;
</script>

<style lang="scss" scoped>
.flex {
  display: flex;
  gap: 16px;
  margin-top: 16px;
}

.el-statistic {
  --el-statistic-content-font-size: 2em;
}

.statistic-card {
  padding: 20px;
  border-radius: var(--el-border-radius-base);
  background-color: var(--el-bg-color-overlay);
  text-align: center;
  flex: 1;
}
</style>
