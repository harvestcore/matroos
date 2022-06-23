namespace Matroos.Resources.Classes.API;

public class ItemsResponse<TValue>
{
    public int Count { get; set; }
    public List<TValue>? Items { get; set; }

    public ItemsResponse(List<TValue> items)
    {
        Items = items;
        Count = items.Count;
    }
}
