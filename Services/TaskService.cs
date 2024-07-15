using Microsoft.EntityFrameworkCore;
using PRN_Project_Summer_2024.Models;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.Services;

public class TaskService
{
    private PrnProjectSummer2024Context context = PrnProjectSummer2024Context.Context;
    public List<Task> GetTasksByDate(string username, DateOnly date)
    {
        return context.Tasks.Where(t => t.Username.Equals(username) && DateOnly.FromDateTime(t.Time).Equals(date)).ToList();
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
    
    public void UpdateStatusById(int id)
    {
        Task? t = context.Tasks.Find(id);
        if (t != null)
        {
            t.Status = t.Status.Equals("Done") ? "Not Done" : "Done";
        }
        context.SaveChanges();
    }
    public Task? GetTaskById(int id)
    {
        return context.Tasks.Include(t=>t.Steps)
            .Include(t=>t.RepeatSchedule)
            .SingleOrDefault(t=>t.Id==id);
    }
}