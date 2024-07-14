using System.Windows.Input;

namespace PRN_Project_Summer_2024.Command;

public class BaseCommand : ICommand
{
    private Action _execute;
    private Func<bool> _canExecute;
    public event EventHandler? CanExecuteChanged;
    public BaseCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute();
    }

    public void Execute(object? parameter)
    {
        _execute();
    }

    public void RaiseCanExecuteChanged() { 
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}