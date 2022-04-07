using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using SaibaData.Blazor.Components.Services;
using System.Xml.Linq;

namespace SaibaData.Blazor.Components;
public class SbrSvg  : ComponentBase
{
    [Inject] private AssemblyResourceLocator Locator { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? OtherAttributes { get; set; }

    private MarkupString _markupString = new();

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        builder.AddContent(0, _markupString);
    }

    [Parameter] public string? ResourceName { get; set; }

    protected override void OnParametersSet()
    {
        var raw = Locator.GetResource(ResourceName!);
        if (OtherAttributes == null || !OtherAttributes.Any())
        {
            _markupString = new MarkupString(raw);
            return;
        }

        var xml = XDocument.Parse(raw);

        foreach (var item in OtherAttributes)
        {
            //xml.SetAttributeOnRoot(item.Key, item!.Value!.ToString());
            xml.Root!.SetAttributeValue(item.Key, item!.Value!.ToString());
        }
        _markupString = new MarkupString(xml.ToString());
    }
}
