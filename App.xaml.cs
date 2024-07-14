using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using PRN_Project_Summer_2024.Util;
using PRN_Project_Summer_2024.ViewModels;

namespace PRN_Project_Summer_2024;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>

public partial class App : Application
{

    private readonly Navigation navigation;
    public App()
    {
        navigation = new Navigation();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        navigation.ViewModel = new LoginPageViewModel(navigation);
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(navigation)
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}