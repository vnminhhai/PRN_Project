using System.Windows.Forms.VisualStyles;
using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Repositories;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.Services;

public class RepeatScheduleService
{
    private const int REPETITION_TIMES = 50;
    private PrnProjectSummer2024Context context = PrnProjectSummer2024Context.Context;
    RepeatScheduleRepository _repeatScheduleRepository = new();
    
    public void UpdateScheduleByTaskId(int taskId, RepeatSchedule repeatSchedule)
    {
        Task task = context.Tasks.Find(taskId);
        task.RepeatSchedule = repeatSchedule;
        RepeatSchedule? MySchedule = _repeatScheduleRepository.GetRepeatSchedulesByRules(repeatSchedule);
        if (MySchedule == null)
        {
            if (!repeatSchedule.RepeatCycle.Equals("None")) _repeatScheduleRepository.AddRepeatSchedule(repeatSchedule);
        }
        MySchedule = _repeatScheduleRepository.GetRepeatSchedulesByRules(repeatSchedule);
        task.RepeatSchedule = MySchedule;
        task.RepeatScheduleId = MySchedule?.Id??null;
        if (repeatSchedule.RepeatCycle.Equals("None"))
        {
            DeleteScheduledFutureTask(taskId);
        }
        else GenerateTask(taskId, repeatSchedule);
        context.SaveChanges();
    }

    public void DeleteScheduledFutureTask(int taskId)
    {
        Task selectedTask = context.Tasks.Find(taskId);
        context.Tasks.RemoveRange(context.Tasks
            .Where(t => t.Title.Equals(selectedTask.Title) &&
                        t.Username.Equals(selectedTask.Username) && t.Time > selectedTask.Time));
        context.SaveChanges();
    }
    public void GenerateTask(int taskId, RepeatSchedule repeatSchedule)
    {
        Task task = context.Tasks.Find(taskId);
        for (int i = 1; i < REPETITION_TIMES; i++)
        {
            DateTime newTime;
            if (repeatSchedule.RepeatCycle == "Daily")
            {
                newTime = task.Time.AddDays(i);
            }
            else if (repeatSchedule.RepeatCycle == "Weekly")
            {
                newTime = task.Time.AddDays(i*7);
            }
            else
            {
                newTime = task.Time.AddMonths(i);
            }
            Task newTask = new Task()
            {
                Username = task.Username,
                Title = task.Title,
                Detail = task.Detail,
                Time = newTime,
                Status = "Not done",
                Priority = task.Priority,
                RepeatSchedule = repeatSchedule,
                RepeatScheduleId = repeatSchedule.Id
            };
            context.Tasks.Add(newTask);
        }
        context.SaveChanges();
    }
}