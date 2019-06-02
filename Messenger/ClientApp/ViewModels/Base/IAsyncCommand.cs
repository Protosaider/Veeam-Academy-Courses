using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientApp.ViewModels.Base
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        Boolean CanExecute();
    }
}
