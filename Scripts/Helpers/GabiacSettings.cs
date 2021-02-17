using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Gabiac.Scripts.Helpers
{
    public static class GabiacSettings
    {
        public static SpriteBatch spriteBatch;
        public static GraphicsDevice graphicsDevice;
        public static GraphicsDeviceManager graphics;
        public static ContentManager contentManager;
        public static Game game;
        public static DefaultEcs.World world;
        public static VelcroPhysics.Dynamics.World physicWorld;
        public static IParallelRunner mainRunner;
    }
}