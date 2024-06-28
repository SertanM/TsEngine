using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TsEngine
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2()
        {
            x = Zero().x;
            y = Zero().y;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }

        public static Vector2 One()
        {
            return new Vector2(1, 1);
        }

        public static Vector2 Center(float width, float height)
        {
            return new Vector2((Screen.PrimaryScreen.Bounds.Width - width) / 2, (Screen.PrimaryScreen.Bounds.Height - height) / 2);
        }

        public static Vector2 CenterOfWindow(Vector2 screenSize,Vector2 scale)
        {

            return new Vector2((screenSize.x - scale.x) / 2, (screenSize.y - scale.y) / 2);
        }

        public override string ToString()
        {
            return x + ":" + y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }
    }
}
