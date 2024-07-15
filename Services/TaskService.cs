using Microsoft.EntityFrameworkCore;
using PRN_Project_Summer_2024.Models;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.Services;

public class TaskService
{
    private PrnProjectSummer2024Context context = PrnProjectSummer2024Context.Context;
    public List<Task> GetTasksByDate(string username, DateOnly date)
    {
        return context.Tasks.Where(t => t.Username.Equals(username)).ToList();
    }
    
    public void AddTask(Task t)
    {
        // context.RepeatSchedules.Add(t.RepeatSchedules);
        context.Tasks.Add(t);
        context.SaveChanges();
    }
    
    public void RemoveTask(Task t)
    {
        context.Tasks.Remove(t);
        context.SaveChanges();
    }
}