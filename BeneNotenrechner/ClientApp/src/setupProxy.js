const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:49131';

const context =  [
    "/logincall",
    "/reauthenticate",

    "/nt/nt_supersubject",
    "/nt/nt_createsupersubject", 
    "/nt/nt_deletesupersubject", 
    "/nt/nt_updatesupersubject", 

    "/nt/nt_subject",
    "/nt/nt_createsubject",
    "/nt/nt_updatesubject",
    "/nt/nt_deletesubject",

    "/nt/nt_grade",
    "/nt/nt_creategrade",
    "/nt/nt_updategrade",
    "/nt/nt_deletegrade",
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};
