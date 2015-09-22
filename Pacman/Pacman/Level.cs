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
using System.IO;


namespace Pacman
{
    class Level
    {
        LevelElement[,] els;
        Game1 game;
        PacmanSprite pacman;
        static int elWidth = 40;
        static int elHeight = 40;

        public Level(Game1 game, int width, int height)
        {
            this.game = game;
            els = new LevelElement[height, width];
            int b0 = els.GetUpperBound(0);
            int b1 = els.GetUpperBound(1);
            ReadLevel("Levels/1.txt");
        }

        private void ReadLevel(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            for (int i = 0; i <= els.GetUpperBound(0); i++)
            {
                char[] line = lines[i].ToCharArray();
                for (int j = 0; j <= els.GetUpperBound(1); j++)
                {
                    switch (line[j])
                    {
                        case 'P':
                            els[i,j] = PelletSprite.create(game);
                            break;
                    }
                }
            }
        }

        public static Level create(Game1 game, GraphicsDeviceManager graphics)
        {   
            return new Level(game, graphics.PreferredBackBufferWidth / elWidth, graphics.PreferredBackBufferHeight / elHeight);
        }

        public void Update()
        {
            for (int i = 0; i < els.GetUpperBound(0); i++)
                for(int j = 0; j < els.GetUpperBound(1); j++)
                {
                    LevelElement el = els[i,j];
                    if (el is PelletSprite && isEaten(new Vector2(j * elWidth, i * elHeight) + el.offset))
                        els[i,j] = null;
                }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i <= els.GetUpperBound(0); i++)
                for (int j = 0; j <= els.GetUpperBound(1); j++ )
                {
                    LevelElement el = els[i, j];
                    if (el != null){
                        Vector2 position = new Vector2(j * elWidth, i * elHeight) + el.offset;
                        spriteBatch.Draw(el.texture, position, Color.White);
                    }
                }
        }

        public bool isEaten(Vector2 pelletPosition)
        {
            PacmanSprite pacman = game.pacmanSprite;
            int offset = 0;
            return (
                pacman.position.X + pacman.size.X - offset > pelletPosition.X &&
                pacman.position.X < pelletPosition.X + PelletSprite.size.X - offset &&
                pacman.position.Y + pacman.size.Y - offset > pelletPosition.Y &&
                pacman.position.Y < pelletPosition.Y + PelletSprite.size.Y - offset
            );
        }

    }
}
