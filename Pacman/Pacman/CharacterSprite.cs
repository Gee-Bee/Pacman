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
    abstract class CharacterSprite
    {
        public Texture2D texture;
        public Texture2D texture0;
        public Texture2D texture1;
        public Vector2 position;
        public Vector2 size;
        public int velocity;

        public Vector2 screenSize;

        public Direction direction;

        public CharacterSprite(Texture2D p0, Texture2D p1, Vector2 p, Vector2 s, int screenWidth, int screenHeight, int velocity)
        {
            texture = texture0 = p0;
            texture1 = p1;
            size = s;
            screenSize = new Vector2(screenWidth, screenHeight);
            direction = Direction.Right;
            position = screenSize / 2;
            this.velocity = velocity;
        }

        public void Dispose()
        {
            texture0.Dispose();
            texture1.Dispose();
        }

        protected Vector2 Movement(Direction direction)
        {
            Vector2 movement = new Vector2(0, 0);
            switch (direction)
            {
                case Direction.Right:
                    movement = new Vector2(velocity, 0);
                    break;
                case Direction.Left:
                    movement = new Vector2(-velocity, 0);
                    break;
                case Direction.Down:
                    movement = new Vector2(0, velocity);
                    break;
                case Direction.Up:
                    movement = new Vector2(0, -velocity);
                    break;
            }
            return movement;
        }

        public void Move()
        {
            position += Movement(direction);
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            Move();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float rotation)
        {
            if (gameTime.TotalGameTime.Milliseconds % 250 == 0)
                if (texture == texture1)
                    texture = texture0;
                else texture = texture1;

            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1, SpriteEffects.None, 1);
        }

        protected bool CanMove(Direction direction)
        {
            return !(
                position.X - size.X/2 <= 0 && direction == Direction.Left || position.X + size.X/2 >= screenSize.X && direction == Direction.Right || 
                position.Y - size.Y/2 <= 0 && direction == Direction.Up   || position.Y + size.Y/2 >= screenSize.Y && direction == Direction.Down
            );
        }


    }
}
