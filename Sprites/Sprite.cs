using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.Managers;
using final.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace final.Sprites
{
    public class Sprite : Component, ICloneable
    {
        protected Dictionary<string, Animation> _animations;

        protected AnimationManager _animationManager;


        protected Vector2 _position { get; set; }


        protected float _scale { get; set; }

        protected Texture2D _texture;

        public List<Sprite> Children { get; set; }

        public Color Colour { get; set; }

        public bool IsRemoved { get; set; }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;

            Children = new List<Sprite>();

            Colour = Color.White;

        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            Children = new List<Sprite>();

            Colour = Color.White;

            _animations = animations;

            _animationManager = new AnimationManager(_animations.FirstOrDefault().Value);

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(
                        _texture,
                        Position,
                        null,
                        Colour,
                        0f,
                        Vector2.Zero,
                        _scale,
                        SpriteEffects.None,
                        0f
                    );
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
        }

       
   

        public object Clone()
        {
            var clone = this.MemberwiseClone() as Sprite;

            if (_animations != null)
            {
                clone._animations = this._animations.ToDictionary(c => c.Key, v => v.Value.Clone() as Animation);
                clone._animationManager = clone._animationManager.Clone() as AnimationManager;
            }

            return clone;
        }
    }
}
