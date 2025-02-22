using final.Controls;
using final.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.States
{
        public class MenuState : State
        {
            private List<Component> _components;
            private Texture2D _backgroundTexture;
            private Song _backgroundMusic;

        public MenuState(Game1 game, ContentManager content)
              : base(game, content)
            {
            }

        public override void LoadContent()
        {
            //load background of mainmenu
            _backgroundTexture = _content.Load<Texture2D>("MainMenu/start");

            var buttonTextureStart = _content.Load<Texture2D>("MainMenu/StartGame");
            var buttonTextureScore = _content.Load<Texture2D>("MainMenu/HighScore");

            _backgroundMusic = _content.Load<Song>("MainMenu/menustatebgm");

            MediaPlayer.IsRepeating = true; // background
            MediaPlayer.Play(_backgroundMusic);

            _components = new List<Component>()
            {
                //add start game button
                new Button(buttonTextureStart)
                {
                  Position = new Vector2((Game1.ScreenWidth ) / 2, Game1.ScreenHeight / 2 - 50),
                    Click = new EventHandler(Button_Start_Clicked),
                    Layer = 0.1f
                },
                //add score button
                new Button(buttonTextureScore)
                {
                   Position = new Vector2((Game1.ScreenWidth ) / 2, Game1.ScreenHeight / 2 + 100),
                    Click = new EventHandler(Button_Highscores_Clicked),
                    Layer = 0.1f
                },


            };
        }

            private void Button_Start_Clicked(object sender, EventArgs args)
            {
                // game start
                _game.ChangeState(new GameState(_game, _content));
            }


            private void Button_Highscores_Clicked(object sender, EventArgs args)
            {
                _game.ChangeState(new HighscoresState(_game, _content));
            } 

            //private void Button_Quit_Clicked(object sender, EventArgs args)
            //{
            //    _game.Exit();
            //}

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
