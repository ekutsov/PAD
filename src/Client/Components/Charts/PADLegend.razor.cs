using MudBlazor.Charts.SVG.Models;

namespace PAD.Client.Components.Charts;

public partial class PADLegend : PADChartBase
{
    [CascadingParameter] public PADChart MudChartParent { get; set; }
    [Parameter] public List<SvgLegend> Data { get; set; } = new List<SvgLegend>();
}