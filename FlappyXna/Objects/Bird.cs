using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace FlappyXna.Objects
{
    class Bird : DrawableGameComponent, IPhysicsBody, ICollidable
    {
        private Texture2D texture;
        private Texture2D debugTexture;
        private SpriteBatch spriteBatch;
        private int currentFrame = 0;
        private float currentTimeDelta = 0;
        private const int BIRD_SPRITE_WIDTH = 34;
        private const int BIRD_SPRITE_HEIGHT = 24;
        private float angle = 0.0f;
        private Vector2 origin = new Vector2(0.5f, 0.5f);

        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 Velocity { get; set; }
        public bool AllowGravity { get; set; }
        public bool IsCollidable { get; set; }
        public Action<ICollidable> OnCollideWith { get; set; }
        
        public Rectangle? AABB {
            get {
                return new Rectangle( (int)(X - origin.X * BIRD_SPRITE_WIDTH), (int)(Y - origin.Y * BIRD_SPRITE_HEIGHT), BIRD_SPRITE_WIDTH, BIRD_SPRITE_HEIGHT );
            }
        }

        public bool OnGround { get; set; }

        public bool IsAlive { get; set; }

        public Bird(Game game) : base(game)
        {
            Reset();
            IsAlive = false;
            AllowGravity = false;
        }

        public void Reset()
        {
            X = 100;
            Y = this.Game.Window.ClientBounds.Height / 2;

            angle = 0;
            currentTimeDelta = 0;
            IsAlive = true;
            AllowGravity = true;
            OnCollideWith = OnCollide;
            OnGround = false;
            Velocity = new Vector2(0, 0);
        }

        public override void Initialize()
        {
            spriteBatch = Game.Services.GetService<SpriteBatch>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("bird");

            debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            Color[] pixels = { Color.Green };
            debugTexture.SetData(pixels);
            base.LoadContent();
        }

        public void Flap()
        {
            OnGround = false;

            if (IsAlive)
            {
                Velocity = new Vector2(0, -400);
                this.angle = -60f;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (OnGround) return;

            currentTimeDelta += gameTime.ElapsedGameTime.Milliseconds;
            if (currentTimeDelta > 1000 / 12)
            {
                currentTimeDelta = 0;
                currentFrame++;
                currentFrame %= 3;
            }

            if (this.angle < 90 && IsAlive)
            {
                this.angle += 2.5f;
            }

            base.Update(gameTime);
        }

        private void OnCollide(ICollidable body)
        {
            if (this.Velocity.Y < 0)
            {
                Velocity = new Vector2(0, 0);
            }

            if (body is Ground && !this.OnGround)
            {
                this.OnGround = true;
                this.IsAlive = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var sourceRectangle = new Rectangle(currentFrame * BIRD_SPRITE_WIDTH, 0, BIRD_SPRITE_WIDTH, BIRD_SPRITE_HEIGHT);
            var destinationRectangle = new Rectangle((int)(X), (int)(Y), BIRD_SPRITE_WIDTH, BIRD_SPRITE_HEIGHT);     
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, MathHelper.ToRadians(angle), new Vector2(origin.X * BIRD_SPRITE_WIDTH, origin.Y * BIRD_SPRITE_HEIGHT), SpriteEffects.None,1);

            //if (AABB.HasValue)
            //{
            //    spriteBatch.Draw(debugTexture, AABB.Value, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1);
            //}
            base.Draw(gameTime);
        }
    }
}
