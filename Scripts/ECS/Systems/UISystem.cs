using DefaultEcs;
using DefaultEcs.System;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Utilities;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Ball))]
    
    public partial class UISystem : AEntitySetSystem<float>
    {
        private World world;
        
        public UISystem(World _world) : base(_world){
            world = _world;
        }

        [Update]
        private void Update(){
            
        }
    }
}