using Microsoft.AspNetCore.Mvc;
namespace _08_VIEW_COMPONENTS.Components;

[ViewComponent]
public class Timer {

    public string Invoke(bool includeSeconds, bool format24) {

        string time;
        DateTime now = DateTime.Now;

        if (format24)   // если 24-часовой формат
            time = now.ToString("HH:mm");
        else           // если 12-часовой формат
            time = now.ToString("hh:mm");

        if (includeSeconds) // если надо добавить секунды
            time = $"{time}:{now.Second}";

        return $"Текущее время: {time}";
        //if (includeSeconds)
        //    return $"Текущее время: {DateTime.Now.ToString("hh:mm:ss")}";
        //else
        //    return $"Текущее время: {DateTime.Now.ToString("hh:mm")}";
    }
}