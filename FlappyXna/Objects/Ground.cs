using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyXna.Objects
{
    class Ground : DrawableGameComponent, IPhysicsBody
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private int GROUND_WIDTH = 335;
        private int GROUND_HEIGHT = 112;

        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 Velocity { get; set; }
        public bool AllowGravity { get; set; }
        public bool IsCollidable { get; set; }
        public Action<IPhysicsBody> OnCollideWith { get; set; }
        public bool IsAlive { get; }

        public Rectangle? AABB
        {
            get
            {
                return new Rectangle((int)X, (int)Y, GROUND_WIDTH, GROUND_HEIGHT);
            }
        }
        public Ground(Game game) : base(game)
        {
            AllowGravity = false;
            X = 0;
            Y = 400;
        }

        public override void Initialize()
        {
            spriteBatch = this.Game.Services.GetService<SpriteBatch>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = this.Game.Content.Load<Texture2D>("ground");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, new Vector2(X, Y), Color.White);
            base.Draw(gameTime);
        }
    }
}
