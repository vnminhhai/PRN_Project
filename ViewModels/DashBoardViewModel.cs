using System.Windows.Input;
using PRN_Project_Summer_2024.Command;
using PRN_Project_Summer_2024.Util;

namespace PRN_Project_Summer_2024.ViewModels;

public class DashBoardViewModel:ViewModel
{
    public ICommand ViewTasks { get; set; }
    public ICommand AddTask { get; set; }
    public ICommand UpdateTaskStatus { get; set; }
    public DashBoardViewModel(Navigation navigation)
    {
        ViewTasks = new BaseCommand(()=> { });
        AddTask = new BaseCommand(()=> { });
        UpdateTaskStatus = new BaseCommand(()=> { });
    }
}