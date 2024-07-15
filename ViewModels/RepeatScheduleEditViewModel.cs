using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Util;

namespace PRN_Project_Summer_2024.ViewModels;

public class RepeatScheduleEditViewModel:ViewModel
{
    private string _type;
    private int _hour;
    private string _weekDay;
    private int _day;
    private string _hourVisibility;
    private string _weekDayVisibility;
    private string _dayVisibility;
    private string[] _repeatTypes = new[] {"Daily", "Weekly", "Monthly"};
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
    
    public string Type
    {
        get => _type;
        set
        {
            SetField(ref _type, value);
        }
    }
    public int Hour
    {
        get => _hour;
        set
        {
            SetField(ref _hour, value);
        }
    }
    public string MonthDay
    {
        get => _day.ToString();
        set
        {
            if (int.TryParse(value, out int day))
            {
                SetField(ref _day, day);
            }
        }
    }

    public string WeekDay
    {
        get => _weekDay;
        set {SetField(ref _weekDay, value);}
    }
    
    public string[] RepeatTypes => _repeatTypes;
    public string[] WeekDays => _weekDays;

    public RepeatScheduleEditViewModel(User u, Navigation navigation)
    {
        
    }
}