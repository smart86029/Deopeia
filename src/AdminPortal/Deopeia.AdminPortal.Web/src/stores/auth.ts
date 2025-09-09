export const useAuthStore = defineStore('auth', () => {
  const signIn = () => {
    window.location.href = `/oidc/SignIn?t=${new Date().getTime()}`;
  };

  const signOut = () => {};

  return {
    signIn,
    signOut,
  };
});
