using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Services;
using PRN_Project_Summer_2024.Util;
using Task = PRN_Project_Summer_2024.Models.Task;
namespace PRN_Project_Summer_2024.ViewModels;

public class TaskDetailViewModel:ViewModel
{
    private Task _task;
    private string _title;
    private string _description;
    private string _status;
    private DateTime _time;
    private string _repeat;
    private string _priority;
    private Step _selectedStep;
    private List<Step> _steps;

    public string Title
    {
        get { return _task.Title; }
        set { SetField(ref _title, value); }
    }

    public string Description
    {
        get { return _task.Detail; }
        set { SetField(ref _description, value); }
    }

    public string Status
    {
        get { return _task.Status; }
        set { SetField(ref _status, value); }
    }

    public DateTime Time
    {
        get { return _task.Time; }
        set { SetField(ref _time, value); }
    }

    public string Repeat
    {
        get { return _task.RepeatSchedule.ToString(); }
        set { SetField(ref _repeat, value); }
    }

    public string Priority
    {
        get { return _task.Priority; }
        set { SetField(ref _priority, value); }
    }

    public List<Step> Steps
    {
        get { return _steps; }
        set { SetField(ref _steps, value); }
    }

    public ICommand ToDashBoard { get; set; }
    public ICommand AddStep { get; set; }
    public ICommand RemoveStep { get; set; }
    
    public ICommand SetRep { get; set; }
    public Task Task
    {
        get => _task;
        set {SetField(ref _task, value);}
    }
    
    public Step SelectedStep
    {
        get => _selectedStep;
        set => SetField(ref _selectedStep, value);
    }
    public TaskDetailViewModel(int taskId, User u, Navigation navigation)
    {
        ToDashBoard = new BaseCommand(() =>
        {
            PrnProjectSummer2024Context.Context.SaveChanges();
            navigation.ViewModel = new DashBoardViewModel(u,navigation);
        });
        _task = new TaskService().GetTaskById(taskId);
        _steps = _task.Steps.ToList();
        AddStep = new BaseCommand(() =>
        {
            Step newStep = new Step
            {
                TaskId = _task.Id,
                Number = _task.Steps.Count + 1,
                Name = "New Step "+(Steps.Count+1),
                Status = "Not Done"
            };
            _task.Steps.Add(newStep);
            Steps = _task.Steps.ToList();
            PrnProjectSummer2024Context.Context.SaveChanges();
        });
        RemoveStep = new BaseCommand(() =>
        {
            _task.Steps.Remove(_task.Steps.First(s => s.Number == _selectedStep.Number && s.TaskId == _selectedStep.TaskId));
            foreach (Step s in _task.Steps.OrderBy(s=>s.Number))  
            {
                if (s.Number > _selectedStep.Number)
                {
                    s.Number--;
                }
            }
            Steps = _task.Steps.ToList();
            PrnProjectSummer2024Context.Context.SaveChanges();
        });
        SetRep = new BaseCommand(() =>
        {
            navigation.ViewModel = new RepeatScheduleEditViewModel(u, taskId, navigation);
        });
    }
}