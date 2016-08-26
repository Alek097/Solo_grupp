namespace API.Infrastructure
{
    #region Using
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using Microsoft.Practices.Unity;

    #endregion
    public sealed class UnityHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IUnityContainer _container;

        public UnityHttpControllerActivator(IUnityContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController result = null;

            if (controllerType != null)
            {
                result = _container.Resolve(controllerType) as IHttpController;
            }

            return result;
        }
    }
}