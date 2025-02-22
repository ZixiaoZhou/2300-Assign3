using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.Controls;
using final.Managers;
using final.States;
using final;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace final.States
{
    public class HighscoresState : State
    {
        private List<Component> _components;

        private ScoreManager _scoreManager;

        public HighscoresState(Game1 game, ContentManager content)
          : base(game, content)
        {
        }

        public override void LoadContent()
        {

            _scoreManager = ScoreManager.Load();

            var buttonTexture = _content.Load<Texture2D>("Back");


            _components = new List<Component>()
            {
                new Button(buttonTexture)
                {
          
                    Position = new Vector2(Game1.ScreenWidth / 2, 560),
                    Click = new EventHandler(Button_MainMenu_Clicked),
                    Layer = 0.1f
                },
            };
        }

        private void Button_MainMenu_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Clicked(this, new EventArgs());

            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            // high score show
            int yOffset = 100; //position
            foreach (var score in _scoreManager.HighScores)
            {
                var position = new Vector2(400, yOffset);
                spriteBatch.DrawString(
                    _content.Load<SpriteFont>("Font"),
                    $"{score.PlayerName}:Score:  {score.Value}, ClickTimes: {score.ClickTimes}",
                    position,
                    Color.Red
                );
                yOffset += 30; // space
            }

   
            spriteBatch.End();
        }
    }
}
