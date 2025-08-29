import { permissionApi } from '@/api/identity/permission-api';

export const usePermissionOptionsQuery = () => {
  const { data, isFetching } = useQuery({
    queryKey: ['permissionApi.getOptions'],
    queryFn: () => permissionApi.getOptions(),
    staleTime: 5 * 60 * 1000,
  });

  return {
    data,
    isFetching,
  };
};
