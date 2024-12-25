<template>
  <div class="trading-info">
    <el-card>
      <template #header>Trading Rules</template>
      <TradingRuleList />
    </el-card>
    <el-card>
      <template #header>Trading Sessions</template>
      <TradingSessionList />
    </el-card>
  </div>
</template>

<style lang="scss" scoped>
.trading-info {
  display: flex;
  gap: 16px;
}

.el-card {
  flex: 1;
}
</style>
