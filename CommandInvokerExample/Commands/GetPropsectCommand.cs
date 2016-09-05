namespace ConsoleApplication4.Commands
{

    public interface IGetPropsectCommand : ICommand
    {
        int Execute();
    }
        
    public class GetPropsectCommand : IGetPropsectCommand
    {
        public int Execute()
        {
            return 123;
        }
    }
}