using SimpleTrader.WPF.State.Assets;
using System.Collections.ObjectModel;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;

        private readonly ObservableCollection<AssetViewModel> _topAssets;

        public double AccountBalance => _assetStore.AccountBalance;

        public IEnumerable<AssetViewModel> TopAssets => _topAssets;

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;

            _topAssets = new ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        private void ResetAssets()
        {
            List<AssetViewModel> temp = [];
            foreach (var asset in _assetStore.AssetTransactions.GroupBy(t => t.Asset.Symbol))
            {
                int shares = asset.Sum(t => t.IsPurchase ? t.Shares : -t.Shares);
                if (shares == 0) continue;
                temp.Add(new AssetViewModel(asset.Key, shares));
            }
            
            _topAssets.Clear();
            foreach (var asset in temp.OrderByDescending(a => a.Shares).Take(3))
            {
                _topAssets.Add(asset);
            }
        }

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
            ResetAssets();
        }

    }
}
