using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsEngine.TsEngine
{
    public class Shape2D
    {
        public Vector2 pos;
        public Vector2 scale;
        public string tag;
        public string name;

        public Shape2D(Vector2 pos, Vector2 scale, string name = "empty", string tag = "default") { 
            this.pos = pos;
            this.scale = scale;
            this.tag = tag;
            this.name = name;

            TsEngine.RegisterShape(this);
        }

        public void DestroySelf()
        {
            TsEngine.UnRegisterShape(this);
        }
    }
}
