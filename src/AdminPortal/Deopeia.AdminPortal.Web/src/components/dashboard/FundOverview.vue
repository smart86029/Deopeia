<template>
  <div class="fund-list">
    <el-card v-for="fund in funds" :key="fund.currencyCode">
      <el-avatar>{{ fund.currencyCode }}</el-avatar>
      <el-text>{{ fund.name }}</el-text>
      <div class="money">
        <section>{{ $n(fund.marginUsed, 'decimal') }}</section>
        <section>{{ $n(fund.balance, 'decimal') }}</section>
      </div>
      <el-text class="risk">
        {{ $n(fund.marginUsed / fund.balance, 'percent') }}
      </el-text>
    </el-card>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  funds: {
    currencyCode: string;
    name: string;
    marginUsed: number;
    balance: number;
  }[];
}>();
</script>

<style scoped lang="scss">
.fund-list {
  display: flex;
  gap: 16px;
}

.el-card {
  flex: auto;

  :deep(.el-card__body) {
    display: flex;
    gap: 8px;
  }

  $names: (
    'danger': 1,
    'warning': 2,
    'success': 3,
    'primary': 4,
  );
  @each $name, $index in $names {
    &:nth-child(#{$index}) {
      background-color: var(--el-color-#{$name}-light-5);

      .el-avatar {
        background-color: var(--el-color-#{$name});
      }
    }
  }
}

.el-avatar {
  font-size: var(--el-font-size-medium);
}

.el-text {
  text-align: right;
  font-size: 1.5em;
}

.money {
  flex: 3;
  text-align: right;
}

.risk {
  font-weight: bold;
}
</style>
