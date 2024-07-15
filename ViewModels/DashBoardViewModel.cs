using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Services;
using PRN_Project_Summer_2024.Util;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.ViewModels;

public class DashBoardViewModel:ViewModel
{
    private User CurrentUser;
    private List<Task> Tasks;
    private DateOnly SelectedDate;

    public List<Task> TaskList
    {
        get => Tasks;
        set => SetField(ref Tasks, value);
    }
    public ICommand ViewTasks { get; set; }
    public ICommand AddTask { get; set; }
    public ICommand RemoveTask { get; set; }
    public ICommand UpdateTaskStatus { get; set; }
    public string CurrentUserGreet => $"Hello, {CurrentUser.FullName}";
    public DashBoardViewModel(User u, Navigation navigation)
    {
        CurrentUser = u;
        SelectedDate = DateOnly.FromDateTime(DateTime.Now);
        Tasks = new TaskService().GetTasksByDate(u.Name, SelectedDate);
        ViewTasks = new BaseCommand(()=> { });
        AddTask = new BaseCommand(() =>
        {
            navigation.ViewModel = new AddTaskViewModel(u, SelectedDate, navigation);
        });
        RemoveTask = new BaseCommand(()=> { });
        UpdateTaskStatus = new BaseCommand(()=> { });
    }
}