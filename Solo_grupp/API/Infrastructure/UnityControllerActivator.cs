namespace API.Infrastructure
{
    #region Using
    using System;
    using System.Diagnostics;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;
    #endregion
    public sealed class UnityControllerActivator : IControllerActivator
    {
        private readonly IUnityContainer _container;

        public UnityControllerActivator(IUnityContainer container)
        {
            _container = container;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            IController result = null;

            if (controllerType != null)
            {
                result = _container.Resolve(controllerType) as IController;
            }

            return result;
        }
    }
}