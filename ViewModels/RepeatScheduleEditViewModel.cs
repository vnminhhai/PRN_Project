using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Services;
using PRN_Project_Summer_2024.Util;

namespace PRN_Project_Summer_2024.ViewModels;

public class RepeatScheduleEditViewModel:ViewModel
{
    private string? _type;
    private int? _hour;
    private string? _weekDay;
    private int? _day;
    private string _hourVisibility;
    private string _weekDayVisibility;
    private string _dayVisibility;
    private string[] _repeatTypes = new[] {"None", "Daily", "Weekly", "Monthly"};
    private string[] _weekDays = new[] {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

    public string HourVisibility
    {
        get => _hourVisibility;
        set
        {
            SetField(ref _hourVisibility, value);
        }
    }
    public string WeekDayVisibility
    {
        get => _weekDayVisibility;
        set
        {
            SetField(ref _weekDayVisibility, value);
        }
    }
    public string DayVisibility
    {
        get => _dayVisibility;
        set
        {
            SetField(ref _dayVisibility, value);
        }
    }
    
    public string? Type
    {
        get => _type;
        set
        {
            SetField(ref _type, value=="None"?null:value);
            DayVisibility=value=="Monthly"?"Visible":"Hidden";
            HourVisibility =value=="Daily"?"Visible":"Hidden";
            WeekDayVisibility=value=="Weekly"?"Visible":"Hidden";
        }
    }
    public int? Hour
    {
        get => _hour;
        set
        {
            SetField(ref _hour, value);
        }
    }
    public int? MonthDay
    {
        get => _day;
        set
        {
            SetField(ref _day, value);
        }
    }

    public string WeekDay
    {
        get => _weekDay;
        set {SetField(ref _weekDay, value);}
    }
    
    public ICommand Confirm { get; set; }
    public ICommand Cancel { get; set; }
    public string[] RepeatTypes => _repeatTypes;
    public string[] WeekDays => _weekDays;

    public RepeatScheduleEditViewModel(User u, int taskId, Navigation navigation)
    {
        Confirm = new BaseCommand(() =>
        {
            RepeatSchedule rs = new RepeatSchedule();
            rs.RepeatCycle = Type??"None";
            if (Type?.Equals("Daily")??false) rs.Hour = Hour;
            if (Type?.Equals("Monthly")??false) rs.Day = MonthDay;
            if (Type?.Equals("Weekly")??false) rs.WeekDay = WeekDay;
            new RepeatScheduleService().UpdateScheduleByTaskId(taskId, rs);
            navigation.ViewModel = new TaskDetailViewModel(taskId, u, navigation);
        });
        Cancel= new BaseCommand(() =>
        {
            navigation.ViewModel = new TaskDetailViewModel(taskId, u, navigation);
        });
    }
}