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
        private static ICommandInvoker _commandInvoker;

        static void Main(string[] args)
        {
            var container = BuildContainer();

            _commandInvoker = container.Resolve<ICommandInvoker>();

            // example usage:

            DoSomething();
            
            var result1 = DoSomethingWithResult();

            var result2 = DoSomethingWithResultAsync().GetAwaiter().GetResult();


        }

        private static void DoSomething()
        {
            _commandInvoker.Invoke<IStoreAddressCommand>(x => x.Execute());
        }

        private static int DoSomethingWithResult()
        {
            return _commandInvoker.Invoke<IGetPropsectCommand, int>(x => x.Execute());
        }

        private static async Task<int> DoSomethingWithResultAsync()
        {
            return await _commandInvoker.Invoke<IStoreProspectCommand, Task<int>>(async x => await x.ExecuteAsync());
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
