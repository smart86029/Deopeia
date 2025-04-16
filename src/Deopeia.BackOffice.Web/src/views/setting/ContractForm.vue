<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-tabs v-model="tab" type="card">
      <el-tab-pane label="common" name="common">
        <el-form-item :label="$t('finance.symbol')">
          <el-input v-if="action === 'create'" v-model="form.symbol" />
          <template v-else>{{ form.symbol }}</template>
        </el-form-item>

        <LocaleTabs v-model:locales="form.locales" :add="add">
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

        <el-form-item :label="$t('finance.underlyingType.name')">
          <SelectEnum
            v-model="form.underlyingType"
            :enum="UnderlyingType"
            localeKey="finance.underlyingType"
          />
        </el-form-item>
        <el-form-item :label="$t('common.currency')">
          <SelectOption v-model="form.currencyCode" :options="currencies" />
        </el-form-item>
        <el-form-item :label="$t('finance.minimumPriceFluctuation')">
          <InputNumber v-model="form.tickSize" />
        </el-form-item>
        <el-form-item :label="$t('finance.contractSize')">
          <el-input v-model="form.contractSizeQuantity">
            <template #append>
              <SelectOption
                v-model="form.contractSizeUnitCode"
                :options="units"
                :clearable="false"
              />
            </template>
          </el-input>
        </el-form-item>
        <el-form-item :label="$t('trading.leverage')">
          <el-input-tag v-model="form.leverages" tag-type="primary">
            <template #tag="{ value }">{{ value }}X</template>
          </el-input-tag>
        </el-form-item>
      </el-tab-pane>

      <el-tab-pane label="sessions" name="session">
        <el-form-item :label="$t('trading.session')">
          <TableSession v-model="form.sessions" />
        </el-form-item>
      </el-tab-pane>
    </el-tabs>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import {
  contractApi,
  type Contract,
  type ContractLocale,
} from '@/api/trading/contract-api';
import { UnderlyingType } from '@/models/underlying-type';
import { success } from '@/plugins/element';
import { useOptionStore } from '@/stores/option';

const props = defineProps<{
  action: 'create' | 'edit';
  symbol: string;
}>();

const tab = ref('common');
const loading = ref(false);
const { currencies, units } = storeToRefs(useOptionStore());
const form: Contract = reactive({
  symbol: '',
  underlyingType: 0,
  currencyCode: '',
  pricePrecision: 0,
  tickSize: 1,
  contractSizeQuantity: 1,
  contractSizeUnitCode: '',
  leverages: [],
  sessions: [],
  locales: [{ culture: 'en', name: '', description: '' }],
});

if (props.action === 'edit') {
  contractApi
    .get(props.symbol)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const add = (culture: string): ContractLocale => ({
  culture: culture,
  name: '',
  description: '',
});

const save = () => {
  loading.value = true;
  const post =
    props.action === 'create' ? contractApi.create : contractApi.update;
  post(form)
    .then(() => success(props.action))
    .finally(() => (loading.value = false));
};

watch(form, (form) => {
  if (form.underlyingType === UnderlyingType.Stock) {
    form.contractSizeUnitCode = 'Shares';
  } else if (form.underlyingType === UnderlyingType.Index) {
    form.contractSizeUnitCode = 'Points';
  }
});
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
