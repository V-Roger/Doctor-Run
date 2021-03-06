﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    public class Background : DrawableGameComponent
    {
        private string textureName;
        private float lvlLength;
        private Texture2D bg_back;
        private Texture2D bg_mid;
        private Texture2D bg_front;
        private Vector2 bg_back_position;
        private Vector2 bg_mid_position;
        private Vector2 bg_front_position;
        SpriteBatch spriteBatch;
        private float speedIdx;

        public Vector2 Bg_back_position
        {
            get
            {
                return bg_back_position;
            }
        }


        public float SpeedIdx 
        {
            get
            {
                return speedIdx;
            }
            set
            {
                speedIdx = value;
            }
        }

        public Background(Game game, string name, float len)
            : base(game)
        {
            textureName = name;
            lvlLength = len;
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            speedIdx = 0.5f;
            bg_back_position.X = 0;
            bg_back_position.Y = 0;
            bg_mid_position.X = 0;
            bg_mid_position.Y = 0;
            bg_front_position.X = 0;
            bg_front_position.Y = 56;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bg_back = Game.Content.Load<Texture2D>(@"img\"+textureName+"_far");
            bg_mid = Game.Content.Load<Texture2D>(@"img\"+textureName+"_mid");
            bg_front = Game.Content.Load<Texture2D>(@"img\"+textureName+"_front");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(bg_back, bg_back_position, new Rectangle(0, 0, (int)lvlLength*GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(bg_mid, bg_mid_position, new Rectangle(0, 0, (int)lvlLength*2*GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(bg_front, bg_front_position, new Rectangle(0, 0, (int)lvlLength*3*GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            moveBackgrounds();
            base.Update(gameTime);
        }

        private void moveBackgrounds()
        {
            bg_back_position.X -= speedIdx;
            bg_mid_position.X -= 2 * speedIdx;
            bg_front_position.X -= 3 * speedIdx;
        }


    }
}
