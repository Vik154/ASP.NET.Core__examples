using Microsoft.AspNetCore.Mvc;
namespace _08_VIEW_COMPONENTS.Components;

[ViewComponent]
public class DependencyTimer {

    ITimeService _timeService;
    public DependencyTimer(ITimeService time) => _timeService = time;
    public string Invoke() => $"Текущее время: {_timeService.GetTime()}";
}
