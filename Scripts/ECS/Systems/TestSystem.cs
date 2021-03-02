using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components.UI;
using Gabiac.Scripts.ECS.Components;
using MonoGame.Extended.Input;
using Microsoft.Xna.Framework.Input;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Test))]
    public partial class TestSystem : AEntitySetSystem<float>
    {
        private World world;
        
        public TestSystem(World _world) : base(_world){
            world = _world;
        }

        [Update]
        private void Update(in Entity _entity){
        }
    }
}