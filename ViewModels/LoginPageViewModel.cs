using System.Collections.ObjectModel;
using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Services;
using PRN_Project_Summer_2024.Util;

namespace PRN_Project_Summer_2024.ViewModels;

public class LoginPageViewModel : ViewModel
{
    private AuthenService authService;
    private string username;
    private string password;
    private string errorMessage;
    public ObservableCollection<User> Users { get; set; }
    public string ErrorMessage { get => errorMessage; set => SetField(ref errorMessage,value); }
    public ICommand LoginCommand { get; set; }
    public ICommand ToSignupCommand { get; set; }
    public string Username
    {
        get => username;
        set
        {
            SetField(ref username, value);
        }
    }
    public string Password 
    { 
        get => password;
        set
        {
            SetField(ref password, value);
        }
    }
    public LoginPageViewModel(Navigation navigation)
    {
        username = "";
        password = "";
        ErrorMessage = "Fill in all the fields";
        authService = new AuthenService();
        LoginCommand = new BaseCommand(()=>Login(navigation));
        ToSignupCommand = new BaseCommand(()=>ToSignup(navigation));
    }
    void Login(Navigation navigation)
    {
        User u = authService.login(username,password);
        if (u == null)
        {
            ErrorMessage = "Wrong credentials.";
            return;
        }
        else
        {
            navigation.ViewModel = new DashBoardViewModel(u, navigation);
        }
    }
    void ToSignup(Navigation navigation)
    {
        navigation.ViewModel = new SignupViewModel(navigation);
    }
}