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
    class PelletSprite : LevelElement
    {
        public static Vector2 size = new Vector2(10,10);

        public PelletSprite(Texture2D t) : base(t)
        {
            offset = new Vector2(15, 15);
        }

        public static PelletSprite create(Game1 game)
        {
            return new PelletSprite(game.Content.Load<Texture2D>("Sprites/pellet"));
        }

    }
}
