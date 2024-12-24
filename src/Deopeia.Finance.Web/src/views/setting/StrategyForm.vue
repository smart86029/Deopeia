<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <LocaleTabs v-model:locales="form.locales" :add="addLocale">
      <LocaleTabPane
        v-for="locale in form.locales"
        :locale="locale"
        :key="locale.culture"
      >
        <el-form-item :label="$t('common.name')">
          <el-input v-model="locale.name" />
        </el-form-item>
        <el-form-item :label="$t('common.description')">
          <el-input v-model="locale.description" type="textarea" />
        </el-form-item>
      </LocaleTabPane>
    </LocaleTabs>

    <el-form-item :label="$t('finance.underlyingAsset')">
      <SelectOption v-model="assetId" :options="assets" />
    </el-form-item>
    <el-form-item :label="$t('status.isEnabled.name')">
      <el-switch v-model="form.isEnabled" />
    </el-form-item>
    <el-form-item :label="$t('trading.openExpression')">
      <el-input v-model="form.openExpression" />
    </el-form-item>
    <el-form-item :label="$t('trading.closeExpression')">
      <el-input v-model="form.closeExpression" />
    </el-form-item>

    <el-form-item label-width="0">
      <el-table :data="form.legs" table-layout="auto">
        <el-table-column prop="serialNumber" align="right" width="200">
          <template #default="{ row }">
            {{ $t('trading.order') }} {{ row.serialNumber }}
          </template>
        </el-table-column>
        <el-table-column :label="$t('trading.orderSide.name')">
          <template #default="{ row }">
            <RadioEnum
              v-model="row.side"
              :enum="OrderSide"
              locale-key="trading.orderSide"
            />
          </template>
        </el-table-column>
        <el-table-column :label="$t('finance.contract')">
          <template #default="{ row }">
            <SelectOption v-model="row.instrumentId" :options="futures" />
          </template>
        </el-table-column>
        <el-table-column :label="$t('trading.ticks')">
          <template #default="{ row }">
            <el-input-number v-model="row.ticks" />
          </template>
        </el-table-column>
        <el-table-column :label="$t('trading.volume')">
          <template #default="{ row }">
            <el-input-number v-model="row.volume" :min="1" />
          </template>
        </el-table-column>
        <el-table-column align="right">
          <template #header>
            <el-button :icon="Plus" circle @click="addLeg" />
          </template>
          <template #default="{ row }">
            <el-button
              v-if="row.serialNumber > 2"
              type="danger"
              :icon="Minus"
              circle
            />
          </template>
        </el-table-column>
      </el-table>
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import strategyApi, {
  type Strategy,
  type StrategyLocale,
} from '@/api/trading/strategy-api';
import { emptyGuid, type Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { OrderSide } from '@/models/trading/order-side';
import { success } from '@/plugins/element';
import { Minus, Plus } from '@element-plus/icons-vue';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const assetId = ref(emptyGuid);
const assets: Ref<OptionResult<Guid>[]> = ref([]);
const futures: Ref<OptionResult<Guid>[]> = ref([]);
const form: Strategy = reactive({
  id: emptyGuid,
  isEnabled: true,
  openExpression: '',
  closeExpression: '',
  locales: [{ culture: 'en', name: '' }],
  legs: [
    {
      serialNumber: 1,
      side: OrderSide.Buy,
      instrumentId: emptyGuid,
      ticks: 0,
      volume: 1,
    },
    {
      serialNumber: 2,
      side: OrderSide.Sell,
      instrumentId: emptyGuid,
      ticks: 0,
      volume: 1,
    },
  ],
});

watch(assetId, () => {
  form.legs.forEach((x) => (x.instrumentId = emptyGuid));
});

if (props.action === 'edit') {
  strategyApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const addLocale = (culture: string): StrategyLocale => ({
  culture: culture,
  name: '',
});

const addLeg = (): void => {
  const serialNumber = form.legs.length + 1;
  form.legs.push({
    serialNumber: serialNumber,
    side: OrderSide.Buy,
    instrumentId: emptyGuid,
    ticks: 0,
    volume: 1,
  });
};

const save = () => {
  loading.value = true;
  const post =
    props.action === 'create' ? strategyApi.create : strategyApi.update;
  post(form)
    .then(() => success(props.action))
    .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
