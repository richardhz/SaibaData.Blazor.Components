using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaibaData.Blazor.Components;

public partial class SbrCard
{
    private bool Collapsed { get; set; } = false;

    [Parameter] public bool IsCollapsed { get; set; } = false;

    [Parameter] public bool Collapsible { get; set; } = false;

    [Parameter] public string? CssClass { get; set; }

    [Parameter] public bool HasBottomStyle { get; set; } = true;

    [Parameter] public bool HasTopStyle { get; set; } = true;

    [Parameter] public RenderFragment? Header { get; set; }

    [Parameter] public RenderFragment? Body { get; set; }

    [Parameter] public RenderFragment? Actions { get; set; }

    protected string ActiveCssClass => HasBottomStyle ? "footer" : "nofooter";
    protected string RoundBaseCssClass => "round-base";
    protected string HeaderCssClass => HasTopStyle && !Collapsed ? "header-base round-top header" : HasTopStyle && Collapsed ? $"header-base round-top header {RoundBaseCssClass}" : "header-base round-top header-blank";

    protected override void OnInitialized()
    {
        Collapsed = IsCollapsed;
    }

    private void OnShowHideClick()
    {
        Collapsed = !Collapsed;
    }
}
