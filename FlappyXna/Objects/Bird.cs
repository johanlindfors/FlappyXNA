using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FlappyXna.Objects
{
    class Bird : DrawableGameComponent, IPhysicsBody
    {
        private Texture2D texture;
        private SpriteBatch spriteBatch;
        private int currentFrame = 0;
        private float currentTimeDelta = 0;
        private const int BIRD_SPRITE_WIDTH = 34;
        private const int BIRD_SPRITE_HEIGHT = 24;
        private float angle = 0.0f;
        private Vector2 origin = new Vector2(BIRD_SPRITE_WIDTH / 2, BIRD_SPRITE_HEIGHT / 2);

        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 Velocity { get; set; }
        public bool AllowGravity { get; set; }
        public bool IsCollidable { get; set; }
        public Action<IPhysicsBody> OnCollideWith { get; set; }

        public Rectangle? AABB {
            get {
                return new Rectangle( (int)(X - origin.X), (int)(Y -origin.Y), BIRD_SPRITE_WIDTH, BIRD_SPRITE_HEIGHT );
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
            Velocity = new Vector2(0, -400);
        }

        public override void Initialize()
        {
            spriteBatch = Game.Services.GetService<SpriteBatch>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("bird");
            base.LoadContent();
        }

        public void Flap()
        {
            OnGround = false;

            if (IsAlive)
            {
                Velocity = new Vector2(0, -400);
                angle = -60f;
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

            if (this.angle < 90 && IsAlive )
            {
                this.angle += 2.5f;
            }

            base.Update(gameTime);
        }

        private void OnCollide(IPhysicsBody body)
        {
            if(body is Ground)
            {
                this.IsAlive = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var sourceRectangle = new Rectangle(currentFrame * BIRD_SPRITE_WIDTH, 0, BIRD_SPRITE_WIDTH, BIRD_SPRITE_HEIGHT);
            var destinationRectangle = new Rectangle((int)X, (int)Y, BIRD_SPRITE_WIDTH, BIRD_SPRITE_HEIGHT);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, MathHelper.ToRadians(angle), origin,SpriteEffects.None,1);
            base.Draw(gameTime);
        }
    }
}
