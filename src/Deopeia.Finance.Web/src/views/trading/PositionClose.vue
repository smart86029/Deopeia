<template>
  <div class="position-close">
    <el-form :model="form" label-width="200" @submit.prevent="save">
      <el-form-item :label="$t('trading.accountNumber')">
        {{ form.accountNumber }}
      </el-form-item>
      <el-form-item :label="$t('trading.positionType.name')">
        {{ $t(`trading.positionType.${form.type}`) }}
      </el-form-item>
      <el-form-item :label="$t('common.name')">
        {{ form.name }}
      </el-form-item>
      <el-form-item :label="$t('trading.orderType.name')">
        <RadioEnum
          v-model="form.orderType"
          :enum="OrderType"
          locale-key="trading.orderType"
        />
      </el-form-item>
      <el-form-item :label="$t('finance.price')">
        <template v-if="form.orderType === OrderType.Market">
          {{ $n(price, 'decimal') }}
        </template>
        <el-input v-else v-model="form.price" />
      </el-form-item>
      <el-form-item :label="$t('trading.volume')">
        <el-input v-model="form.volume" />
      </el-form-item>

      <el-form-item>
        <ButtonBack />
        <ButtonSave :loading="loading" />
      </el-form-item>
    </el-form>

    <OrderBook :bids="bids" :asks="asks" :price="price" />
  </div>
</template>

<script setup lang="ts">
import optionApi from '@/api/option-api';
import positionApi, { type Position } from '@/api/trading/position-api';
import { emptyGuid, type Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { OrderType } from '@/models/trading/order-type';
import { useQuoteStore } from '@/stores/quote';

const props = defineProps<{
  id: Guid;
}>();
const loading = ref(false);
const currencies: Ref<OptionResult<string>[]> = ref([]);
const form: Position = reactive({
  id: emptyGuid,
  accountNumber: '',
  type: 0,
  name: '',
  orderType: OrderType.Market,
  price: 0,
  volume: 0,
});
const { quotes, bids, asks } = storeToRefs(useQuoteStore());
const price = computed(() => quotes.value.get('GCZ2024').value);

optionApi.getCurrencies().then((x) => (currencies.value = x.data));

positionApi
  .get(props.id)
  .then((x) => Object.assign(form, x.data))
  .finally(() => (loading.value = false));

const save = () => {
  // loading.value = true;
  // post(form)
  //   .then(() => success('Close'))
  //   .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.position-close {
  display: flex;
  direction: row;
  gap: 32px;
}

.el-form {
  flex: 1 1 60%;
}

.order-book {
  flex: 1 1 60%;
}
</style>
