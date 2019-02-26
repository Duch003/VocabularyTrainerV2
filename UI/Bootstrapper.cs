using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Caliburn.Micro;
using UI.ViewModels;

namespace UI
{
    public class Bootstrapper : BootstrapperBase
    {
        private CompositionContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        //protected override void Configure()
        //{
        //    container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(z => new AssemblyCatalog(z)).OfType<ComposablePartCatalog>()));

        //    CompositionBatch batch = new CompositionBatch();

        //    batch.AddExportedValue<IWindowManager>(new WindowManager());
        //    batch.AddExportedValue<IEventAggregator>(new EventAggregator());
        //    batch.AddExportedValue(container);

        //    container.Compose(batch);
        //}

        //protected override object GetInstance(Type serviceType, string key)
        //{
        //    string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
        //    var exports = container.GetExportedValues<object>(contract);
        //    if (exports.Count() > 0)
        //        return exports.First();
        //    throw new Exception($"Could not locate any instances of contract { contract }");
        //}

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
