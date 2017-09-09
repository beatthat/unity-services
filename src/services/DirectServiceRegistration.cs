using System;

namespace BeatThat.Service
{
	/// <summary>
	/// registers a service to a provided implementation
	/// </summary>
	public class DirectServiceRegistration : ServiceRegistration
	{
		public DirectServiceRegistration(object service, Type registrationInterface)
		{
			this.registrationInterface = registrationInterface;
			m_service = service;
		}

		public DirectServiceRegistration(object service) : this(service, service.GetType()) {}

		protected Type registrationInterface { get; private set; }

		public int registrationGroup { get { return m_registrationGroup; } }

		public ServiceRegistration SetRegistrationGroup(int registrationGroup)
		{
			m_registrationGroup = registrationGroup;
			return this;
		}

		public void SetServiceRegistration(ServiceLoader loader)
		{
			loader.SetServiceRegistration(this, this.registrationInterface);
		}

		public void RegisterService(Services toLocator)
		{
			toLocator.RegisterService(this, this.registrationInterface);
		}

		virtual public void InitService(Services serviceLocator, Action onCompleteCallback)
		{
			ServiceLoader.DoDefaultInitService(this, serviceLocator, onCompleteCallback);
		}

		public bool UnregisterService(Services toLocator)
		{
			return toLocator.UnregisterService(this.registrationInterface);
		}

		public ServiceType GetService<ServiceType>(Services serviceLocator)
			where ServiceType : class
		{
			return GetService(serviceLocator) as ServiceType;
		}

		public object GetService(Services serviceLocator)
		{
			return m_service;
		}
			
		private object m_service;
		private int m_registrationGroup = 0;
	}

	public class DirectServiceRegistration<RegistrationInterface> : DirectServiceRegistration
	{
		public DirectServiceRegistration(RegistrationInterface service) : base(service, typeof(RegistrationInterface)) {}
	}
}