using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Tardis : DrawableGameComponent
    {
        protected Texture2D spritesheet;
        protected Vector2 TardisPosition;
        protected float TardisAlpha;
        protected double TardisDematerialisationTime;
        protected bool dematerialisation;
        protected double TardisStartTime;
        protected BoundingBox TardisBbox;

        protected SpriteBatch spriteBatch;

        protected Point frameSize = new Point(50, 90);//this is the size of your frame.  This is an example.
        //It should be the Width, Height of each of your frames.  
        //It's important that each frame is the same size.

        protected Point SheetSize = new Point(3, 1);//this is how many frames of animation
        //you have.  The first number is the number of frames in a row.  The second is the
        //number of rows you have.  E.g, for 8 frames that are in one row, it would be (8,1).

        protected Point CurrentFrame = new Point(0, 0); //You'll use this later to keep track
        //of what frame you're on.

        protected int AnimationDelay = 0;

        public BoundingBox tardisBbox
        {
            get
            {
                return TardisBbox;
            }
        }

        public Vector2 tardisPosition
        {
            get
            {
                return TardisPosition;
            }
            set
            {
                TardisPosition = value;
            }
        }

        public Tardis(Game game)
            : base(game)
        {
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.TardisPosition.X = 2048;
            this.TardisPosition.Y = 970;
            this.TardisAlpha = 1;
            this.TardisDematerialisationTime = 2f;
            this.dematerialisation = false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.spritesheet = Game.Content.Load<Texture2D>(@"img\tardis_spritesheet");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            float scale = 1f;
            spriteBatch.Draw(this.spritesheet, this.TardisPosition, new Rectangle(CurrentFrame.X * frameSize.X, CurrentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White * this.TardisAlpha, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            checkTardis(gameTime);
            this.TardisBbox = new BoundingBox(new Vector3(this.TardisPosition.X, this.TardisPosition.Y, 0),
                                              new Vector3(this.TardisPosition.X + this.frameSize.X, this.TardisPosition.Y + this.frameSize.Y, 0));
            base.Update(gameTime);
        }

        protected void checkTardis(GameTime gameTime)
        {
            if (this.dematerialisation)
            {
                if (this.TardisAlpha > 0)
                {
                    this.TardisAlpha -= 0.05f;
                }
            }
        }
        public void dematerialise(GameTime gameTime)
        {
            if (CurrentFrame.X < SheetSize.X - 1)
            {
                ++CurrentFrame.X;// Move to a new frame
            }
            if (!this.dematerialisation)
            {
                this.TardisStartTime = gameTime.ElapsedGameTime.TotalSeconds;
                this.dematerialisation = true;
            }
        }
    }
}
