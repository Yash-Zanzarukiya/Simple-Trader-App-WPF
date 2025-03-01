namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public MajorIndexViewModel MajorIndexViewModel { get; set; }

        public HomeViewModel(MajorIndexViewModel majorIndexViewModel, AssetSummaryViewModel assetSummaryViewModel)
        {
            MajorIndexViewModel = majorIndexViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }

    }
}
