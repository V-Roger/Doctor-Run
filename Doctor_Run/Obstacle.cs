using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class Obstacle : DrawableGameComponent
    {
        private Vector2 position;
        private Vector2 velocity;
        SpriteBatch spriteBatch;
        Texture2D sprite;
        public BoundingBox bbox;
        Point FrameSize = new Point(100, 64);
        Random rnd;

        public BoundingBox Bbox
        {
            get
            {
                return bbox;
            }
        }

        public Obstacle(Game game)
            : base(game)
        {
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.velocity.X = -1.5f;
            this.position.X = 1850;
            this.position.Y = 1000;
            rnd = new Random();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            string spriteName = "obstacle";
            spriteName += rnd.Next(1, 6);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sprite = Game.Content.Load<Texture2D>(@"img\"+spriteName);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.position += this.velocity;
            this.bbox = new BoundingBox(new Vector3(this.position.X, this.position.Y, 0),
                                        new Vector3(this.position.X + this.FrameSize.X, this.position.Y + this.FrameSize.Y, 0));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.sprite, this.position, Color.AliceBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
