import { UserManager } from 'oidc-client-ts';

export const useAuthStore = defineStore('auth', () => {
  const uri = 'http://localhost:5173/';
  const userManager = new UserManager({
    authority: 'https://localhost:7099/',
    response_type: 'code',
    client_id: 'Finance',
    scope: 'openid profile email api',
    redirect_uri: `${uri}auth/callback`,
    silent_redirect_uri: `${uri}auth/silent-refresh`,
    post_logout_redirect_uri: uri,
  });

  const isOperator = ref(false);

  const getUser = async () => {
    let user = await userManager.getUser();
    if (user === null) {
      await signIn();
      user = await userManager.getUser();
    }

    if (user) {
      const payload = JSON.parse(atob(user?.access_token.split('.')[1]));
      isOperator.value = !payload.permissions.split(',').includes('Trade');
    }

    return user;
  };

  const signIn = () => {
    return userManager.signinRedirect();
  };

  const signInCallback = () => userManager.signinCallback();

  const signOut = () =>
    userManager.revokeTokens().then(() => userManager.removeUser());

  const refresh = () => userManager.signinSilent();

  return {
    isOperator,
    getUser,
    signIn,
    signInCallback,
    signOut,
    refresh,
  };
});
