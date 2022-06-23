namespace Matroos.Resources.Classes.API;

public class SuccessResponse
{
    public bool Success { get; set; }

    public SuccessResponse(bool success)
    {
        Success = success;
    }
}
