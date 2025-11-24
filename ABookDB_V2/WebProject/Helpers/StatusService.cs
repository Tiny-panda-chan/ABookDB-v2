/*namespace WebProject.Helpers
{
    public interface IStatusService
    {
        void GetServiceStatus();
        T GetService<T>();
    }

    public class StatusService : IStatusService
    {
        private IServiceProvider serviceProvider;
        public StatusService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public T GetService<T>()
        {
            return (T)this.serviceProvider.GetService(typeof(T));
        }

        public void GetServiceStatus()
        {
            var config = this.GetService<IConfiguration>();

        }
    }
}*/
