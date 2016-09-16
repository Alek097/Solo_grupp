namespace API
{
	#region Using
	using Microsoft.Practices.Unity;
	#endregion
	public static class DependencyContainer
	{
		public static IUnityContainer Container { get; set; }
		public static T GetType<T>()
		{
			return Container.Resolve<T>();
		}
	}
}