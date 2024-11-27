<template>
  <div>
    <h2>{{ $t('trading.newOrder') }}</h2>

    <el-form :model="form" label-position="top">
      <el-form-item>
        <el-radio-group v-model="radio1">
          <el-radio-button label="Market" value="Market" />
          <el-radio-button label="Limit" value="Limit" />
        </el-radio-group>
      </el-form-item>
      <el-form-item :label="$t('finance.price')">
        <template v-if="radio1 === 'Market'">
          {{ 100 }}
        </template>
        <InputNumber v-else v-model="form.amount" />
      </el-form-item>
      <el-form-item :label="$t('trading.volume')">
        <InputNumber v-model="form.amount" />
      </el-form-item>
      <el-form-item :label="$t('trading.leverage')">
        <el-radio-group v-model="form.leverage">
          <el-radio-button label="1x" :value="1" />
          <el-radio-button label="10x" :value="10" />
          <el-radio-button label="25x" :value="25" />
        </el-radio-group>
      </el-form-item>

      <div class="details">
        <div class="detail">
          <el-text type="info">{{ $t('trading.margin') }}</el-text>
          {{ margin }}
        </div>
        <div class="detail">
          <el-text type="info">{{ $t('trading.margin') }}</el-text>
          {{ margin }}
        </div>
      </div>

      <el-form-item :label="$t('trading.stopLoss')">
        <InputNumber v-model="form.stopLoss" />
      </el-form-item>
      <el-form-item :label="$t('trading.takeProfit')">
        <InputNumber v-model="form.takeProfit" />
      </el-form-item>

      <el-form-item>
        <el-button :type="positive" class="button-trade">
          {{ $t(`trading.orderSide.${OrderSide.Buy}`) }}
        </el-button>
        <el-button :type="negative" class="button-trade">
          {{ $t(`trading.orderSide.${OrderSide.Sell}`) }}
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { OrderSide } from '@/models/trading/order-side';
import { usePreferencesStore } from '@/stores/preferences';

const { positive, negative } = storeToRefs(usePreferencesStore());

const radio1 = ref('Market');

const form = reactive({
  currencyCode: '',
  amount: 0,
  leverage: 1,
  stopLoss: undefined,
  takeProfit: undefined,
});

const margin = computed(() => form.amount / form.leverage);
</script>

<style lang="scss" scoped>
.el-radio-group {
  width: 100%;
}

.el-radio-button {
  flex: 1;
}

:deep(.el-radio-button__inner) {
  width: 100%;
}

.details {
  display: flex;
  flex-direction: column;
  margin-bottom: 18px;

  .detail {
    display: flex;
    justify-content: space-between;
    flex: 1;
  }
}

.button-trade {
  flex: 1;
}
</style>
