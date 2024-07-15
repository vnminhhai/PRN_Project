using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Services;
using PRN_Project_Summer_2024.Util;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.ViewModels;

public class AddTaskViewModel:ViewModel
{
    private User CurrentUser;
    private string Date;
    private DateOnly SelectedDate;
    private string Title;
    private string Detail;
    private string Priority;
    private DateTime Time;

    public string DayText
    {
        get => Date;
        set => SetField(ref Date, value);
    }
    public string TaskTitle
    {
        get => Title;
        set => SetField(ref Title, value);
    }
    public string TaskDetail
    {
        get => Detail;
        set => SetField(ref Detail, value);
    }
    public string TaskPriority
    {
        get => Priority;
        set => SetField(ref Priority, value);
    }
    public DateTime TaskTime
    {
        get => Time;
        set => SetField(ref Time, value);
    }
    public ICommand Cancel { get; set; }
    public ICommand AddTask { get; set; }
    public string CurrentUserGreet => $"Hello, {CurrentUser.FullName}";
    public AddTaskViewModel(User u, DateOnly date, Navigation navigation)
    {
        CurrentUser = u;
        SelectedDate = date;
        DayText = "Add a task for day " + SelectedDate.ToString();
        Cancel = new BaseCommand(() =>
        {
            navigation.ViewModel=new DashBoardViewModel(u, navigation);
        });
        AddTask = new BaseCommand(() =>
        {
            Task t = new Task();
            t.Title = Title;
            t.Detail = Detail;
            t.Priority = Priority;
            t.Time = Time;
            t.Username = CurrentUser.Name;
            new TaskService().AddTask(t);
            navigation.ViewModel=new DashBoardViewModel(u, navigation);
        });
    }
}