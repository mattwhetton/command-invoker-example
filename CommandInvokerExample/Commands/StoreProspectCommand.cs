using System.Threading.Tasks;

namespace ConsoleApplication4.Commands
{
    public interface IStoreProspectCommand : ICommand
    {
        Task<int> ExecuteAsync();
    }

    public class StoreProspectCommand : IStoreProspectCommand
    {
        public async Task<int> ExecuteAsync()
        {
            return 123;
        }
    }
}