using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyXna
{
    interface IPhysicsBody
    {
        float X { get; set; }
        float Y { get; set; }
        Vector2 Velocity { get; set; }
        //Vector2 Acceleration { get; set; }
        bool AllowGravity { get; set; }
        bool IsCollidable { get; set; }
        Action<IPhysicsBody> OnCollideWith { get; set; }
        Rectangle? AABB { get; }
        bool IsAlive { get; }
    }

    class PhysicsEngine : GameComponent
    {
        private const float GRAVITY = 9.1f;
        private IList<IPhysicsBody> bodies;

        public PhysicsEngine(Game game) : base(game)
        {
            bodies = new List<IPhysicsBody>();
        }

        public void AddBody(IPhysicsBody body)
        {
            bodies.Add(body);
        }

        public bool CheckCollision(IPhysicsBody a, IPhysicsBody b, Action<IPhysicsBody, IPhysicsBody> collisionCallback)
        {
            bool intersects = false;
            if (a.AABB.HasValue)
            {
                if (b.AABB.HasValue)
                {
                    intersects = a.AABB.Value.Intersects(b.AABB.Value);
                }
            }

            if (intersects && collisionCallback != null)
            {
                collisionCallback(a, b);
            }
            return intersects;
        }

        public override void Update(GameTime gameTime)
        {
            // apply gravity
            for (int i = 0; i < bodies.Count; i++)
            {
                // apply gravity to velocity vector
                var body = bodies[i];
                if (!body.IsAlive) continue;
                var velocity = body.Velocity;
                if (body.AllowGravity)
                {
                    velocity.Y = velocity.Y + (GRAVITY * gameTime.ElapsedGameTime.Milliseconds / 10);
                }

                body.Y = body.Y + (velocity.Y * gameTime.ElapsedGameTime.Milliseconds / 1000);
                body.X = body.X + (velocity.X * gameTime.ElapsedGameTime.Milliseconds / 1000);
                body.Velocity = velocity;
                bodies[i] = body;
            }

            base.Update(gameTime);
        }
    }
}
