<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('finance.symbol')">
      {{ form.symbol }}
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

    <el-form-item :label="$t('finance.expirationDate')">
      <DatePicker v-model="form.expirationDate" />
    </el-form-item>

    <el-form-item :label="$t('finance.exchange')">
      {{ form.exchange }}
    </el-form-item>
    <el-form-item :label="$t('finance.underlyingAsset')">
      {{ form.underlyingAsset }}
    </el-form-item>
    <el-form-item :label="$t('common.currency')">
      {{ form.currency }}
    </el-form-item>
    <el-form-item :label="$t('finance.minimumPriceFluctuation')">
      {{ form.tickSize.toLocaleString() }}
    </el-form-item>
    <el-form-item :label="$t('finance.contractSize')">
      {{ form.contractSizeQuantity.toLocaleString() }}
      {{ form.contractSizeUnit }}
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import futuresContractApi, {
  type FuturesContract,
  type FuturesContractLocale,
} from '@/api/quote/futures-contract-api';
import { emptyGuid, type Guid } from '@/models/guid';
import { success } from '@/plugins/element';

const props = defineProps<{
  id: Guid;
}>();
const loading = ref(false);
const form: FuturesContract = reactive({
  id: emptyGuid,
  symbol: '',
  expirationDate: new Date(),
  exchange: '',
  underlyingAsset: '',
  currency: '',
  tickSize: 1,
  contractSizeQuantity: 1,
  contractSizeUnit: '',
  locales: [{ culture: 'en', name: '' }],
});

futuresContractApi
  .get(props.id)
  .then((x) => Object.assign(form, x.data))
  .finally(() => (loading.value = false));

const add = (culture: string): FuturesContractLocale => ({
  culture: culture,
  name: '',
});

const save = () => {
  loading.value = true;
  futuresContractApi
    .update(form)
    .then(() => success('edit'))
    .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
