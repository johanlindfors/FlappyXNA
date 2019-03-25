using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FlappyXna
{
    interface ITween
    {
        bool IsComplete { get; }
        void Update(GameTime gameTime);
        ITween To(Func<float> getter, Action<float> setter, float targetValue, float duration);
    }

    class Tween : ITween
    {
        float duration;
        float elapsed;
        float targetValue;
        float originalValue;
        Action<float> tweenFunc;
        public bool IsComplete { get; private set; }

        public Tween()
        {

        }

        public ITween To(Func<float> getter, Action<float> setter, float targetValue, float duration)
        {
            this.originalValue = getter();
            this.targetValue = targetValue;
            this.tweenFunc = setter;
            this.duration = duration;
            this.elapsed = 0;
            IsComplete = false;
            return this;
        }

        public void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            float currentValue = targetValue;
            if(elapsed <= duration)
            {
                currentValue = MathHelper.Lerp(originalValue, targetValue, elapsed / duration);
            } else
            {
                IsComplete = true;
            }
            tweenFunc(currentValue);
        }
    }

    class TweenEngine
    {
        private List<ITween> objects;

        public TweenEngine()
        {
            objects = new List<ITween>();
        }

        public ITween Add()
        {
            objects.Add(new Tween());
            return objects[objects.Count-1];
        }

        public void Update(GameTime gameTime)
        {
            // apply gravity
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Update(gameTime);
            }

            objects.RemoveAll(tween => tween.IsComplete);
        }
    }
}