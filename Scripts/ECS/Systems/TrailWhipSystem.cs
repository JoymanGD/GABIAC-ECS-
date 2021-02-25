using VelcroPhysics.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DefaultEcs.System;
using DefaultEcs;
using DefaultEcs.Threading;
using Gabiac.Scripts.ECS.Components;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Dynamics.Joints;
using MonoGame.Extended;

namespace Gabiac.Scripts.ECS.Systems
{
    [With(typeof(Trail))]
    [With(typeof(TrailWhip))]
    public partial class TrailWhipSystem : AEntitySetSystem<float>
    {
        DefaultEcs.World world;
        VelcroPhysics.Dynamics.World physicWorld;
        
        public TrailWhipSystem(DefaultEcs.World _world, VelcroPhysics.Dynamics.World _physicWorld) : base(_world){
            world = _world;
            physicWorld = _physicWorld;
        }

        [Update]
        private void Update(ref Trail _trail, in PhysicBody _physicBody, in Entity _entity){
            if(!_trail.Whipped){
                _entity.Remove<DoTheTrail>();
                SetWhip(ref _trail, _physicBody);
            }
        }

        void SetWhip(ref Trail _trail, PhysicBody _physicBody){
            var list = _trail.TrailPoints;
            var firstNode = list.First;
            // var firstBody = firstNode.Value;
            // var firstAnchor = Vector2.Lerp(_physicBody.Position(), ConvertUnits.ToDisplayUnits(firstBody.Position), .5f);
            // var firstRj = new RopeJoint(_physicBody.Body, firstBody, firstAnchor, firstAnchor);
            // physicWorld.AddJoint(firstRj);

            var currentNode = firstNode;
            
            while(currentNode != null){
                currentNode.Value.BodyType = BodyType.Dynamic;
                var prevNode = currentNode.Previous;

                if(prevNode == null) prevNode = new LinkedListNode<Body>(_physicBody.Body);

                var anchor = Vector2.Lerp(currentNode.Value.Position, prevNode.Value.Position, .5f);
                var rj = new DistanceJoint(prevNode.Value, currentNode.Value, anchor, anchor);
                physicWorld.AddJoint(rj);
                currentNode.Value.Mass = .001f;
                currentNode = currentNode.Next;
            }

            _trail.Whipped = true;
        }
    }
}