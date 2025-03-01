using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.State.Navigators
{
    public enum ViewType
    {
        LOGIN,
        HOME,
        PORTFOLIO,
        BUY,
        SELL
    }

    public interface INavigator
    {
        public ViewModelBase CurrentViewModel { get; set; }

        event Action StateChanged;
    }
}
