using PRN_Project_Summer_2024.Util;

namespace PRN_Project_Summer_2024.ViewModels;
class MainViewModel:ViewModel
{
    private Navigation navigation;
    public ViewModel CurrentViewModel { get => navigation.ViewModel;  }

    public MainViewModel(Navigation navigation)
    {
        this.navigation = navigation;
        navigation.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        NotifyPropertyChanged(nameof(CurrentViewModel));
    }
}