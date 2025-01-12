<template>
  <el-table table-layout="auto">
    <el-table-column :label="$t('finance.symbol')">
      <template #default="{ row }">
        <TextLink
          :to="{ name: 'trading.view', params: { symbol: row.symbol } }"
          :text="row.symbol"
        />
      </template>
    </el-table-column>
    <el-table-column prop="name" :label="$t('common.name')" />
    <el-table-column :label="$t('finance.price')" align="right">
      <template #default="{ row }">
        <TextPrice :value="getPrice(row.symbol)" :comparison />
      </template>
    </el-table-column>
    <el-table-column :label="$t('finance.priceChange')" align="right">
      <template #default="{ row }">
        <TextPrice :value="getPriceChange(row.symbol)" percentage />
      </template>
    </el-table-column>
    <el-table-column prop="volume" :label="$t('finance.volume')" align="right">
      <template #default="{ row }">
        {{ $n(getVolume(row.symbol), 'decimal') }}
      </template>
    </el-table-column>
    <el-table-column :label="$t('route.trading')" align="right">
      <template #default="{ row }">
        <el-button :type="positive" @click="$router.push({ name: 'trading' })">
          {{ $t(`trading.orderSide.${OrderSide.Buy}`) }}
          {{ $n(getBid(row.symbol), 'decimal') }}
        </el-button>
        <el-button :type="negative" @click="$router.push({ name: 'trading' })">
          {{ $t(`trading.orderSide.${OrderSide.Sell}`) }}
          {{ $n(getAsk(row.symbol), 'decimal') }}
        </el-button>
      </template>
    </el-table-column>
  </el-table>
</template>

<script setup lang="ts">
import { OrderSide } from '@/models/trading/order-side';
import { usePreferencesStore } from '@/stores/preferences';
import { useQuoteStore } from '@/stores/quote';

const { positive, negative } = storeToRefs(usePreferencesStore());
const { ticks } = storeToRefs(useQuoteStore());

const comparison = 90;
const previusClose = 90;

const getPrice = (symbol: string) => ticks.value.get(symbol)?.price || 0;

const getPriceChange = (symbol: string) =>
  ((ticks.value.get(symbol)?.price || previusClose) - previusClose) /
  previusClose;

const getVolume = (symbol: string) => ticks.value.get(symbol)?.volume || 0;

const getBid = (symbol: string) => ticks.value.get(symbol)?.bid || 0;

const getAsk = (symbol: string) => ticks.value.get(symbol)?.ask || 0;
</script>
