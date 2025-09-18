using ELearningBlazor.Models;

namespace ELearningBlazor.Services;

public class NavigationService
{
    public static List<HeaderItem> GetHeaderItems()
    {
        return new List<HeaderItem>
        {
            new() { Label = "Home", Href = "/" },
            new() { Label = "Courses", Href = "/#courses" }
        };
    }
}