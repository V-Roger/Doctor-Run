using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class LaserBeam : DrawableGameComponent
    {
        private Vector2 position;
        private Vector2 velocity;
        private int orientation;
        private int state;
        SpriteBatch spriteBatch;
        Texture2D laser;
        public BoundingBox bbox;
        Point FrameSize = new Point(60, 60);


        public BoundingBox Bbox
        {
            get
            {
                return bbox;
            }
        }

        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        public LaserBeam(Game game, Vector2 _pos)
            : base(game)
        {
            this.position = _pos;
            this.Game.Components.Add(this);
        }

        public override void Initialize()
        {
            this.state = Status.FREE;
            this.velocity.X = 10f;
            this.position.Y += 10f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            laser = Game.Content.Load<Texture2D>(@"img\laser");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.state != Status.DEAD)
            {
                this.position += this.velocity;
                this.bbox = new BoundingBox(new Vector3(this.position.X, this.position.Y + 22, 0),
                                            new Vector3(this.position.X + this.FrameSize.X, this.position.Y + 42, 0));
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (this.state != Status.DEAD)
            {
                spriteBatch.Draw(this.laser, this.position, Color.AliceBlue);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
