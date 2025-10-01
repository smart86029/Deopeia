<template>
  <TableToolbar>
    <el-form :model="request" :inline="true">
      <el-form-item :label="$t('action.search')">
        <el-input v-model="request.keyword" />
      </el-form-item>
      <el-form-item :label="$t('common.type')">
        <SelectEnum
          v-model="request.type"
          :enum="InstrumentType"
          localeKey="product.instrumentType"
        />
      </el-form-item>
    </el-form>
    <template #right>
      <ButtonCreate route="setting.instrument.create" />
    </template>
  </TableToolbar>

  <el-table v-loading="isLoading" :data="data?.items" table-layout="auto">
    <el-table-column prop="symbol" :label="$t('product.symbol')" />
    <el-table-column prop="name" :label="$t('common.name')" />
    <TableColumnEnum prop="type" :label="$t('common.type')" localeKey="product.instrumentType" />
    <el-table-column :label="$t('common.actions')">
      <template #default="{ row }">
        <DividerSpace>
          <ButtonLink :to="{ name: 'setting.instrument.edit', params: { id: row.id } }" />
          <el-button type="danger" link @click="deleteInstrument(row.id, row.symbol)">
            {{ $t('action.delete') }}
          </el-button>
        </DividerSpace>
      </template>
    </el-table-column>
  </el-table>

  <TablePagination
    v-model:current-page="request.pageIndex"
    v-model:page-size="request.pageSize"
    :total="data?.totalCount"
  />
</template>

<script setup lang="ts">
import { instrumentApi, type GetInstrumentsRequest } from '@/api/setting/instrument-api';
import type { Guid } from '@/models/guid';
import { InstrumentType } from '@/models/instrument-type';

const request: GetInstrumentsRequest = reactive({
  ...defaultQuery,
});
const queryClient = useQueryClient();
const { data, isFetching } = useQuery({
  queryKey: ['instrumentApi.getList', request],
  queryFn: () => instrumentApi.getList(request),
});
const { isLoading } = useDeferredLoading(isFetching);
const { confirmDelete } = useConfirm();
const { success } = useNotify();

const deleteInstrument = async (id: Guid, name: string) => {
  await confirmDelete('product.instrument', name);
  await instrumentApi.delete(id);
  queryClient.invalidateQueries({ queryKey: ['instrumentApi.getList', request] });
  success('delete');
};
</script>
