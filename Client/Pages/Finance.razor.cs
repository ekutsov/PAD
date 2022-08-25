using Microsoft.AspNetCore.Authorization;

namespace PAD.Client.Pages;

[Authorize]
public partial class Finance
{
    public bool showDataLabels = false;

    public DataItem[] revenue = new DataItem[] {
        new DataItem {
            Quarter = "Q1",
            Revenue = 30000
        },
        new DataItem {
            Quarter = "Q2",
            Revenue = 40000
        },
        new DataItem {
            Quarter = "Q3",
            Revenue = 50000
        },
        new DataItem
        {
            Quarter = "Q4",
            Revenue = 80000
        },
    };
}

public class DataItem
{
    public string Quarter { get; set; }

    public double Revenue { get; set; }
}