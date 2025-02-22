using final.Managers;
using final.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Sprites
{
    internal class Bomb
    {
        private Texture2D _texture;
        private Vector2 _position;    // bomb position
        private float _scale;         // bomb size

        private AnimationManager _explosionAnimationManager;
        private bool _isExploding;
        public Rectangle Bounds => new Rectangle(
            (int)(_position.X - _texture.Width * _scale / 2),
            (int)(_position.Y - _texture.Height * _scale / 2),
            (int)(_texture.Width * _scale),
            (int)(_texture.Height * _scale)
        );
        public bool IsFinished => _isExploding && _explosionAnimationManager.CurrentAnimation.CurrentFrame >= _explosionAnimationManager.CurrentAnimation.FrameCount - 1;

        public Bomb(Texture2D texture, Vector2 position, float scale = 1f)
        {
            _texture = texture;
            _position = position;
            _scale = scale;
            _isExploding = false;

        }
        public void TriggerExplosion(Animation explosionAnimation)
        {
            _isExploding = true;
            _explosionAnimationManager = new AnimationManager(explosionAnimation)
            {
                Position = _position,
                Scale = _scale
            };
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_isExploding)
            {
                _explosionAnimationManager.Draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(
                        _texture,
                        _position,
                        null,
                        Color.White,
                        0f,
                        new Vector2(_texture.Width / 2, _texture.Height / 2),
                        _scale,
                        SpriteEffects.None,
                        0f
                    );
            }
            //}
            //else if (Explosion != null)
            //{
            //    Explosion.Draw(gameTime, spriteBatch);
            //}
        }

        public void Update(GameTime gameTime)
        {
            if (_isExploding)
            {
                _explosionAnimationManager.Update(gameTime);
            }
        }
    }
}
