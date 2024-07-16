using PRN_Project_Summer_2024.Models;

namespace PRN_Project_Summer_2024.Repositories;

public class RepeatScheduleRepository
{
    public RepeatSchedule? GetRepeatSchedulesByRules(RepeatSchedule repeatSchedule)
    {
        return PrnProjectSummer2024Context.Context.RepeatSchedules
            .SingleOrDefault(rs=>rs.Equals(repeatSchedule));
    }
    
    public void AddRepeatSchedule(RepeatSchedule repeatSchedule)
    {
        foreach (RepeatSchedule rs in PrnProjectSummer2024Context.Context.RepeatSchedules)
        {
            if (rs.Equals(repeatSchedule))
            {
                return;
            }
        }
        PrnProjectSummer2024Context.Context.RepeatSchedules.Add(repeatSchedule);
        PrnProjectSummer2024Context.Context.SaveChanges();
    }
}