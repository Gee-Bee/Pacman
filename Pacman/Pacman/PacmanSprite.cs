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
    class PacmanSprite : CharacterSprite
    {

        public Direction inputDirection;
        private int defaultVelocity;
        private float rotation;

        public PacmanSprite(Texture2D p0, Texture2D p1, Vector2 p, Vector2 s, int screenWidth, int screenHeight, int velocity) : base(p0, p1, p, s, screenWidth, screenHeight, velocity)
        {
            inputDirection = direction;
            defaultVelocity = velocity;
            rotation = 0;
        }

        public static PacmanSprite create(Game game, GraphicsDeviceManager graphics)
        {
            PacmanSprite pacmanSprite = new PacmanSprite(game.Content.Load<Texture2D>("Sprites/pacman0"), game.Content.Load<Texture2D>("Sprites/pacman1"), 
                Vector2.Zero, new Vector2(40f, 40f),
                graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 2);
            return pacmanSprite;
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
                inputDirection = Direction.Left;
            if (keyboardState.IsKeyDown(Keys.Right))
                inputDirection = Direction.Right;
            if (keyboardState.IsKeyDown(Keys.Down))
                inputDirection = Direction.Down;
            if (keyboardState.IsKeyDown(Keys.Up))
                inputDirection = Direction.Up;

            if (CanMove(inputDirection))
                direction = inputDirection;
            if (CanMove(direction)) {
                velocity = defaultVelocity;
               switch (direction)
                {
                    case Direction.Right: { rotation = 0; break; }
                    case Direction.Down:  { rotation = MathHelper.PiOver2; break; }
                    case Direction.Left:  { rotation = 2 * MathHelper.PiOver2; break; }
                    case Direction.Up:    { rotation = 3 * MathHelper.PiOver2; break; }
                }
            }
            else
                velocity = 0;

            base.Update(gameTime, keyboardState);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            base.Draw(gameTime, spriteBatch, rotation);
        }
        
        
        

    }
}
