using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;

namespace TsEngine.TsEngine
{
    public class Sprite2D
    {
        public Vector2 pos;
        public Vector2 scale;
        public string directory;
        public string tag;
        public string name;
        public Bitmap Sprite;

        public Vector2 velocity = Vector2.Zero();
        public Sprite2D(Vector2 pos, Vector2 scale, string directory = "T", string name = "empty", string tag = "default")
        {
            this.pos = pos;
            this.scale = scale;
            this.directory = directory;
            this.tag = tag;
            this.name = name;
            string a = Assembly.GetEntryAssembly().Location;
            a = a.Substring(0, a.LastIndexOf("T"));
            
            string path = a + $"Assets\\Sprites\\{directory}.png";
            Image tmp = Image.FromFile(path);
            Sprite = new Bitmap(tmp, (int)this.scale.x, (int)this.scale.y);

            TsEngine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
            TsEngine.UnRegisterSprite(this);
        }

        public void restartImage()
        {
            TsEngine.UnRegisterSprite(this);
            Image tmp = Image.FromFile($"Assets/Sprites/{directory}.png");
            Sprite = new Bitmap(tmp, (int)this.scale.x, (int)this.scale.y);

            TsEngine.RegisterSprite(this);
        }

        public bool IsColliding(Sprite2D a, Sprite2D b)
        {
            return (a.pos.y + a.scale.y > b.pos.y && b.pos.y + b.scale.y > a.pos.y
                    && a.pos.x + a.scale.x > b.pos.x && b.pos.x + b.scale.x > a.pos.x);
        }

        public Sprite2D IsCollidingWithTag(string tag)
        {
            foreach(Sprite2D s in TsEngine.GetSpritesWithTag(tag))
            {
                if(pos.y + scale.y > s.pos.y && s.pos.y + s.scale.y > pos.y
                    && pos.x + scale.x > s.pos.x && s.pos.x + s.scale.x > pos.x) return s;
            }
            return null;
        }

        public bool IsCursorCollidingWith()
        {
            if (pos.y + scale.y > TsEngine.mousePos.y && TsEngine.mousePos.y > pos.y
                && pos.x + scale.x > TsEngine.mousePos.x && TsEngine.mousePos.x > pos.x) return true;
            return false;
        }

        public static Sprite2D IsCursorCollidingWithTag(string tag)
        {
            foreach (Sprite2D s in TsEngine.GetSpritesWithTag(tag))
            {
                if (s.pos.y + s.scale.y > TsEngine.mousePos.y && TsEngine.mousePos.y > s.pos.y
                && s.pos.x + s.scale.x > TsEngine.mousePos.x && TsEngine.mousePos.x > s.pos.x) return s;
            }
            return null;
        }

        public void FlipImageY()
        {
            Sprite.RotateFlip(RotateFlipType.Rotate180FlipY);
        }

        public void FlipImageX()
        {
            Sprite.RotateFlip(RotateFlipType.Rotate180FlipX);
        }

        public void ApplyVelocity()
        {
            pos += velocity;
        }
    }
}
