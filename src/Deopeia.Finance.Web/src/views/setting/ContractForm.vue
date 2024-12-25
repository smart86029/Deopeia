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

    <el-form-item :label="$t('trading.session')">
      <div class="new-session">
        <SelectDayOfWeek class="day-of-week" />
        <TimeRangePicker v-model="times" class="time-range-picker" />
        <el-button type="primary" plain @click="addSession">
          {{ $t('operation.add') }}
        </el-button>
      </div>
      <div class="sessions">
        <el-text v-for="session in form.sessions" :key="session.openDay">
          {{ weekday(session.openDay) }} {{ session.openTime }}-{{
            session.closeTime
          }}
        </el-text>
      </div>
    </el-form-item>

    <el-form-item>
      <ButtonBack />
      <ButtonSave :loading="loading" />
    </el-form-item>
  </el-form>
</template>

<script setup lang="ts">
import contractApi, {
  type Contract,
  type ContractLocale,
} from '@/api/trading/contract-api';
import { DayOfWeek } from '@/models/day-of-week';
import { UnderlyingType } from '@/models/underlying-type';
import { weekday } from '@/plugins/dayjs';
import { success } from '@/plugins/element';
import { useOptionStore } from '@/stores/option';
import { dayjs } from 'element-plus';

const props = defineProps<{
  action: 'create' | 'edit';
  symbol: string;
}>();
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
const dayOfWeek = ref(DayOfWeek.Sunday);
const times: [Date, Date] = reactive([
  dayjs('00:00:00', 'HH:mm:ss').toDate(),
  dayjs('01:00:00', 'HH:mm:ss').toDate(),
]);

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

const addSession = () => {
  form.sessions.push({
    openDay: dayOfWeek.value,
    openTime: dayjs(times[0]).format('HH:mm:ss'),
    closeDay: dayOfWeek.value,
    closeTime: dayjs(times[1]).format('HH:mm:ss'),
  });
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

.sessions {
  display: flex;
  flex-direction: column;

  .el-text {
    align-self: flex-start;
  }
}

.new-session {
  display: flex;
  width: 100%;
  gap: 16px;
}

.day-of-week {
  flex: 1;
}

.time-range-picker {
  flex: 1;
}
</style>
