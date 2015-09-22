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
    class LevelElement
    {

        public Texture2D texture;
        public Vector2 offset;

        public LevelElement(Texture2D t)
        {
            texture = t;
            offset = new Vector2(0, 0);
        }

    }
}
