import { UserManager } from 'oidc-client-ts';

export const useAuthStore = defineStore('auth', () => {
  const uri = 'http://localhost:5174/';
  const userManager = new UserManager({
    authority: 'https://localhost:7099/',
    response_type: 'code',
    client_id: 'Back Office',
    scope: 'openid profile email api',
    redirect_uri: `${uri}auth/callback`,
    silent_redirect_uri: `${uri}silent-refresh`,
    post_logout_redirect_uri: uri,
  });

  const isOperator = ref(false);

  const getUser = async () => {
    let user = await userManager.getUser();
    if (user === null) {
      signIn();
      user = await userManager.getUser();
    }

    if (user) {
      const payload = JSON.parse(atob(user?.access_token.split('.')[1]));
      isOperator.value = !payload.permissions.split(',').includes('Trade');
    }

    return user;
  };

  const signIn = () => {
    console.log('Redirecting to OIDC login...');

    window.location.href = `/oidc/Login?t=${new Date().getTime()}`;
  };

  const signInCallback = () => userManager.signinCallback();

  const signOut = () => userManager.signoutRedirect().then(() => userManager.removeUser());

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
