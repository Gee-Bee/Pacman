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
    public class PacmanSprite : CharacterSprite
    {

        private int defaultVelocity;
        private float rotation;

        public PacmanSprite(Game1 game, Texture2D p0, Texture2D p1, int screenWidth, int screenHeight, int velocity)
            : base(game, p0, p1, screenWidth, screenHeight, velocity, Color.White)
        {
            defaultVelocity = velocity;
            rotation = 0;
        }

        public static PacmanSprite create(Game1 game, GraphicsDeviceManager graphics)
        {
            return new PacmanSprite(game, game.Content.Load<Texture2D>("Sprites/pacman0"), game.Content.Load<Texture2D>("Sprites/pacman1"),
                graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 2);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
                requestedDirection = Direction.Left;
            if (keyboardState.IsKeyDown(Keys.Right))
                requestedDirection = Direction.Right;
            if (keyboardState.IsKeyDown(Keys.Down))
                requestedDirection = Direction.Down;
            if (keyboardState.IsKeyDown(Keys.Up))
                requestedDirection = Direction.Up;

            base.Update(gameTime);

            if (game.gameState != GameState.GameOver)
            {

                if (canMove(direction))
                {
                    velocity = defaultVelocity;
                    switch (direction)
                    {
                        case Direction.Right: { rotation = 0; break; }
                        case Direction.Down: { rotation = MathHelper.PiOver2; break; }
                        case Direction.Left: { rotation = 2 * MathHelper.PiOver2; break; }
                        case Direction.Up: { rotation = 3 * MathHelper.PiOver2; break; }
                    }
                }
                else
                    Stop();
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch, rotation);
        }

        public bool GhostCollision(GhostSprite[] ghosts)
        {
            bool collision = false;
            foreach (var ghost in ghosts)
                if (this.Collides(ghost, 10))
                {
                    collision = true;
                    break;
                }
            return collision;
        }


    }
}
