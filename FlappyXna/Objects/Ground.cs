using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyXna.Objects
{
    class Ground : DrawableGameComponent
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;

        public Ground(Game game) : base(game)
        {

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
            spriteBatch.Draw(texture, new Vector2(0, 400), Color.White);
            base.Draw(gameTime);
        } 
    }
}
