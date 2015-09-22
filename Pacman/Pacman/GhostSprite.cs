using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Pacman
{
    public class GhostSprite : CharacterSprite
    {
        private static Random random = new Random();

        public GhostSprite(Game1 game, Texture2D p0, Texture2D p1, int screenWidth, int screenHeight, int velocity, Color color)
            : base(game, p0, p1, screenWidth, screenHeight, velocity, color)
        {
            position = new Vector2(
                random.Next(0 + (int)Math.Ceiling(size.X / 2), screenWidth - (int)Math.Ceiling(size.X / 2)),
                random.Next(0 + (int)Math.Ceiling(size.Y / 2), screenHeight - (int)Math.Ceiling(size.Y / 2))
            ) / size * size - size / 2;
        }

        public static GhostSprite create(Game1 game, GraphicsDeviceManager graphics, Color color)
        {
            return new GhostSprite(game, game.Content.Load<Texture2D>("Sprites/ghost0"), game.Content.Load<Texture2D>("Sprites/ghost1"),
                graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 2, color);
        }

        public void Update(GameTime gameTime)
        {
            if (canMove(direction))
                ChangeDirection();
            if (gameTime.TotalGameTime.Milliseconds % random.Next(2000, 5000) == 0)
                ChangeDirection();

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        private void ChangeDirection()
        {
            Array values = Enum.GetValues(typeof(Direction));
            direction = (Direction)values.GetValue(random.Next(values.Length));
        }


    }
}
