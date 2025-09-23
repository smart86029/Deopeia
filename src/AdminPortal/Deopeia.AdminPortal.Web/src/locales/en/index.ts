import action from './action';
import auth from './auth';
import common from './common';
import fund from './fund';
import identity from './identity';
import product from './product';
import route from './route';
import status from './status';
import trading from './trading';

export default {
  ...action,
  ...auth,
  ...common,
  ...fund,
  ...identity,
  ...product,
  ...route,
  ...status,
  ...trading,
};
