export default {
  route: {
    client: {
      module: '客戶',
      trader: {
        list: '交易者列表',
        create: '新增交易者',
        edit: '編輯交易者',
      },
      introducingBroker: {
        list: '介紹經紀商列表',
      },
      kyc: {
        list: 'KYC',
      },
    },
    dashboard: {
      default: '儀表板',
    },
    fund: {
      module: '資金',
      deposit: {
        list: '存款列表',
      },
      withdrawal: {
        list: '提款列表',
      },
    },
    home: '首頁',
    identity: {
      module: '身分',
      user: {
        list: '使用者列表',
        create: '新增使用者',
        edit: '編輯使用者',
      },
      role: {
        list: '角色列表',
        create: '新增角色',
        edit: '編輯角色',
      },
      permission: {
        list: '權限列表',
        create: '新增權限',
        edit: '編輯權限',
      },
    },
    me: {
      profile: '個人資料',
      password: '修改密碼',
      twoFactorAuthentication: '二步驟驗證',
    },
    report: {
      module: '報表',
      profitAndLoss: '損益報表',
      cashFlow: '現金流報表',
    },
    risk: {
      module: '風險',
      overview: '概觀',
      marginCall: {
        list: '保證金通知',
      },
      forcedLiquidation: {
        list: '強制平倉列表',
      },
    },
    setting: {
      module: '設定',
      instrument: {
        list: '標的列表',
        create: '新增標的',
        edit: '編輯標的 {symbol}',
      },
    },
    trading: {
      module: '交易',
      position: {
        list: '持倉列表',
        close: '平倉',
      },
      order: {
        list: '訂單列表',
      },
      trade: {
        list: '交易列表',
      },
    },
  },
};
