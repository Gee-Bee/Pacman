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

        public Direction inputDirection;
        private int defaultVelocity;
        private float rotation;

        public PacmanSprite(Game1 game, Texture2D p0, Texture2D p1, Vector2 s, int screenWidth, int screenHeight, int velocity)
            : base(game, p0, p1, s, screenWidth, screenHeight, velocity, Color.White)
        {
            inputDirection = direction;
            defaultVelocity = velocity;
            rotation = 0;
        }

        public static PacmanSprite create(Game1 game, GraphicsDeviceManager graphics)
        {
            return new PacmanSprite(game, game.Content.Load<Texture2D>("Sprites/pacman0"), game.Content.Load<Texture2D>("Sprites/pacman1"),
                new Vector2(40f, 40f), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 2);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            if (game.gameState != GameState.GameOver)
            {
                if (keyboardState.IsKeyDown(Keys.Left))
                    inputDirection = Direction.Left;
                if (keyboardState.IsKeyDown(Keys.Right))
                    inputDirection = Direction.Right;
                if (keyboardState.IsKeyDown(Keys.Down))
                    inputDirection = Direction.Down;
                if (keyboardState.IsKeyDown(Keys.Up))
                    inputDirection = Direction.Up;

                if (!HitBorder(inputDirection))
                    direction = inputDirection;
                if (!HitBorder(direction))
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

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch, rotation);
        }

        public bool GhostCollision(GhostSprite[] ghosts)
        {
            bool collision = false;
            int offset = 10;
            foreach (var ghost in ghosts)
            {
                if (
                    this.position.X + this.size.X - offset > ghost.position.X &&
                    this.position.X < ghost.position.X + ghost.size.X - offset &&
                    this.position.Y + this.size.Y - offset > ghost.position.Y &&
                    this.position.Y < ghost.position.Y + ghost.size.Y - offset
                )
                {
                    collision = true;
                    break;
                }
            }
            return collision;
        }


    }
}
