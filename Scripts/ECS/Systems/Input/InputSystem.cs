using VelcroPhysics.Utilities;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using Gabiac.Scripts.ECS.Components.Input;
using Gabiac.Scripts.ECS.Systems.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(InputComponent))]
    [With(typeof(Transform))]
    [With(typeof(RotationComponent))]
    public partial class InputSystem : AEntitySetSystem<GameTime>
    {
        private IParallelRunner runner;
        private World world;
        private List<IInputSystem> InputSystems = new List<IInputSystem>();
        
        public InputSystem(World _world, IParallelRunner _runner) : base(_world, CreateEntityContainer, null, 0){
            world = _world;
            runner = _runner;
            InputSystems.Add(new MouseInputSystem());
            InputSystems.Add(new KeyboardInputSystem());
        }

        [Update]
        private void Update(in Entity _entity, in Transform _transform, ref RotationComponent _rotationComponent){
            foreach (var system in InputSystems)
            {
                system.Update(_entity);
            }
        }
    }
} 