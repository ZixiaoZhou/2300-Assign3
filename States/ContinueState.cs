using final.Controls;
using final.Managers;
using final.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.States
{
   public class ContinueState : State
    {
        private List<Component> _components;
        private Texture2D _backgroundTexture;
        private string _playerName;
        private int _score;
        private int _clickTimes;
        private Texture2D _inputBoxTexture;

        private double _inputCooldown;


        public ContinueState(Game1 game, ContentManager content, int score, int clickTimes)
            : base(game, content)
        {
            _score = score;
            _clickTimes = clickTimes;
            _playerName = "";

        }
        public override void LoadContent()
        {
            //load background of pausestate
            _backgroundTexture = _content.Load<Texture2D>("ContinueState/continuebackground");
            _inputBoxTexture = _content.Load<Texture2D>("ContinueState/PlayerName");

            var continueButtonTexture = _content.Load<Texture2D>("ContinueState/continue");
            var quitButtonTexture = _content.Load<Texture2D>("ContinueState/quit");

            _components = new List<Component>()
            {
                 new Button(continueButtonTexture)
                {
                    Position = new Vector2(Game1.ScreenWidth / 2 - 150, Game1.ScreenHeight / 2+150),
                    Click = ContinueGame
                },
                new Button(quitButtonTexture)
                {
                    Position = new Vector2(Game1.ScreenWidth / 2 + 150, Game1.ScreenHeight / 2),
                    Click = EndGame
                }
            };
        }
        private void ContinueGame(object sender, EventArgs e)
        {
            // go to game2state
            _game.ChangeState(new Game2State(_game, _content, _score, _clickTimes));
        }
        private void EndGame(object sender, EventArgs e)
        {
            // entername
            var scoreManager = ScoreManager.Load();
            scoreManager.Add(new Score
            {
                PlayerName = _playerName,
                Value = _score,
                ClickTimes = _clickTimes
            });
            ScoreManager.Save(scoreManager);
            _game.ChangeState(new HighscoresState(_game, _content));
        }
        public override void Update(GameTime gameTime)
        {
            _inputCooldown -= gameTime.ElapsedGameTime.TotalSeconds;

            // Get current keyboard state
            var keyboardState = Keyboard.GetState();
            if (_inputCooldown > 0)
                return;


            foreach (var key in keyboardState.GetPressedKeys())
            {
                if (key == Keys.Back && _playerName.Length > 0)
                    _playerName = _playerName.Substring(0, _playerName.Length - 1);
                else if (_playerName.Length < 10 && key >= Keys.A && key <= Keys.Z)
                    _playerName += key.ToString();
                _inputCooldown = 0.2;
                break;
            }
            foreach (var component in _components)
                component.Update(gameTime);
        }
        public override void PostUpdate(GameTime gameTime)
        {
            // TODO

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // background
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight), Color.White);

            // input name textbox
            spriteBatch.Draw(
                _inputBoxTexture,
                new Rectangle(Game1.ScreenWidth / 2 - 150, Game1.ScreenHeight / 2, 300, 40),
                Color.White
            );

            // text inputed
            spriteBatch.DrawString(
                _content.Load<SpriteFont>("Font"),
                _playerName,
                new Vector2(Game1.ScreenWidth / 2 - 140, Game1.ScreenHeight / 2 + 5),
                Color.Black
            );
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

    }
}
