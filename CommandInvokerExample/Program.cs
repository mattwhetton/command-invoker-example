using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ConsoleApplication4.Commands;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = BuildContainer();

            var commandInvoker = container.Resolve<ICommandInvoker>();

            // simple command invocation example
            commandInvoker.Invoke<IStoreAddressCommand>(x => x.Execute());


            // example with return value
            var result1 = commandInvoker.Invoke<IGetPropsectCommand, int>(x => x.Execute());

            // async example
            var result2 = commandInvoker.Invoke<IStoreProspectCommand, Task<int>>(x => x.ExecuteAsync()).GetAwaiter().GetResult();
            
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<ICommand>())
                .AsImplementedInterfaces();

            builder.RegisterType<CommandInvoker>().As<ICommandInvoker>();

            var container = builder.Build();
            return container;
        }
    }


}
