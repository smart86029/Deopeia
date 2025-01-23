import { UserManager } from 'oidc-client-ts';

export const useAuthStore = defineStore('auth', () => {
  const uri = 'http://localhost:5173/';
  const userManager = new UserManager({
    authority: 'https://localhost:7099/',
    response_type: 'code',
    client_id: 'Finance',
    scope: 'openid profile email api',
    redirect_uri: `${uri}auth/sign-in-callback`,
    silent_redirect_uri: `${uri}auth/silent-callback`,
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
      isOperator.value = !payload.permissions.split(',').includes('Trader');
    }

    return user;
  };

  const signIn = () => {
    return userManager.signinRedirect();
  };

  const signInCallback = () => userManager.signinCallback();

  const signOut = () => userManager.signoutRedirect();

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
