using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class WeepingAngel : Foe
    {
        private Doctor who;

        public WeepingAngel(Game game, Vector2 pos, int orientation, Doctor _who) : base(game, pos, orientation)
        {
            who = _who;
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.position.Y = 980;
            this.velocity = Vector2.Zero;
            this.state = Status.ALIVE;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Game.Content.Load<Texture2D>(@"img\angel_spritesheet");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            float scale = 1.5f;
            if (state == Status.ALIVE)
            {
                spriteBatch.Draw(this.spriteSheet, this.position, new Rectangle(CurrentFrame.X * frameSize.X, CurrentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            stalk(gameTime);
            base.Update(gameTime);
        }

        protected void stalk(GameTime gameTime)
        {
            if (who.DoctorOrientation == this.orientation && who.Position.X > this.position.X)
            {
                CurrentFrame.X = 0;
                attack(gameTime);
            }
            else
            {
                CurrentFrame.X = 1;
                this.velocity.X = -1.5f;
            }
        }

        protected void attack(GameTime gameTime)
        {
            this.velocity.X = 4f;
        }
    }
}
