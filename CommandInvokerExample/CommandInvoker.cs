using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public interface ICommandInvoker
    {
        void Invoke<T>(Expression<Action<T>> expression) where T : ICommand;

        TResult Invoke<TCommand, TResult>(Expression<Func<TCommand, TResult>> expression) where TCommand:ICommand;
            
    }


    public class CommandInvoker : ICommandInvoker
    {
        private readonly IList<ICommand> _commands;

        public CommandInvoker(IList<ICommand> commands)
        {
            _commands = commands;
        }

        public void Invoke<TCommand>(Expression<Action<TCommand>> expression) where TCommand:ICommand
        {
            var command = _commands.OfType<TCommand>().FirstOrDefault();
            if (command != null)
            {
                expression.Compile().Invoke(command);
                return;
            }
            throw new ArgumentException(nameof(TCommand));
        }

        public TResult Invoke<TCommand, TResult>(Expression<Func<TCommand, TResult>> expression) where TCommand:ICommand
        {
            var command = _commands.OfType<TCommand>().FirstOrDefault();
            if (command != null)
            {
                return expression.Compile().Invoke(command);
            }
            throw new ArgumentException(nameof(TCommand));
        }
    }
}