using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.Models;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace SimpleTrader.WPF.State.Navigators
{
	public class Navigator : ObservableObject, INavigator
	{
		private ViewModelBase _currentViewModel;

		public ViewModelBase CurrentViewModel
		{
			get { return _currentViewModel; }
			set { _currentViewModel = value; OnPropertyChanged(); }
		}

		public ICommand UpdateCurrentViewModelCommand { get; set; }

		public Navigator(IRootViewModelFactory viewModelAbstractFactory)
		{
			UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelAbstractFactory);
		}

	}
}
 