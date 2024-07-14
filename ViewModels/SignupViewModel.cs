using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Services;
using PRN_Project_Summer_2024.Util;

namespace PRN_Project_Summer_2024.ViewModels;

public class SignupViewModel:ViewModel
{
    private AuthenService authService;
    private string username;
    private string password;
    private string fullname;
    private string password2;
    private DateOnly birthdate;
    private string errorMessage;
    public ObservableCollection<User> Users { get; set; }
    public string ErrorMessage { get => errorMessage; set => SetField(ref errorMessage,value); }
    public ICommand SignupCommand { get; set; }
    
    public ICommand ToLoginCommand { get; set; }
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
    public SignupViewModel(Navigation navigation)
    {
        username = "";
        password = "";
        password2 = "";
        fullname = "";
        ErrorMessage = "";
        authService = new AuthenService();
        ToLoginCommand = new BaseCommand(()=>navigation.ViewModel = new LoginPageViewModel(navigation));
        SignupCommand = new BaseCommand(()=>Signup(navigation));
    }
    void Signup(Navigation navigation)
    {
        MessageBox.Show(username +" "+birthdate+" "+fullname+" "+password+" "+password2);
        User u = new User();
        u.Name = username;
        u.Password = password;
        u.FullName = fullname;
        u.BirthDate = birthdate;
        try
        {
            authService.signup(u);
        }
        catch (Exception e)
        {
            ErrorMessage = "Username already exists.";
        }
        navigation.ViewModel = new DashBoardViewModel(u,navigation);
    }
}