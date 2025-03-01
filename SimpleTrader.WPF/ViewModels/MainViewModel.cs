using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAuthenticator _autheticator;

        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;
        public bool IsLoggedIn => _autheticator.IsLoggedIn;

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IAuthenticator autheticator, IViewModelFactory viewModelAbstractFactory)
        {
            _navigator = navigator;
            _autheticator = autheticator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelAbstractFactory);

            _navigator.StateChanged += Navigator_StateChanged;
            _autheticator.StateChanged += Autheticator_StateChanged;

            UpdateCurrentViewModelCommand.Execute(ViewType.LOGIN);
        }

        private void Autheticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
