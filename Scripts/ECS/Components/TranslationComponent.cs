using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gabiac.Scripts.ECS.Components
{
    public struct TranslationComponent
    {
        public float TranslationSpeed { get; private set; }

        public TranslationComponent(float _movementSpeed){
            TranslationSpeed = _movementSpeed;
        }
    }
}