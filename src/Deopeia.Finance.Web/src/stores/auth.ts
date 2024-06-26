import { UserManager } from 'oidc-client-ts';

export const useAuthStore = defineStore('auth', () => {
  const uri = 'http://localhost:5173/';
  const userManager = new UserManager({
    authority: 'https://localhost:7099/',
    response_type: 'code',
    client_id: 'Enterprise',
    scope: 'openid profile email api',
    redirect_uri: `${uri}auth/sign-in-callback`,
    silent_redirect_uri: `${uri}auth/silent-callback.html`,
    post_logout_redirect_uri: uri,
  });

  const getUser = () => userManager.getUser();

  const signIn = () => userManager.signinRedirect();

  const signInCallback = () => userManager.signinCallback();

  const signOut = () => userManager.signoutRedirect();

  const refresh = () => userManager.signinSilent();

  return {
    getUser,
    signIn,
    signInCallback,
    signOut,
    refresh,
  };
});
