using final.Managers;
using final.Models;
using final.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace final.States
{
    internal class GameState:State
    {
        private SpriteFont _font;
        private List<Bubble> _bubbles;
        private Texture2D _bubbleTexture;
        private double _remainingTime;
        private Random _random;
        private SoundEffect _bubblePopSound;
        private Song _backgroundMusic;
        private Animation _explosionAnimation;
        //score part
        private int _score;
        private int _clickTimes;

        private ButtonState _previousMouseState;

        public GameState(Game1 game, ContentManager content)
       : base(game, content)
        {
            _remainingTime = 60; // 1 min game time
            _random = new Random();

        }
        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Font");
            _bubbleTexture = _content.Load<Texture2D>("GameState/bubble");// load bubble texture
            _bubblePopSound = _content.Load<SoundEffect>("GameState/bubblevoice");//load bubble voice
            _backgroundMusic = _content.Load<Song>("GameState/gamestatebgm");

            //load explosion
            var explosionTexture = _content.Load<Texture2D>("Game2State/bubbleexploani");
            _explosionAnimation = new Animation(explosionTexture, 10)
            {
                FrameSpeed = 0.1f, 
                IsLooping = true  
            };



            MediaPlayer.IsRepeating = true; // background
            MediaPlayer.Play(_backgroundMusic);


            // load 10 bubbles
            _bubbles = new List<Bubble>();
            for (int i = 0; i < 10; i++)
            {
                var position = new Vector2(
                    _random.Next(0, Game1.ScreenWidth - 40),
                    _random.Next(0, Game1.ScreenHeight - 40)
                );
                _bubbles.Add(new Bubble(_bubbleTexture, position, 1f)); //original size
            }
            //initial score and click time
            _score = 0;
            _clickTimes = 0;
        }


        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _game.ChangeState(new PauseState(_game, _content, this)); // pause the game
                return;
            }

            // renew remain time
            _remainingTime -= gameTime.ElapsedGameTime.TotalSeconds;
            if (_remainingTime <= 0)
            {
                // continue page
                _game.ChangeState(new ContinueState(_game, _content, _score, _clickTimes));
                return;
            }

            var mouseState = Mouse.GetState();


            if (_previousMouseState == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                _clickTimes++;
                var mousePosition = Mouse.GetState().Position.ToVector2();
                for (int i = 0; i < _bubbles.Count; i++)
                {
                    if (_bubbles[i].Bounds.Contains(mousePosition))
                    {
                        _bubblePopSound.Play();
                        //_bubbles.RemoveAt(i); //remove clicked bubble

                        //unique animation
                        var explosionAnimation = new Animation(_explosionAnimation.Texture, _explosionAnimation.FrameCount)
                        {
                            FrameSpeed = _explosionAnimation.FrameSpeed,
                            IsLooping = false
                        };


                        _bubbles[i].TriggerExplosion(explosionAnimation); // explosion animation
                        _score += 20;//get score
                        break;
                    }
                }
            }
            _previousMouseState = mouseState.LeftButton;
            foreach (var bubble in _bubbles)
            {
                bubble.Update(gameTime);
            }
            _bubbles.RemoveAll(b => b.IsFinished);//remove explosion animation
        }



        public override void PostUpdate(GameTime gameTime)
        {
            // TODO

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {       
            spriteBatch.Begin();
            string rawText = $"Time: {Math.Max(0, (int)_remainingTime)}s";
            string scoreText = $"Score: {_score}";
            string clickText = $"Clicks: {_clickTimes}";
            // todo clean scene

            spriteBatch.DrawString(
                _font,
                rawText,
                new Vector2(10, 10),
                Color.White
            ); 
            spriteBatch.DrawString(_font, scoreText, new Vector2(10, 40), Color.White);
            spriteBatch.DrawString(_font, clickText, new Vector2(10, 70), Color.White);
            //draw bubble
            foreach (var bubble in _bubbles)
            {
                bubble.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
