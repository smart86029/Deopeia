<template>
  <el-form :model="form" label-width="200" @submit.prevent="save">
    <el-form-item :label="$t('finance.underlyingAsset')">
      <SelectOption v-model="underlyingAssetId" :options="assets" />
    </el-form-item>
    <el-form-item :label="$t('finance.contractSpecification')">
      <SelectOption
        v-model="form.contractSpecificationId"
        :options="contractSpecifications"
      />
    </el-form-item>
    <el-form-item :label="$t('finance.expirationDate')">
      <DatePicker v-model="form.expirationDate" />
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
import contractSpecificationApi from '@/api/quote/contract-specification-api';
import exchangeApi from '@/api/quote/exchange-api';
import futuresContractApi, {
  type CreateFuturesContractCommand,
} from '@/api/quote/futures-contract-api';
import { emptyGuid, type Guid } from '@/models/guid';
import type { OptionResult } from '@/models/option-result';
import { success } from '@/plugins/element';

const loading = ref(false);
const assets: Ref<OptionResult<Guid>[]> = ref([]);
const exchanges: Ref<OptionResult<string>[]> = ref([]);
const contractSpecifications: Ref<OptionResult<Guid>[]> = ref([]);
const currencies: Ref<OptionResult<string>[]> = ref([]);
const units: Ref<OptionResult<string>[]> = ref([]);
const underlyingAssetId = ref(emptyGuid);
const form: CreateFuturesContractCommand = reactive({
  contractSpecificationId: emptyGuid,
  expirationDate: new Date(),
});

assetApi.getOptions().then((x) => (assets.value = x.data));
exchangeApi.getOptions().then((x) => (exchanges.value = x.data));
optionApi.getCurrencies().then((x) => (currencies.value = x.data));
optionApi.getUnits().then((x) => (units.value = x.data));

watch(underlyingAssetId, (underlyingAssetId) =>
  contractSpecificationApi
    .getOptions(underlyingAssetId)
    .then((x) => (contractSpecifications.value = x.data)),
);

const save = () => {
  loading.value = true;
  futuresContractApi
    .create(form)
    .then(() => success('create'))
    .finally(() => (loading.value = false));
};
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
