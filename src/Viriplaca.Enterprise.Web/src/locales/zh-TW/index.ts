import auth from './auth';
import common from './common';
import leave from './leave';
import operation from './operation';
import organization from './organization';
import route from './route';
import status from './status';

export default {
  ...auth,
  ...common,
  ...leave,
  ...operation,
  ...organization,
  ...route,
  ...status,
};
