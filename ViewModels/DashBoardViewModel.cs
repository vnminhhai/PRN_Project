using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Util;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.ViewModels;

public class DashBoardViewModel:ViewModel
{
    private User CurrentUser;
    private List<Task> Tasks;
    private DateOnly SelectedDate;
    public ICommand ViewTasks { get; set; }
    public ICommand AddTask { get; set; }
    public ICommand UpdateTaskStatus { get; set; }
    public string CurrentUserGreet => $"Hello, {CurrentUser.FullName}";
    public DashBoardViewModel(User u, Navigation navigation)
    {
        CurrentUser = u;
        SelectedDate = DateOnly.FromDateTime(DateTime.Now);
        Tasks = new List<Task>();
        ViewTasks = new BaseCommand(()=> { });
        AddTask = new BaseCommand(()=> { });
        UpdateTaskStatus = new BaseCommand(()=> { });
    }
}