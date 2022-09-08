namespace PAD.Finance.Domain.DTO;

public class TableStateDTO
{
    public string SearchString { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public string SortLabel { get; set; }

    public SortDirection SortDirection { get; set; }
}