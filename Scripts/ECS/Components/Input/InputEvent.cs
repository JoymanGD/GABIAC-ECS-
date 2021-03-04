using System;

namespace Gabiac.Scripts.ECS.Components.Input
{
    public interface InputEvent
    {
        EventArgs EventArgs { get; set; }
    }
}