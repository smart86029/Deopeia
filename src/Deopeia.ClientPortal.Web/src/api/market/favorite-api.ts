import httpClient from '../http-client';

export const favoriteApi = {
  like: (symbol: string) => httpClient.put(`/Favorites/${symbol}`),
  dislike: (symbol: string) => httpClient.delete(`/Favorites/${symbol}`),
};
