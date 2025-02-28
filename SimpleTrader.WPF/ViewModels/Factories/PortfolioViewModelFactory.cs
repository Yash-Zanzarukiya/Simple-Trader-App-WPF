namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class PortfolioViewModelFactory : IViewModelFactory<PortfolioViewModel>
    {
        public PortfolioViewModel CreateViewModel()
        {
            return new PortfolioViewModel();
        }
    }
}
