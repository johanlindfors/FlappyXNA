using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyXna.Objects
{
    class Ground : DrawableGameComponent, ICollidable
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        public const float GROUND_SPEED = 200f;

        public float X { get; set; }
        public float Y { get; set; }
        public bool IsCollidable { get; set; }
        public Action<ICollidable> OnCollideWith { get; set; }
        public bool IsAlive { get; set; }

        private Rectangle sourceRectangle;

        private Rectangle? aabb;
        public Rectangle? AABB
        {
            get { return aabb; }
            private set { aabb = value; }
        }

        public Ground(Game game) : base(game)
        {
            X = 0;
            Y = 400;

            IsAlive = true;
        }

        public override void Initialize()
        {
            spriteBatch = this.Game.Services.GetService<SpriteBatch>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = this.Game.Content.Load<Texture2D>("ground");
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            aabb = new Rectangle((int)X, (int)Y, texture.Width, texture.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                var deltaX = GROUND_SPEED * (gameTime.ElapsedGameTime.Milliseconds / 1000f);
                sourceRectangle.X = (sourceRectangle.X + (int)deltaX) % texture.Width;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, new Vector2(X, Y), sourceRectangle, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            base.Draw(gameTime);
        }
    }
}
