using PRN_Project_Summer_2024.Models;
using PRN_Project_Summer_2024.Repositories;
using Task = PRN_Project_Summer_2024.Models.Task;

namespace PRN_Project_Summer_2024.Services;

public class RepeatScheduleService
{
    private PrnProjectSummer2024Context context = PrnProjectSummer2024Context.Context;
    RepeatScheduleRepository _repeatScheduleRepository = new();
    
    public void UpdateScheduleByTaskId(int taskId, RepeatSchedule repeatSchedule)
    {
        Task task = context.Tasks.Find(taskId);
        task.RepeatSchedule = repeatSchedule;
        RepeatSchedule? MySchedule = _repeatScheduleRepository.GetRepeatSchedulesByRules(repeatSchedule);
        if (MySchedule == null)
        {
            _repeatScheduleRepository.AddRepeatSchedule(repeatSchedule);
        }
        MySchedule = _repeatScheduleRepository.GetRepeatSchedulesByRules(repeatSchedule);
        task.RepeatSchedule = MySchedule;
        task.RepeatScheduleId = MySchedule.Id;
        context.SaveChanges();
    }
}