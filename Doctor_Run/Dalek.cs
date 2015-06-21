using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Dalek : Foe
    {
        private Doctor who;
        private List<LaserBeam> dalekLove;
        Random rnd;

        public List<LaserBeam> DalekLove
        {
            get
            {
                return dalekLove;
            }
        }

        public Dalek(Game game, Vector2 pos, int orientation, Doctor _who) : base(game, pos, orientation)
        {
            who = _who;
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            rnd = new Random();
            dalekLove = new List<LaserBeam>();
            this.position.Y = 980;
            this.velocity = Vector2.Zero;
            this.state = Status.ALIVE;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Game.Content.Load<Texture2D>(@"img\gray_dalek");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            float scale = 1.3f;
            if (state == Status.ALIVE)
            {
                spriteBatch.Draw(this.spriteSheet, this.position, new Rectangle(CurrentFrame.X * frameSize.X, CurrentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            exterminate(gameTime);
            base.Update(gameTime);
        }

        public void exterminate(GameTime gameTime)
        {
            if (rnd.Next(1, 350) == 1)
            {
                fire();
            }
        }

        public void fire()
        {
            dalekLove.Add(new LaserBeam(this.Game, this.position));
        }
    }
}
