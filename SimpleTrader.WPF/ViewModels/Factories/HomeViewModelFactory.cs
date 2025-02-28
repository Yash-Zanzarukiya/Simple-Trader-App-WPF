namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory : IViewModelFactory<HomeViewModel>
    {
        private readonly IViewModelFactory<MajorIndexViewModel> majorIndexViewModelFactory;

        public HomeViewModelFactory(IViewModelFactory<MajorIndexViewModel> majorIndexViewModelFactory)
        {
            this.majorIndexViewModelFactory = majorIndexViewModelFactory;
        }

        public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(majorIndexViewModelFactory.CreateViewModel());
        }
    }
}
