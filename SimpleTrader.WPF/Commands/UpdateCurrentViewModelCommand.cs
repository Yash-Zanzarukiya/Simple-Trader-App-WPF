using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : AsyncCommandBase
    {

        private INavigator navigator;

        private readonly IViewModelFactory viewModelAbstractFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelAbstractFactory)
        {
            this.navigator = navigator;
            this.viewModelAbstractFactory = viewModelAbstractFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is ViewType viewType)
            {
                navigator.CurrentViewModel = viewModelAbstractFactory.CreateViewModel(viewType);
            }
        }
    }
}
