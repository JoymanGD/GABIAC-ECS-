using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct TranslationComponent
    {
        public float TranslationSpeed { get; private set; }
        public TranslationType CurrentType { get; private set; }

        public TranslationComponent(float _movementSpeed, TranslationType _currentType){
            TranslationSpeed = _movementSpeed;
            CurrentType = _currentType;
        }

        public enum TranslationType{
            ApplyForce, TranslatePosition
        }
    }
}