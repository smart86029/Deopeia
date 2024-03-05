import auth from './auth';
import common from './common';
import leave from './leave';
import organization from './organization';
import route from './route';
import status from './status';

export default {
  ...auth,
  ...common,
  ...leave,
  ...organization,
  ...route,
  ...status,
};
