using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> createPortfolioViewModel;
        private readonly CreateViewModel<LoginViewModel> createLoginViewModel;
        private readonly CreateViewModel<BuyViewModel> createBuyViewModel;

        public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, 
            CreateViewModel<PortfolioViewModel> createPortfolioViewModel, 
            CreateViewModel<LoginViewModel> createLoginViewModel, 
            CreateViewModel<BuyViewModel> createBuyViewModel)
        {
            this.createHomeViewModel = createHomeViewModel;
            this.createPortfolioViewModel = createPortfolioViewModel;
            this.createLoginViewModel = createLoginViewModel;
            this.createBuyViewModel = createBuyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.LOGIN:
                    return createLoginViewModel();
                case ViewType.HOME:
                    return createHomeViewModel();
                case ViewType.PORTFOLIO:
                    return createPortfolioViewModel();
                case ViewType.BUY:
                    return createBuyViewModel();
                default:
                    throw new ApiException("View model not found");
            }
        }
    }
}
