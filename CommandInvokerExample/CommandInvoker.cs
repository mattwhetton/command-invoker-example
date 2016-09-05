using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public interface ICommandInvoker
    {
        void Invoke<T>(Action<T> method) where T : ICommand;
        
        TResult Invoke<TCommand, TResult>(Func<TCommand, TResult> method) where TCommand : ICommand;
    }


    public class CommandInvoker : ICommandInvoker
    {
        private readonly IList<ICommand> _commands;

        public CommandInvoker(IList<ICommand> commands)
        {
            _commands = commands;
        }

        public void Invoke<TCommand>(Action<TCommand> method) where TCommand:ICommand
        {
            var command = _commands.OfType<TCommand>().FirstOrDefault();
            if (command != null)
            {
                method.Invoke(command);
                return;
            }
            throw new ArgumentException(nameof(TCommand));
        }
        
        public TResult Invoke<TCommand, TResult>(Func<TCommand, TResult> method) where TCommand : ICommand
        {
            var command = _commands.OfType<TCommand>().FirstOrDefault();
            if (command != null)
            {
                return method.Invoke(command);
            }
            throw new ArgumentException(nameof(TCommand));
        }
    }
}