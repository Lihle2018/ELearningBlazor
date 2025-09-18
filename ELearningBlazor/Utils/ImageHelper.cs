namespace ELearningBlazor.Utils;

public static class ImageHelper
{
    public static string GetImagePath(string imagePath)
    {
        // In Blazor WASM, images are served from wwwroot
        return imagePath.StartsWith("/") ? imagePath : $"/{imagePath}";
    }
}