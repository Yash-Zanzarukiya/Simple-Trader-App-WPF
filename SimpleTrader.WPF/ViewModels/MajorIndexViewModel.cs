using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexViewModel : ViewModelBase
    {

        private readonly IMajorIndexService majorIndexService;

        private MajorIndex _aapl;
        public MajorIndex AAPL
        {
            get { return _aapl; }
            set { _aapl = value; OnPropertyChanged(nameof(AAPL)); }
        }

        private MajorIndex _nvda;
        public MajorIndex NVDA
        {
            get { return _nvda; }
            set { _nvda = value; OnPropertyChanged(nameof(NVDA)); }
        }

        private MajorIndex _tsla;
        public MajorIndex TSLA
        {
            get { return _tsla; }
            set { _tsla = value; OnPropertyChanged(nameof(TSLA)); }
        }

        public MajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            this.majorIndexService = majorIndexService;
        }

        public static MajorIndexViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndex();
            return majorIndexViewModel;
        }

        private void LoadMajorIndex()
        {
            majorIndexService.GetMajorIndex(SymbolType.AAPL).ContinueWith(task =>
            {
                if(task.Exception == null)
                    AAPL = task.Result;
            });
            majorIndexService.GetMajorIndex(SymbolType.NVDA).ContinueWith(task =>
            {
                if (task.Exception == null)
                    NVDA = task.Result;
            });
            majorIndexService.GetMajorIndex(SymbolType.TSLA).ContinueWith(task =>
            {
                if (task.Exception == null)
                    TSLA = task.Result;
            });
        }
    }
}
