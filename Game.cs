using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace SnowFallfna
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager reset;
        SpriteBatch spriteBatch;
        Random rnd = new Random();
        Texture2D backfon, snow;
        private List<SnowCord> snowfall = new List<SnowCord>();
        private KeyboardState start = Keyboard.GetState();
        private KeyboardState stop;
        private static MouseState mouse;
        public Game()
        {
            reset = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Res";
            reset.PreferredBackBufferWidth = 1280;
            reset.PreferredBackBufferHeight = 720;
            reset.IsFullScreen = true;
            IsMouseVisible = true;
            AddSnowFall();
            reset.ApplyChanges();
        }
        public void AddSnowFall()
        {
            for (int i = 0; i < 1000; i++)
            {
                snowfall.Add(new SnowCord
                {
                    X = rnd.Next(0, reset.PreferredBackBufferWidth),
                    Y = -rnd.Next(reset.PreferredBackBufferHeight),
                    Size = rnd.Next(10, 20)
                });
            }
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backfon = Content.Load<Texture2D>("back.jpg");
            snow = Content.Load<Texture2D>("Snow.png");
        }
        protected override void Update(GameTime gameTime)
        {
            stop = start;
            start = Keyboard.GetState();
            mouse = Mouse.GetState();
            foreach (var Snowfall in snowfall)
            {
                Snowfall.Y += Snowfall.Size;
                if (Snowfall.Y > reset.PreferredBackBufferHeight)
                {
                    Snowfall.Y = -50;
                    Snowfall.X = rnd.Next(0, reset.PreferredBackBufferWidth);
                }
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Snowfall.Y += Snowfall.Size / 20 - Snowfall.Size;
                }
                if (stop.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backfon,
                new Rectangle(0, 0,
                reset.PreferredBackBufferWidth,
                reset.PreferredBackBufferHeight),
                Color.White);
            foreach (var Snowfall in snowfall)
            {
                spriteBatch.Draw(snow, new Rectangle(
                    Snowfall.X,
                    Snowfall.Y,
                    Snowfall.Size,
                    Snowfall.Size),
                    Color.White);
            }
            spriteBatch.End();
        }
    }
}
