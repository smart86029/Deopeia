<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
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
          <SelectOption v-model="form.contractSizeUnitCode" :options="units" />
        </template>
      </el-input>
    </el-form-item>
    <el-form-item :label="$t('trading.leverage')">
      <el-input-tag v-model="form.leverages" tag-type="primary">
        <template #tag="{ value }">{{ value }}X</template>
      </el-input-tag>
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import optionApi from '@/api/option-api';
import contractApi, {
  type Contract,
  type ContractLocale,
} from '@/api/trading/contract-api';
import type { OptionResult } from '@/models/option-result';
import { UnderlyingType } from '@/models/underlying-type';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  symbol: string;
}>();
const loading = ref(false);
const currencies: Ref<OptionResult<string>[]> = ref([]);
const units: Ref<OptionResult<string>[]> = ref([]);
const form: Contract = reactive({
  symbol: '',
  underlyingType: 0,
  currencyCode: '',
  tickSize: 1,
  contractSizeQuantity: 1,
  contractSizeUnitCode: '',
  leverages: [],
  locales: [{ culture: 'en', name: '', description: '' }],
});

optionApi.getCurrencies().then((x) => (currencies.value = x.data));
optionApi.getUnits().then((x) => (units.value = x.data));

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
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
