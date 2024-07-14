using PRN_Project_Summer_2024.ViewModels;

namespace PRN_Project_Summer_2024.Util;

    public class Navigation
    {
        private ViewModel _viewModel;
        public ViewModel ViewModel { get => _viewModel; set
        {
            _viewModel = value;
            OnCurrentViewModelChanged();
        } }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public event Action CurrentViewModelChanged;
    }