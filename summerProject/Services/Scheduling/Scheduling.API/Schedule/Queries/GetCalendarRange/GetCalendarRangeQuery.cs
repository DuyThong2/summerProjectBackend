using Scheduling.API.Dto;

namespace Scheduling.API.Schedule.Queries.GetCalendarRange
{
    public record GetCalendarRangeQuery(string UserId, DateTime From, DateTime To)
   : IQuery<GetCalendarRangeResult>;

    public record GetCalendarRangeResult(string UserId, DateTime From, DateTime To, List<CalendarDayDto> Days);
}
