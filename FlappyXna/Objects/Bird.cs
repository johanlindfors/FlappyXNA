using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyXna.Objects
{
    class Bird : DrawableGameComponent
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private int currentFrame = 0;
        private float currentTimeDelta = 0;
        private const int BIRD_SPRITE_WIDTH = 34;
        private const int BIRD_SPRITE_HEIGHT = 24;

        public Bird(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            spriteBatch = this.Game.Services.GetService<SpriteBatch>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = this.Game.Content.Load<Texture2D>("bird");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            currentTimeDelta += gameTime.ElapsedGameTime.Milliseconds;
            if(currentTimeDelta > 1000/12)
            {
                currentTimeDelta = 0;
                currentFrame++;
                currentFrame %= 3;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var sourceRectangle = new Rectangle(currentFrame * BIRD_SPRITE_WIDTH, 0, BIRD_SPRITE_WIDTH, BIRD_SPRITE_HEIGHT);
            spriteBatch.Draw(texture, new Vector2(100, this.Game.Window.ClientBounds.Height/2), sourceRectangle, Color.White);
            base.Draw(gameTime);
        }
    }
}
