namespace PAD.Client.Pages;

[Authorize]
[Route("finance/dashboard")]
public partial class Dashboard
{
    private int Index = -1;
    public double[] data = { 50, 25, 20, 5 };
    public string[] labels = { "Fossile", "Nuclear", "Solar", "Wind" };
}