using DefaultEcs;
using Gabiac.Scripts.ECS.Components;

namespace Gabiac.Scripts.ECS.Systems.Input
{
    public interface IInputSystem
    {
        void Update(Entity _entity);
    }
}