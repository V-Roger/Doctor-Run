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
        protected Background bg;
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

        public Vector2 Bg_Position
        {
            get
            {
                return this.bg.Bg_back_position;
            }
        }

        public Level(Game game) : base(game)
        {
        }
        
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            checkLvlEnd();
            base.Update(gameTime);
        }

        protected void checkLvlEnd()
        {
            if (this.bg.Bg_back_position.X <= -lvlLength * GraphicsDevice.Viewport.Width)
            {
                this.bg.SpeedIdx = 0;
            }
        }
    }
}
