using Autofac;
using PetrolPriceMonitor.Factories;

namespace PetrolPriceMonitor.Bootstrapping
{
    public abstract class AutofacBootstrapper
    {
        public IContainer Run()
        {
            var builder = new ContainerBuilder();

            ConfigureContainer(builder);

            var container = builder.Build();
            var viewFactory = container.Resolve<IViewFactory>();

            RegisterViews(viewFactory);

            ConfigureApplication(container);

            return container;
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModule>();
        }

        protected abstract void RegisterViews(IViewFactory viewFactory);

        protected abstract void ConfigureApplication(IContainer container);
    }

}
