using final.Controls;
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
    public class HelpState : State
    {
        private List<Component> _components;
        private Texture2D _backgroundTexture;
        private PauseState _pauseState; // save  previous PauseState
        public HelpState(Game1 game, ContentManager content, PauseState pauseState)
          : base(game, content)
        {
            _pauseState = pauseState;
        }
        public override void LoadContent()
        {
            //load background of helpstate
            _backgroundTexture = _content.Load<Texture2D>("HelpState/DeveloperInformation");

            var buttonTextureBack = _content.Load<Texture2D>("Back");

            _components = new List<Component>()
            {
                //add back button
                new Button(buttonTextureBack)
                {
                    Position = new Vector2(Game1.ScreenWidth / 2, 560),
                    Click = new EventHandler(Button_Back_Clicked),
                    Layer = 0.1f
                },
            };
        }
        private void Button_Back_Clicked(object sender, EventArgs args)
        {
            // game start
            _game.ChangeState(_pauseState);
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