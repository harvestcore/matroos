namespace Matroos.Resources.Classes.API;

public class SuccessResponse
{
    /// <summary>
    /// Whether the response is successful or not.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="success">Whether the response is successful or not.</param>
    public SuccessResponse(bool success)
    {
        Success = success;
    }
}
