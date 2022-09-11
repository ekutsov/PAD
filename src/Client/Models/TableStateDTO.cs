namespace PAD.Client.Models;

public class TableStateDTO
{
    public TableStateDTO(string searchString, DateRange dateRange, TableState state)
    {
        SearchString = searchString;
        StartDate = dateRange.Start.Value.ToString("MM/dd/yyyy");
        EndDate = dateRange.End.Value.ToString("MM/dd/yyyy");
        Page = state.Page.ToString();
        PageSize = state.PageSize.ToString();
        SortLabel = state.SortLabel ?? string.Empty;
        SortDirection = ((int)state.SortDirection).ToString();
    }

    public string SearchString { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public string Page { get; set; }

    public string PageSize { get; set; }

    public string SortLabel { get; set; }

    public string SortDirection { get; set; }
}