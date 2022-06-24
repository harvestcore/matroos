namespace Matroos.Resources.Classes.API;

public class ItemsResponse<TValue>
{
    /// <summary>
    /// The count of items.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// The list of items.
    /// </summary>
    public List<TValue>? Items { get; set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="items">The list of items.</param>
    public ItemsResponse(List<TValue> items)
    {
        Items = items;
        Count = items.Count;
    }
}
