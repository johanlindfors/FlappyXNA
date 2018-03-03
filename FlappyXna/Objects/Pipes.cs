using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyXna.Objects
{
    class Pipe
    {
        public Rectangle? AABB { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Rectangle DestinationRectangle { get; set; }
    }

    class Pipes
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        Pipe topPipe;
        Pipe bottomPipe;

        public bool HasScored { get; set; }
        public bool IsAlive { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        Vector2 origin = new Vector2(0.5f, 0.5f);

        public Pipes(Game game) 
        {
            topPipe = new Pipe { SourceRectangle = new Rectangle(0, 0, 54, 320) };
            bottomPipe = new Pipe { SourceRectangle = new Rectangle(54, 0, 54, 320) };

            spriteBatch = game.Services.GetService<SpriteBatch>();
            texture = game.Content.Load<Texture2D>("pipes");
        }

        public void Reset(int x, int y)
        {
            X = x;
            Y = y;
            HasScored = false;
            IsAlive = true;
        }


        public void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                var deltaX = Ground.GROUND_SPEED * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
                X -= (int)deltaX;
            }
            if(X < -54)
            {
                IsAlive = false;
            }
        }

        public void Draw(GameTime gameTime)
        {
            var topPipeDestinationRectangle = new Rectangle(X - (int)(origin.X * 54), Y - (int)(origin.Y * 320), 54, 320);
            var bottomPipeDestinationRectangle = new Rectangle(X - (int)(origin.X * 54), Y + 440 - (int)(origin.Y * 320), 54, 320);

            spriteBatch.Draw(texture, topPipeDestinationRectangle, topPipe.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, bottomPipeDestinationRectangle, bottomPipe.SourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
