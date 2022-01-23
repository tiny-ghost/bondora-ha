const { env } = require('process');

const target = env.ASPNETCORE_HTTP_PORT ? `https://localhost:${env.ASPNETCORE_HTTP_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:64440';

const PROXY_CONFIG = [
  {
    context: [
      "/Customer",
      "/Equipment",
      "/Order",
      "/Invoice"
   ],
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
