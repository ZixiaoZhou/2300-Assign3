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

namespace final.States
{
    internal class Game2State : State
    {
        private SpriteFont _font;
        private List<Bubble> _bubbles;
        private List<Bomb> _bombs;
        private Texture2D _bubbleTexture;
        private Texture2D _bombTexture;
        private double _remainingTime;
        private Random _random;
        private SoundEffect _bubblePopSound;
        private Song _backgroundMusic;
        private Animation _explosionAnimation;
        private SoundEffect _bombPopSound;
        private Animation _bombExplosionAnimation;

        private int _score;
        private int _clickTimes;
 
        private ButtonState _previousMouseState;

        public Game2State(Game1 game, ContentManager content, int score, int clickTimes)
        : base(game, content)
        {
            _remainingTime = 60; // 1 min game time
            _random = new Random();
            //score part
            _score = score;
            _clickTimes = clickTimes;

        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Font");
            _bubbleTexture = _content.Load<Texture2D>("GameState/bubble");// load bubble texture
            _bombTexture = _content.Load<Texture2D>("Game2State/Bomb");//load bomd texture

            _bubblePopSound = _content.Load<SoundEffect>("GameState/bubblevoice");//load bubble voice
            _bombPopSound = _content.Load<SoundEffect>("Game2State/explosion");//load bomb voice

            _backgroundMusic = _content.Load<Song>("GameState/gamestatebgm");

            //load explosion
            var explosionTexture = _content.Load<Texture2D>("Game2State/bubbleexploani");
            var bombexplosionTexture = _content.Load<Texture2D>("Game2State/bombexploani");

            _explosionAnimation = new Animation(explosionTexture, 10)
            {
                FrameSpeed = 0.1f,
                IsLooping = false
            };
            _bombExplosionAnimation = new Animation(bombexplosionTexture, 6)
            {
                FrameSpeed = 0.1f,
                IsLooping = false
            };

            MediaPlayer.IsRepeating = true; // bgm
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
            //load 5 bomb
            _bombs = new List<Bomb>();
            for (int i = 0; i < 5; i++)
            {
                var position = new Vector2(
                    _random.Next(0, Game1.ScreenWidth - 40),
                    _random.Next(0, Game1.ScreenHeight - 40)
                );
                _bombs.Add(new Bomb(_bombTexture, position, 1f)); //original size
            }

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
                // end game when no time left
                _game.ChangeState(new EndState(_game, _content, _score, _clickTimes));
                return;
            }
            var mouseState = Mouse.GetState();

            if (_previousMouseState == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                _clickTimes++;
                var mousePosition = Mouse.GetState().Position.ToVector2();
                //click bubble
                for (int i = 0; i < _bubbles.Count; i++)
                {
                    if (_bubbles[i].Bounds.Contains(mousePosition))
                    {
                        _bubblePopSound.Play();
                        //_bubbles.RemoveAt(i); //remove clicked bubble
                        var bubbleExplosion = new Animation(_explosionAnimation.Texture, _explosionAnimation.FrameCount)
                        {
                            FrameSpeed = _explosionAnimation.FrameSpeed,
                            IsLooping = false
                        };

                        _bubbles[i].TriggerExplosion(bubbleExplosion); // explosion animation
                        _score += 20;
                        break;
                    }
                }
                // click bomd
                for (int i = 0; i < _bombs.Count; i++)
                {
                    if (_bombs[i].Bounds.Contains(mousePosition))
                    {
                        _bombPopSound.Play();
                        var bombExplosion = new Animation(_bombExplosionAnimation.Texture, _bombExplosionAnimation.FrameCount)
                        {
                            FrameSpeed = _bombExplosionAnimation.FrameSpeed,
                            IsLooping = false
                        };
                        _bombs[i].TriggerExplosion(bombExplosion);
                        _score -= 10;
                        break;
                    }
                }
            }
            _previousMouseState = mouseState.LeftButton;

            foreach (var bubble in _bubbles)
            {
                bubble.Update(gameTime);
            }
            foreach (var bomb in _bombs)
            {
                bomb.Update(gameTime);
            }
            _bubbles.RemoveAll(b => b.IsFinished);//remove explosion animation
            _bombs.RemoveAll(b => b.IsFinished);

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
            //draw bombs
            foreach (var bomb in _bombs)
            {
                bomb.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }


    }
}