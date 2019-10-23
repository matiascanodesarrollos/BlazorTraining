using Microsoft.AspNetCore.Components;

namespace EventSubscriptionApp
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
