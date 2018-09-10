using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;
using VyatkinSchool.Controllers;
using VyatkinSchool.Helpers;

namespace VyatkinSchool
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            System.Func<HttpContextBase, bool> contextCheckDelegate = contextCheck;
            DefaultDisplayMode mobileMode = new DefaultDisplayMode("Mobile");
            mobileMode.ContextCondition = contextCheckDelegate;
            DisplayModeProvider.Instance.Modes.Add(mobileMode);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //BundleMobileConfig.RegisterBundles(BundleTable.Bundles);
        }

        public bool contextCheck(HttpContextBase httpContextBase)
        {
            string strUserAgent = httpContextBase.Request.UserAgent;
            Regex strBrowser = new Regex(@"android.+mobile|blackberry|iphone|ip(hone|od)",
                            RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if ((strBrowser.IsMatch(strUserAgent)))
            {
                return true;
            }
            return false;
        }

        protected void Application_BeginRequest()
        {
            if (!Context.Request.IsSecureConnection)
            {
                // This is an insecure connection, so redirect to the secure version
                UriBuilder uri = new UriBuilder(Context.Request.Url);
                uri.Scheme = "https";
                if (uri.Port > 44360  && uri.Host.Equals("localhost"))
                {
                    // Development box - set uri.Port to 51184 by default
                    uri.Port = 44360;
                }
                else
                {
                    uri.Port = 443;
                }

                Response.Redirect(uri.ToString());
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Log.Info("Application_Error: invoked");
            var httpContext = ((MvcApplication)sender).Context;
            var currentController = " ";
            var currentAction = " ";
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                Log.Info("Application_Error: currentRouteData exist");

                if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                    Log.Info($"Application_Error: currentController = {currentController}");
                }
                else
                {
                    Log.Info("Application_Error: currentController not set");
                }

                if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                    Log.Info($"Application_Error: currentAction = {currentAction}");
                }
                else
                {
                    Log.Info("Application_Error: currentAction not set");
                }
            }
            else
            {
                Log.Info("Application_Error: currentRouteData not exist");
            }

            var lastException = Server.GetLastError();
            if(lastException != null)
            {
                Log.Error("Application_Error: Last exception is:", lastException);
            }

            var routeData = new RouteData();
            var action = "GenericError";

            if (lastException is HttpException)
            {
                var httpException = lastException as HttpException;

                var httpCode = httpException.GetHttpCode();
                Log.Info($"Application_Error: This is HTTP exception: {httpCode}");

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        action = "NotFound";
                        break;

                        // others if any
                }
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = lastException is HttpException ? ((HttpException)lastException).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            Log.Info($"Application_Error: Create new HandleErrorInfo");
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;
            routeData.Values["exception"] = new HandleErrorInfo(lastException, currentController, currentAction);

            IController errormanagerController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(httpContext);
            var requestContext = new RequestContext(wrapper, routeData);
            errormanagerController.Execute(requestContext);

            Log.Info($"Application_Error: Completed");
        }
    }
}
