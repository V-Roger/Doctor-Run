using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_Run
{
    class City : Level
    {
        public City(Game game) : base(game) 
        {
            name = "city";
            lvlLength = 0.5f;
            this.bg = new Background(game, name, 2);
            this.Game.Components.Add(this);
        }
    }
}
