namespace Matroos.Backend.Core.Classes;

public class QueryFilter
{
    /// <summary>
    /// Documents to be skip.
    /// </summary>
    public int Skip { get; set; } = 0;

    /// <summary>
    /// Amount of documents to be fetched.
    /// </summary>
    public int Limit { get; set; } = 0;

    /// <summary>
    /// The search term.
    /// </summary>
    public string? SearchTerm { get; set; } = string.Empty;

    /// <summary>
    /// Fields where the SearchTerm will be searched.
    /// </summary>
    public List<string> SearchFields { get; set; } = new List<string>();
}
