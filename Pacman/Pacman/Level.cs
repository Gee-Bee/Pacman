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
        static int elWidth = 40;
        static int elHeight = 40;

        public int pelletMax;
        public int pelletEaten;

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
                            pelletMax += 1;
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
            int pacmanI = (int)(game.pacmanSprite.position / PacmanSprite.size).Y;
            int pacmanJ = (int)(game.pacmanSprite.position / PacmanSprite.size).X;
            LevelElement el = els[pacmanI, pacmanJ];
            if (el is PelletSprite)
                EatPellet((PelletSprite)el, pacmanI, pacmanJ);
        }

        private void EatPellet(PelletSprite pellet, int i, int j)
        {
            Vector2 pelletPosition = new Vector2(j * elWidth, i * elHeight) + pellet.offset;
            int offset = 10;
            if
            (
                game.pacmanSprite.position.X + offset > pelletPosition.X &&
                game.pacmanSprite.position.X - offset < pelletPosition.X + PelletSprite.size.X &&
                game.pacmanSprite.position.Y + offset > pelletPosition.Y &&
                game.pacmanSprite.position.Y - offset < pelletPosition.Y + PelletSprite.size.Y
            )
            {
                pelletEaten += 1;
                game.pacmanSprite.eating = true;
                els[i, j] = null;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i <= els.GetUpperBound(0); i++)
                for (int j = 0; j <= els.GetUpperBound(1); j++)
                {
                    LevelElement el = els[i, j];
                    if (el != null)
                    {
                        Vector2 position = new Vector2(j * elWidth, i * elHeight) + el.offset;
                        spriteBatch.Draw(el.texture, position, Color.White);
                    }
                }

        }

    }
}
