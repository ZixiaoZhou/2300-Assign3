using final.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace final.Sprites
{
    public class Explosion : Sprite
    {
        public bool IsFinished => _animationManager != null &&
                               _animationManager.CurrentFrame == _animationManager.CurrentAnimation.FrameCount - 1 &&
                               _animationManager.FrameTimer >= _animationManager.CurrentAnimation.FrameSpeed;

        public Explosion(Animation animation, Vector2 position)
            : base(new Dictionary<string, Animation> { { "Default", animation } })
        {
            Position = position;
            _animationManager.Play(animation);
        }

        public override void Update(GameTime gameTime)
        {
            _animationManager?.Update(gameTime);

            // Remove the explosion if the animation is finished
            if (IsFinished)
                IsRemoved = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationManager?.Draw(spriteBatch);
        }
    }
}
