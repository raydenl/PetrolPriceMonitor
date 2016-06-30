using Autofac;

namespace PetrolPriceMonitor.iOS.Bootstrapping
{
    public abstract class AutofacBootstrapper
    {
        public IContainer Run()
        {
            var builder = new ContainerBuilder();

            ConfigureContainer(builder);

            var container = builder.Build();
            
            ConfigureApplication(container);

            return container;
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModule>();
        }
        
        protected abstract void ConfigureApplication(IContainer container);
    }

}
