using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Cyberman : Foe
    {

        public Cyberman(Game game) : base(game)
        {
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.position.Y = 980;            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Game.Content.Load<Texture2D>(@"img\cyberman_spritesheet");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            float scale = 1.5f;
            spriteBatch.Draw(this.spriteSheet, this.position, new Rectangle(CurrentFrame.X * frameSize.X, CurrentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            animate();
            base.Update(gameTime);
        }

        public override void animate()
        {
            if (AnimationDelay == 4)// delay frame update if it's too fast
            {
                if (CurrentFrame.X < SheetSize.X)
                {
                    ++CurrentFrame.X;// Move to a new frame
                }
                else
                {
                    CurrentFrame.X = 0;//set the X to 1, so we start fresh
                }
                AnimationDelay = 0;//Set this to 0, so we delay it again
            }
            else
            {
                AnimationDelay += 1;// add one, so we can continue when we are ready
            }
        }
    }
}
