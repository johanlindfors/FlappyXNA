using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyXna.Objects
{
    class Panorama : DrawableGameComponent
    {
        private Texture2D cloudTexture;
        private Texture2D cityscapeTexture;
        private Texture2D treesTexture;

        private SpriteBatch spriteBatch;

        public Panorama(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            spriteBatch = this.Game.Services.GetService<SpriteBatch>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            cloudTexture = this.Game.Content.Load<Texture2D>("clouds");
            cityscapeTexture = this.Game.Content.Load<Texture2D>("cityscape");
            treesTexture = this.Game.Content.Load<Texture2D>("trees");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(cloudTexture, new Vector2(0, 300), Color.White);
            spriteBatch.Draw(cityscapeTexture, new Vector2(0, 330), Color.White);
            spriteBatch.Draw(treesTexture, new Vector2(0, 360), Color.White);

            base.Draw(gameTime);
        }
    }
}
