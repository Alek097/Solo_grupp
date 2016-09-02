namespace API.App_Start
{
	#region Using
	using Microsoft.Practices.Unity;
	using System;
	using System.Collections.Generic;
	using System.Web.Http.Dependencies;
	#endregion
	public class UnityResolver : IDependencyResolver
	{
		internal IUnityContainer Container { get; set; }

		public UnityResolver(IUnityContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			this.Container = container;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return Container.Resolve(serviceType);
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
				return Container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		public IDependencyScope BeginScope()
		{
			var child = Container.CreateChildContainer();
			return new UnityResolver(child);
		}

		public void Dispose()
		{
			Container.Dispose();
		}
	}
}