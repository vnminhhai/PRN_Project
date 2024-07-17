using System.Windows;
using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Services;
using PRN_Project_Summer_2024.Util;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.ViewModels;

public class DashBoardViewModel:ViewModel
{
    private User _currentUser;
    private List<Task> _tasks;
    private DateOnly _selectedDate;
    private string _greeting;
    private Task _selectedTask;
    public Task SelectedTask
    {
        get => _selectedTask;
        set
        {
            _selectedTask = value;
        }
    }
    public DateOnly SelectedDay
    {
        get => _selectedDate;
        set
        {
            CurrentUserGreet=$"Hello, {_currentUser.FullName}\nHere are your tasks for {value.ToString()}";
            SetField(ref _selectedDate, value);
            TaskList = new TaskService().GetTasksByDate(_currentUser.Name, value);
        }
    }

    public List<Task> TaskList
    {
        get => _tasks;
        set => SetField(ref _tasks, value);
    }
    public ICommand Prev { get; set; }
    public ICommand AddTask { get; set; }
    public ICommand Next { get; set; }
    public ICommand UpdateTaskStatus { get; set; }
    
    public ICommand ToDetail { get; set; }

    public ICommand Logout { get; set; }
    public string CurrentUserGreet
    {
        get => _greeting;
        set => SetField(ref _greeting, value);
    }

    public DashBoardViewModel(User u, Navigation navigation)
    {
        _currentUser = u;
        _selectedDate = DateOnly.FromDateTime(DateTime.Now);
        _tasks = new TaskService().GetTasksByDate(u.Name, _selectedDate);
        _greeting=$"Hello, {_currentUser.FullName}\nHere are your tasks for {_selectedDate.ToString()}";
        ToDetail = new BaseCommand(
            () =>
            {
                if (_selectedTask==null) {MessageBox.Show("Pick a task first"); return;}
                navigation.ViewModel = new TaskDetailViewModel(_selectedTask.Id, u, navigation);
            });
        Logout = new BaseCommand(() =>
        {
            navigation.ViewModel = new LoginPageViewModel(navigation);
        });
        UpdateTaskStatus = new ParamCommand((taskId) =>
        {
            if (_selectedTask==null) {MessageBox.Show("Pick a task first"); return;}
            new TaskService().UpdateStatusById(_selectedTask.Id);
            TaskList = new TaskService().GetTasksByDate(u.Name, _selectedDate);
        }, (o) => true);
        Prev = new BaseCommand(() =>
        {
            SelectedDay = _selectedDate.AddDays(-1);
        });
        AddTask = new BaseCommand(() =>
        {
            navigation.ViewModel = new AddTaskViewModel(u, _selectedDate, navigation);
        });
        Next = new BaseCommand(()=> {
            SelectedDay = _selectedDate.AddDays(1);
        });
    }
}