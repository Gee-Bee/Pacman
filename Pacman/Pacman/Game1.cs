using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pacman
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }

    public enum GameState
    {
        Play,
        GameOver
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public PacmanSprite pacmanSprite;
        public GhostSprite[] ghostSprites;
        public GameState gameState;
        Level level;

        SpriteFont scoreFont;
        SpriteFont gameOverFont;
        public SoundEffect am;
        public SoundEffect bum;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();  
            gameState = GameState.Play;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pacmanSprite = PacmanSprite.create(this, graphics);
            ghostSprites = new GhostSprite[] {                
                GhostSprite.create(this, graphics, Color.Thistle),
                GhostSprite.create(this, graphics, Color.SteelBlue),
                GhostSprite.create(this, graphics, Color.Blue),
                GhostSprite.create(this, graphics, Color.Violet),
                GhostSprite.create(this, graphics, Color.Turquoise)
            };
            level = Level.create(this, graphics);

            scoreFont = Content.Load<SpriteFont>("Fonts/Score");
            gameOverFont = Content.Load<SpriteFont>("Fonts/GameOver");
            am = Content.Load<SoundEffect>("Audio/am");
            bum = Content.Load<SoundEffect>("Audio/bum");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            pacmanSprite.Dispose();
            foreach (var ghostSprite in ghostSprites)
                ghostSprite.Dispose();
            spriteBatch.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState keyboardState = Keyboard.GetState();
            pacmanSprite.Update(gameTime, keyboardState);
            foreach (var ghostSprite in ghostSprites)
                ghostSprite.Update(gameTime);
            level.Update();

            if (pacmanSprite.GhostCollision(ghostSprites))
            {
                gameState = GameState.GameOver;
                bum.Play();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            level.Draw(gameTime, spriteBatch);
            pacmanSprite.Draw(gameTime, spriteBatch);
            foreach (var ghostSprite in ghostSprites)
                ghostSprite.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(scoreFont, "Wynik: " + level.pelletEaten + " / " + level.pelletMax, new Vector2(10, 10), Color.White);
            if (gameState == GameState.GameOver)
                spriteBatch.DrawString(gameOverFont, "GAME OVER", new Vector2(40, 60), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
