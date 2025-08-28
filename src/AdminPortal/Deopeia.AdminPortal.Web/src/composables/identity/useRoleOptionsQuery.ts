import { roleApi } from '@/api/identity/role-api';

export const useRoleOptionsQuery = () => {
  const { data, isFetching } = useQuery({
    queryKey: ['roleApi.getOptions'],
    queryFn: () => roleApi.getOptions(),
    staleTime: 5 * 60 * 1000,
  });

  return {
    data,
    isFetching,
  };
};
