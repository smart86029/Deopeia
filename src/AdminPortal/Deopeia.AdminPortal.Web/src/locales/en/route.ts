export default {
  route: {
    client: {
      module: 'Client',
      trader: {
        list: 'Traders',
        create: 'Create Trader',
        edit: 'Edit Trader',
      },
      introducingBroker: {
        list: 'Introducing Brokers',
      },
      kyc: {
        list: 'KYC',
      },
    },
    dashboard: {
      default: 'Dashboard',
    },
    fund: {
      module: 'Fund',
      deposit: {
        list: 'Deposits',
      },
      withdrawal: {
        list: 'Withdrawals',
      },
    },
    home: 'Home',
    identity: {
      module: 'Identity',
      user: {
        list: 'Users',
        create: 'Create User',
        edit: 'Edit User',
      },
      role: {
        list: 'Roles',
        create: 'Create Role',
        edit: 'Edit Role',
      },
      permission: {
        list: 'Permissions',
        create: 'Create Permission',
        edit: 'Edit Permission',
      },
    },
    me: {
      profile: 'Profile',
      password: 'Change Password',
      twoFactorAuthentication: 'Two-Factor Authentication',
    },
    report: {
      module: 'Report',
      profitAndLoss: 'Profit And Loss',
      cashFlow: 'Cash Flow',
    },
    risk: {
      module: 'Risk',
      overview: 'Overview',
      marginCall: {
        list: 'Margin Calls',
      },
      forcedLiquidation: {
        list: 'Forced Liquidations',
      },
    },
    setting: {
      module: 'Setting',
      instrument: {
        list: 'Instruments',
        create: 'Create Instrument',
        edit: 'Edit Instrument {symbol}',
      },
    },
    trading: {
      module: 'Trading',
      position: {
        list: 'Positions',
        close: 'Close Position',
      },
      order: {
        list: 'Orders',
      },
      trade: {
        list: 'Trades',
      },
    },
  },
};
