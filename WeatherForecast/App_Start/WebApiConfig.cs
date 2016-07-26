using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Microsoft.Practices.Unity;
using WeatherForecast.App_Start;
using WeatherForecast.DataService.Configuration;
using WeatherForecast.DataService;
using WeatherForecast.DataService.External;
using WeatherForecast.DataService.Cache;

namespace WeatherForecast
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Registering components
            var unityContainer = new UnityContainer();
            RegisterConfiguration(unityContainer);
            RegisterServices(unityContainer);
            config.DependencyResolver = new UnityResolver(unityContainer);

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterConfiguration(IUnityContainer container)
        {
            var configuration = DataServiceConfiguration.GetConfiguration();

            container.RegisterInstance<IDataServiceConfiguration>(configuration, new ContainerControlledLifetimeManager());
        }

        public static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IForecastService, ForecastService>(new PerResolveLifetimeManager());

            container.RegisterType<IExternalApiService, ExternalApiService>(new ContainerControlledLifetimeManager());

            container.RegisterType<IForecastCache, ForecastCache>(new ContainerControlledLifetimeManager());
        }
    }
}
