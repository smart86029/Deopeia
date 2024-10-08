<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('finance.symbol')">
      <el-input v-model="form.symbol" />
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
      </LocaleTabPane>
    </LocaleTabs>

    <el-form-item :label="$t('finance.exchange')">
      <SelectOption v-model="form.exchangeId" :options="exchanges" />
    </el-form-item>
    <el-form-item :label="$t('finance.underlyingAsset')">
      <SelectOption v-model="form.underlyingAssetId" :options="assets" />
    </el-form-item>
    <el-form-item :label="$t('common.currency')">
      <SelectOption v-model="form.currencyCode" :options="currencies" />
    </el-form-item>
    <el-form-item :label="$t('finance.tickSize')">
      <el-input v-model="form.tickSize" />
    </el-form-item>
    <el-form-item :label="$t('finance.contractSize')">
      <el-input v-model="form.contractSizeQuantity">
        <template #append>
          <SelectOption v-model="form.currencyCode" :options="currencies" />
        </template>
      </el-input>
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import optionApi from '@/api/option-api';
import assetApi from '@/api/quote/asset-api';
import exchangeApi from '@/api/quote/exchange-api';
import futuresApi, {
  type Futures,
  type FuturesLocale,
} from '@/api/quote/futures-api';
import { emptyGuid, type Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();
const loading = ref(false);
const assets: Ref<OptionResult<Guid>[]> = ref([]);
const exchanges: Ref<OptionResult<string>[]> = ref([]);
const currencies: Ref<OptionResult<string>[]> = ref([]);
const form: Futures = reactive({
  id: emptyGuid,
  symbol: '',
  exchangeId: '',
  underlyingAssetId: emptyGuid,
  currencyCode: '',
  tickSize: 1,
  contractSizeQuantity: 1,
  contractSizeUnitCode: '',
  locales: [{ culture: 'en', name: '' }],
});

assetApi.getOptions().then((x) => (assets.value = x.data));
exchangeApi.getOptions().then((x) => (exchanges.value = x.data));
optionApi.getCurrencies().then((x) => (currencies.value = x.data));

if (props.action === 'edit') {
  futuresApi
    .get(props.id)
    .then((x) => Object.assign(form, x.data))
    .finally(() => (loading.value = false));
}

const add = (culture: string): FuturesLocale => ({
  culture: culture,
  name: '',
});

const save = () => {
  loading.value = true;
  const post =
    props.action === 'create' ? futuresApi.create : futuresApi.update;
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
