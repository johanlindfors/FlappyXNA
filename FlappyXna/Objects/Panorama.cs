using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyXna.Objects
{
    class Panorama : DrawableGameComponent
    {
        private Texture2D cloudTexture;
        private Texture2D cityscapeTexture;
        private Texture2D treesTexture;

        private const float CLOUD_SPEED = 20f;
        private const float CITYSCAPE_SPEED = 30f;
        private const float TREES_SPEED = 60f;

        private SpriteBatch spriteBatch;

        private Rectangle cloudSourceRectangle;
        private Rectangle cityscapeSourceRectangle;
        private Rectangle treesSourceRectangle;

        private float cloudDelta = 0f;
        private float cityscapeDelta = 0f;
        private float treesDelta = 0f;

        public bool IsAlive = false;

        public Panorama(Game game) : base(game)
        {
            IsAlive = true;
        }

        public override void Initialize()
        {
            spriteBatch = this.Game.Services.GetService<SpriteBatch>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            cloudTexture = this.Game.Content.Load<Texture2D>("clouds");
            cloudSourceRectangle = new Rectangle(0, 0, cloudTexture.Width, cloudTexture.Height);

            cityscapeTexture = this.Game.Content.Load<Texture2D>("cityscape");
            cityscapeSourceRectangle = new Rectangle(0, 0, cityscapeTexture.Width, cityscapeTexture.Height);

            treesTexture = this.Game.Content.Load<Texture2D>("trees");
            treesSourceRectangle = new Rectangle(0, 0, treesTexture.Width, treesTexture.Height);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsAlive)
            {
                var deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
                cloudDelta += deltaTime * CLOUD_SPEED % cloudTexture.Width;
                cityscapeDelta += deltaTime * CITYSCAPE_SPEED % cityscapeTexture.Width;
                treesDelta += deltaTime * TREES_SPEED % treesTexture.Width;

                cloudSourceRectangle.X = (int)cloudDelta;
                cityscapeSourceRectangle.X = (int)cityscapeDelta;
                treesSourceRectangle.X = (int)treesDelta;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(cloudTexture, new Vector2(0, 300), cloudSourceRectangle, Color.White);
            spriteBatch.Draw(cityscapeTexture, new Vector2(0, 330), cityscapeSourceRectangle, Color.White);
            spriteBatch.Draw(treesTexture, new Vector2(0, 360), treesSourceRectangle, Color.White);

            base.Draw(gameTime);
        }
    }
}
