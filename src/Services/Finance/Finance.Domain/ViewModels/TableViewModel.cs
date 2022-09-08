namespace PAD.Finance.Domain.ViewModels;

public class TableViewModel<T>
{
    public TableViewModel(List<T> items, int totalItems)
    {
        Items = items;
        TotalItems = totalItems;
    }
    
    public List<T> Items { get; set; }
    public int TotalItems { get; set; }
}