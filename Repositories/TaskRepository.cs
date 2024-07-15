using Microsoft.EntityFrameworkCore;
using PRN_Project_Summer_2024.Models;
using Task = PRN_Project_Summer_2024.Models.Task;


namespace PRN_Project_Summer_2024.Repositories;

public class TaskRepository
{
    private PrnProjectSummer2024Context context = PrnProjectSummer2024Context.Context;
    
    public List<Task> GetTasksByUsername(string username)
    {
        return context.Tasks.Include(t=>t.RepeatSchedule).Where(t => t.Username == username).ToList();
    }
    
    public void UpdateTaskStatus(int id, string username, string status)
    {
        foreach (Task t in context.Tasks.Include(t=>t.RepeatSchedule).Where(t => t.Id == id && t.Username.Equals(username)))
        {
            t.Status = status;
        }
    }
}