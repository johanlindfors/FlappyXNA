using FlappyXna.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace FlappyXna
{
    public class FlappyGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Panorama panorama;
        Bird bird;
        Ground ground;
        List<Pipes> pipes;

        PhysicsEngine physics;

        KeyboardState lastKeyboardState;
        bool gameOver;
        Random rnd;
        int width = 0;

        Timer pipeGenerator;

        public FlappyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 505;
            graphics.PreferredBackBufferWidth = 288;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            gameOver = true;
            rnd = new Random(1223);

            pipeGenerator = new Timer(1250);
            pipeGenerator.Elapsed += (_,__) => GeneratePipes();
        }

        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService<SpriteBatch>(spriteBatch);

            physics = new PhysicsEngine(this);
            this.Components.Add(physics);

            panorama = new Panorama(this);
            this.Components.Add(panorama);

            bird = new Bird(this);
            this.Components.Add(bird);
            physics.AddBody(bird);

            ground = new Ground(this);
            this.Components.Add(ground);
            physics.AddBody(ground);

            pipes = new List<Pipes>();

            width = Window.ClientBounds.Width;

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void StartGame()
        {
            bird.Reset();
            pipeGenerator.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            physics.CheckCollision(bird, ground, OnBirdCollided);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space))
            {
                if (gameOver)
                {
                    StartGame();
                    gameOver = false;
                    ground.IsAlive = true;
                    panorama.IsAlive = true;
                    pipes.Clear();
                    pipeGenerator.Start();
                }
                else
                {
                    bird.Flap();
                }
            }

            pipes.ForEach(p => p.Update(gameTime));

            base.Update(gameTime);

            lastKeyboardState = currentKeyboardState;
        }

        private void GeneratePipes()
        {
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    pipeGenerator.Stop();
            //}
            var newPipes = pipes.Where(p => !p.IsAlive).FirstOrDefault();
            if (newPipes == null)
            {
                newPipes = new Pipes(this);
                pipes.Add(newPipes);
            } else
            {

            }
            var pipesY = (int)(rnd.NextDouble() * 200 - 100);
           newPipes.Reset(width + 20, pipesY);
        }

        private void OnBirdCollided(IPhysicsBody bird, IPhysicsBody enemy)
        {
            var actualBird = bird as Bird;
            if (!actualBird.OnGround)
            {
                actualBird.OnGround = true;
            }

            if (!gameOver)
            {
                gameOver = true;
                actualBird.IsAlive = false;
                ground.IsAlive = false;
                panorama.IsAlive = false;
                pipes.ForEach(p => p.IsAlive = false);
                pipeGenerator.Stop();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(7, 140, 254, 255));

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.LinearWrap, null, null);
            base.Draw(gameTime);
            pipes.ForEach(p => p.Draw(gameTime));
            spriteBatch.End();
        }
    }
}
