using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Level : DrawableGameComponent
    {
        protected Texture2D Tardis;
        protected Vector2 TardisPosition;
        protected Background bg;
        protected BoundingBox TardisBbox;
        protected SpriteBatch spriteBatch;
        protected float lvlLength;
        public string name;

        public float LvlLength
        {
            get
            {
                return lvlLength;
            }
            set
            {
                lvlLength = value;
            }
        }

        public BoundingBox tardisBbox
        {
            get
            {
                return TardisBbox;
            }
        }

        public Level(Game game) : base(game)
        {
        }
        
        public override void Initialize()
        {
            this.TardisPosition.X = lvlLength * 1920 - 250;
            this.TardisPosition.Y = 970;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tardis = Game.Content.Load<Texture2D>(@"img\tardis_spritesheet");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.Tardis, this.TardisPosition, Color.AliceBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            checkTardis();
            this.TardisBbox = new BoundingBox(new Vector3(this.TardisPosition.X, this.TardisPosition.Y, 0),
                                        new Vector3(this.TardisPosition.X + this.Tardis.Width, this.TardisPosition.Y + this.Tardis.Height, 0));
            base.Update(gameTime);
        }

        protected void checkTardis()
        {
            if (this.bg.Bg_back_position.X <= -lvlLength * GraphicsDevice.Viewport.Width)
            {
                this.bg.SpeedIdx = 0;
            }
        }
    }
}
