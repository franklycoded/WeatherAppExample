using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using WeatherForecast.DataService;
using WeatherForecast.DataService.Configuration;

namespace WeatherForecast.App_Start
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            RegisterConfiguration(container);
            RegisterServices(container);

            this.container = container;
        }

        public void RegisterConfiguration(IUnityContainer container)
        {
            var configuration = DataServiceConfiguration.GetConfiguration();

            container.RegisterInstance<IDataServiceConfiguration>(configuration, new ContainerControlledLifetimeManager());
        }

        public void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IForecastService, ForecastService>(new PerResolveLifetimeManager());
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}