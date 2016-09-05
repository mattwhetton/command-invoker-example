namespace ConsoleApplication4.Commands
{
    public interface IStoreAddressCommand : ICommand
    {
        void Execute();
    }

    public class StoreAddressCommand : IStoreAddressCommand
    {
        public void Execute()
        {

        }
    }
}