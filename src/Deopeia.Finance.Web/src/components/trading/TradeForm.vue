<template>
  <div>
    <h2>{{ $t('trading.newOrder') }}</h2>

    <el-form :model="form" label-position="top" @submit.prevent>
      <el-form-item>
        <el-radio-group v-model="type">
          <el-radio-button label="Market" value="Market" />
          <el-radio-button label="Limit" value="Limit" />
        </el-radio-group>
      </el-form-item>
      <el-form-item :label="$t('finance.price')">
        <template v-if="type === 'Market'">
          {{ lastTradedPrice }}
        </template>
        <InputNumber v-else v-model="form.price" />
      </el-form-item>
      <el-form-item :label="$t('trading.volume')">
        <InputNumber v-model="form.volume" />
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
        <InputNumber v-model="form.stopLossPrice" />
      </el-form-item>
      <el-form-item :label="$t('trading.takeProfit')">
        <InputNumber v-model="form.takeProfitPrice" />
      </el-form-item>

      <el-form-item>
        <el-button :type="positive" class="button-trade" @click="buy">
          {{ $t(`trading.orderSide.${OrderSide.Buy}`) }}
        </el-button>
        <el-button :type="negative" class="button-trade" @click="sell">
          {{ $t(`trading.orderSide.${OrderSide.Sell}`) }}
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script setup lang="ts">
import orderApi, { type Order } from '@/api/trading/order-api';
import { emptyGuid } from '@/models/guid';
import { OrderSide } from '@/models/trading/order-side';
import { usePreferencesStore } from '@/stores/preferences';
import { useQuoteStore } from '@/stores/quote';
import { ElMessage } from 'element-plus';

const props = defineProps<{
  price?: number;
}>();

const { positive, negative } = storeToRefs(usePreferencesStore());
const { lastTradedPrice } = storeToRefs(useQuoteStore());

const type = ref('Market');
const { t } = useI18n();
const loading = ref(false);
const form: Order = reactive({
  side: OrderSide.Buy,
  symbol: '',
  volume: 0,
  currencyCode: '',
  leverage: 1,
  price: undefined,
  stopLossPrice: undefined,
  takeProfitPrice: undefined,
  accountId: emptyGuid,
});

const margin = computed(() => form.volume / form.leverage);

watch(
  () => props.price,
  (price) => {
    if (type.value === 'Limit' && price) {
      form.price = price;
    }
  },
);

const buy = () => {
  form.side = OrderSide.Buy;
  save();
};

const sell = () => {
  form.side = OrderSide.Sell;
  save();
};

const save = () => {
  loading.value = true;
  orderApi
    .create(form)
    .then(() =>
      ElMessage.success({
        message: t('common.message.createSuccess'),
      }),
    )
    .finally(() => (loading.value = false));
};
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
