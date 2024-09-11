<template>
  <el-tabs
    v-model="model"
    editable
    @tab-add="openDialog"
    @tab-remove="removeLocale"
  >
    <slot></slot>

    <el-dialog
      v-model="dialogVisible"
      width="500"
      :title="$t('common.message.selectLocale')"
    >
      <el-form label-width="120">
        <el-form-item :label="$t('common.locale')">
          <SelectOption v-model="culture" :options="cultures" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button type="primary" @click="add">
          {{ $t('operation.add') }}
        </el-button>
      </template>
    </el-dialog>
  </el-tabs>
</template>

<script setup lang="ts" generic="TLocale extends Locale">
import type { Locale } from '@/models/localization';
import { usePreferencesStore } from '@/stores/preferences';

const model = defineModel<string>({ default: 'en' });
const locales = defineModel<TLocale[]>('locales', { default: () => [] });

const props = withDefaults(
  defineProps<{
    labelWidth?: string | number;
    add(culture: string): TLocale;
  }>(),
  { labelWidth: 200 },
);

const dialogVisible = ref(false);
const { cultures } = storeToRefs(usePreferencesStore());
const culture = ref('');

const openDialog = () => (dialogVisible.value = true);

const add = () => {
  if (locales.value.findIndex((x) => x.culture === culture.value) < 0) {
    locales.value.push(props.add(culture.value));
  }

  dialogVisible.value = false;
  model.value = culture.value;
};

const removeLocale = (locale: string) => {
  const index = locales.value.findIndex((x) => x.culture === locale);
  locales.value.splice(index, 1);
  model.value = locales.value.length > 0 ? locales.value[0].culture : '';
};

const labelWidth = computed(() =>
  typeof props.labelWidth === 'number'
    ? `${props.labelWidth}px`
    : props.labelWidth,
);
</script>

<style scoped lang="scss">
:deep(.el-tabs__header) {
  padding-left: v-bind('labelWidth');
}
</style>
