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
    private string greeting;

    public DateOnly SelectedDay
    {
        get => SelectedDate;
        set => SetField(ref SelectedDate, value);
    }
    public List<Task> TaskList
    {
        get => Tasks;
        set => SetField(ref Tasks, value);
    }
    public ICommand Prev { get; set; }
    public ICommand AddTask { get; set; }
    public ICommand Next { get; set; }
    public ICommand UpdateTaskStatus { get; set; }

    public string CurrentUserGreet
    {
        get => greeting;
        set => SetField(ref greeting, value);
    }

    public DashBoardViewModel(User u, Navigation navigation)
    {
        CurrentUser = u;
        SelectedDate = DateOnly.FromDateTime(DateTime.Now);
        Tasks = new TaskService().GetTasksByDate(u.Name, SelectedDate);
        greeting=$"Hello, {CurrentUser.FullName}\nHere are your tasks for {SelectedDate.ToString()}";
        Prev = new BaseCommand(() =>
        {
            SelectedDay = SelectedDate.AddDays(-1);
            CurrentUserGreet=$"Hello, {CurrentUser.FullName}\nHere are your tasks for {SelectedDate.ToString()}";
            TaskList = new TaskService().GetTasksByDate(u.Name, SelectedDate);
        });
        AddTask = new BaseCommand(() =>
        {
            navigation.ViewModel = new AddTaskViewModel(u, SelectedDate, navigation);
        });
        Next = new BaseCommand(()=> {
            SelectedDay = SelectedDate.AddDays(1);
            CurrentUserGreet=$"Hello, {CurrentUser.FullName}\nHere are your tasks for {SelectedDate.ToString()}";
            TaskList = new TaskService().GetTasksByDate(u.Name, SelectedDate);
        });
        UpdateTaskStatus = new BaseCommand(()=> { });
    }
}