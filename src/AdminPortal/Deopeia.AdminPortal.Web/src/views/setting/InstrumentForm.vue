<template>
  <el-form v-loading="isLoading" :model="form" label-width="200" @submit.prevent="mutate">
    <el-form-item :label="$t('common.type')">
      <RadioEnum
        v-if="action === 'create'"
        v-model="form.type"
        :enum="InstrumentType"
        localeKey="product.instrumentType"
      />
      <TextEnum v-else :value="form.type" localeKey="product.instrumentType" />
    </el-form-item>

    <el-form-item :label="$t('product.symbol')">
      <el-input v-model="form.symbol" />
    </el-form-item>

    <LocaleTabs v-model:locales="form.localizations" :add="add">
      <LocaleTabPane v-for="locale in form.localizations" :locale="locale" :key="locale.culture">
        <el-form-item :label="$t('common.name')">
          <el-input v-model="locale.name" />
        </el-form-item>
      </LocaleTabPane>
    </LocaleTabs>

    <el-form-item :label="$t('product.baseAsset')">
      <el-input v-model="form.baseAsset" />
    </el-form-item>
    <el-form-item :label="$t('product.quoteAsset')">
      <el-input v-model="form.quoteAsset" />
    </el-form-item>
    <el-form-item :label="$t('product.pricePrecision')">
      <el-input-number v-model="form.pricePrecision" :min="0" :step="1" />
    </el-form-item>
    <el-form-item :label="$t('product.quantityPrecision')">
      <el-input-number v-model="form.quantityPrecision" :min="0" :step="1" />
    </el-form-item>
    <el-form-item :label="$t('product.minQuantity')">
      <el-input-number v-model="form.minQuantity" :min="0" :step="0.00000001" />
    </el-form-item>
    <el-form-item :label="$t('product.minNotional')">
      <el-input-number v-model="form.minNotional" :min="0" :step="0.00000001" />
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="isPending" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import {
  instrumentApi,
  type Instrument,
  type InstrumentLocalization,
} from '@/api/setting/instrument-api';
import { InstrumentType } from '@/models/instrument-type';
import { success } from '@/plugins/element';

const props = defineProps<{
  action: 'create' | 'edit';
  id: Guid;
}>();

const form: Instrument = reactive({
  id: emptyGuid,
  type: InstrumentType.Spot,
  symbol: '',
  baseAsset: '',
  quoteAsset: '',
  pricePrecision: 0,
  quantityPrecision: 0,
  minQuantity: 0,
  minNotional: 0,
  localizations: [{ culture: 'en', name: '' }],
});

const { isFetching } = useQuery({
  queryKey: ['instrumentApi.get', props.id],
  queryFn: () => instrumentApi.get(props.id).then((x) => Object.assign(form, x)),
  enabled: props.action === 'edit',
});
const { isLoading } = useDeferredLoading(isFetching);

const { isPending, mutate } = useMutation({
  mutationFn: () =>
    props.action === 'create' ? instrumentApi.create(form) : instrumentApi.update(form),
  onSuccess: () => success(props.action),
});

const add = (culture: string): InstrumentLocalization => ({
  culture: culture,
  name: '',
});
</script>

<style scoped lang="scss">
.el-form {
  max-width: 1000px;
}
</style>
