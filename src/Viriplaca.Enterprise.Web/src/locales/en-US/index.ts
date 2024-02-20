import auth from './auth';
import common from './common';
import leave from './leave';
import route from './route';

export default {
  ...auth,
  ...common,
  ...leave,
  ...route,
};
