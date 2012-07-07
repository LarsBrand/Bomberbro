using Microsoft.Xna.Framework;

namespace Bomberbro.bomberman
{
    public interface IGameState
    {
        void Initialize();
        void LoadContent();
        void UnloadContent();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}