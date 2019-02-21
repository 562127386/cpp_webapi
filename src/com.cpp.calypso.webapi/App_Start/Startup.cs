using System;
using System.Configuration;
using Abp.Owin;
using com.cpp.calypso.webapi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace com.cpp.calypso.webapi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseAbp();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //var OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            //app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString(Constantes.AUTENTIFICACION_LOGIN),
            //    // by setting following values, the auth cookie will expire after the configured amount of time (default 14 days) when user set the (IsPermanent == true) on the login
            //    ExpireTimeSpan = new TimeSpan(int.Parse(ConfigurationManager.AppSettings["AuthSession.ExpireTimeInDays.WhenPersistent"] ?? "14"), 0, 0, 0),
            //    SlidingExpiration = bool.Parse(ConfigurationManager.AppSettings["AuthSession.SlidingExpirationEnabled"] ?? bool.FalseString)

            //});

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


        }
    }
}
