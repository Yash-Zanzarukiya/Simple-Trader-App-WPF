using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private readonly IViewModelFactory<HomeViewModel> homeViewModelFactory;
        private readonly IViewModelFactory<PortfolioViewModel> portfolioViewModelFactory;
        private readonly BuyViewModel buyViewModel;

        public RootViewModelFactory(IViewModelFactory<HomeViewModel> homeViewModelFactory, IViewModelFactory<PortfolioViewModel> portfolioViewModelFactory, BuyViewModel buyViewModel)
        {
            this.homeViewModelFactory = homeViewModelFactory;
            this.portfolioViewModelFactory = portfolioViewModelFactory;
            this.buyViewModel = buyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.HOME:
                    return homeViewModelFactory.CreateViewModel();
                case ViewType.PORTFOLIO:
                    return portfolioViewModelFactory.CreateViewModel();
                case ViewType.BUY:
                    return buyViewModel;
                default:
                    throw new ApiException("View model not found");
            }
        }
    }
}
