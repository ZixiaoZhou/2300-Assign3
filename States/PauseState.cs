using final.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.States
{
     public class PauseState : State
    {
        private List<Component> _components;
        private Texture2D _backgroundTexture;
        private State _previousState;//save gamestate state
        public PauseState(Game1 game, ContentManager content, State previousState)
          : base(game, content)
        {
            _previousState = previousState;
        }
        public override void LoadContent()
        {
            //load background of pausestate
            _backgroundTexture = _content.Load<Texture2D>("PauseState/PauseBackground");

            var buttonTextureResume = _content.Load<Texture2D>("PauseState/Resume");
            var buttonTextureReturnMainMenu = _content.Load<Texture2D>("PauseState/ReturnToMain");
            var buttonTextureHelp = _content.Load<Texture2D>("PauseState/AboutDeveloper");

            _components = new List<Component>()
            {
                //add resume button
                new Button(buttonTextureResume)
                {
                  Position = new Vector2((Game1.ScreenWidth ) / 2, Game1.ScreenHeight / 2 - 50),
                    Click = new EventHandler(Button_Resume_Clicked),
                    Layer = 0.1f
                },
                //add return main menu button
                new Button(buttonTextureReturnMainMenu)
                {
                   Position = new Vector2((Game1.ScreenWidth ) / 2, Game1.ScreenHeight / 2 + 50),
                    Click = new EventHandler(Button_ReturnMainMenu_Clicked),
                    Layer = 0.1f
                },
                //add help button
                new Button(buttonTextureHelp)
                {
                   Position = new Vector2((Game1.ScreenWidth ) / 2, Game1.ScreenHeight / 2 + 150),
                    Click = new EventHandler(Button_Help_Clicked),
                    Layer = 0.1f
                },
            };
        }
        private void Button_Resume_Clicked(object sender, EventArgs args)
        {
            // game restart
            _game.ChangeState(_previousState);
        }
        private void Button_ReturnMainMenu_Clicked(object sender, EventArgs args)
        {
            // game menu state
            _game.ChangeState(new MenuState(_game, _content));
        }
        private void Button_Help_Clicked(object sender, EventArgs args)
        {
            // game developer information
            _game.ChangeState(new HelpState(_game, _content, this));
        }
        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            // add button component
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

    }
}
