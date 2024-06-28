using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Diagnostics;

namespace TsEngine.TsEngine
{

    class Canvas : Form 
    { 
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }


    public abstract class TsEngine
    {
        private Vector2 ScreenSize = new Vector2(512f, 512f);
        private string Title = "FirstApp";
        private Canvas Window = null;
        private Thread GameLoopThread = null;


        private static List<Shape2D> AllShapes = new List<Shape2D>();
        private static List<Sprite2D> AllSprites = new List<Sprite2D>();


        public Color bgColor = Color.White;
        public Vector2 camPos = Vector2.Zero();
        public float camAngle = 0f;
        public Vector2 camZoom = Vector2.One();

        public static Vector2 mousePos = Vector2.Zero();
        public bool isMouseClicked = false;

        public Vector2 screenPos = Vector2.Zero();

        public TsEngine(Vector2 ScreenSize, Vector2 firstPos, Color bgColor, string Title = "FirstApp") {
            Debug.Info("Game is starting...");
            this.ScreenSize = ScreenSize;
            this.Title = Title;
            this.bgColor = bgColor;

            


            Window = new Canvas();
            Window.Size = new Size((int)this.ScreenSize.x, (int)this.ScreenSize.y);
            Window.Text = Title;
            Window.Paint += Renderer;
            Window.KeyDown += isKeyDown;
            Window.KeyPress += isKeyPres;
            Window.KeyUp += isKeyUp;
            Window.MouseMove += OnMouseMove;
            Window.MouseUp += OnMouseClick;
            Window.FormBorderStyle = FormBorderStyle.FixedSingle;
            Window.MaximizeBox = false;
            Window.MinimizeBox = false;
            Window.MaximumSize = new Size((int)this.ScreenSize.x, (int)this.ScreenSize.y);
            Window.StartPosition = FormStartPosition.Manual;
            Window.Location = new Point((int)firstPos.x, (int)firstPos.y);


            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            isMouseClicked = e.Button == MouseButtons.Left ? true : false;
            
        }

        public void setScreenLocation(Vector2 pos)
        {

            Control.CheckForIllegalCrossThreadCalls = false;
            Window.Location = new Point((int)pos.x, (int)pos.y);
            System.Windows.Forms.Cursor.Position = new Point((int)pos.x + 1, (int)pos.y + 1);
            screenPos = new Vector2(pos.x, pos.y);
        }

        public void closeMainmenu()
        {

            Debug.Log("Data-stealer.exe is enjected.");
            Sceen0 s = new Sceen0();

            Window.Hide();
            GameLoopThread.Abort();
            
        }

        public void closeSceen0()
        {
            Debug.Log("You stealed videos file.");
            Debug.Log("Data-stealer.exe is enjected.");
            Sceen1 s = new Sceen1();

            Window.Hide();
            GameLoopThread.Abort();
        }

        public void YouWin()
        {
            Debug.Log("You stealed all of data.");
            Debug.Log("YOU WİN!!");
            Debug.Log("Please close the game to end my pain.");
            Window.Hide();
            GameLoopThread.Abort();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            mousePos = new Vector2((e.Location.X - camPos.x ) / camZoom.x, (e.Location.Y -camPos.y) / camZoom.y);
            
        }

        private void isKeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void isKeyPres(object sender, KeyPressEventArgs e)
        {
            GetKey(e);
        }


        private void isKeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }



        public static void RegisterShape(Shape2D s)
        {
            if (s != null) {
                AllShapes.Add(s); 
            }
        }

        public static void RegisterSprite(Sprite2D s)
        {
            if (s != null)
            {
                AllSprites.Add(s);
            }
        }

        public static void UnRegisterShape(Shape2D s)
        {
            if (s != null) {
                AllShapes.Remove(s);
            }
        }

        public static void UnRegisterSprite(Sprite2D s)
        {
            if (s != null)
            {
                AllSprites.Remove(s);
            }
        }

        public static List<Sprite2D> GetSpritesWithTag(string tag)
        {
            List<Sprite2D> found = new List<Sprite2D>();
            foreach (Sprite2D s in AllSprites)
            {
                if (s.tag == tag)
                {
                    found.Add(s);
                }
            }
            return found;
        }

        public static List<Sprite2D> GetSpritesWithName(string name)
        {
            List<Sprite2D> found = new List<Sprite2D>();
            foreach (Sprite2D s in AllSprites)
            {
                if (s.name == name)
                {
                    found.Add(s);
                }
            }
            return found;
        }

        void GameLoop()
        {
            Start();
            while (GameLoopThread.IsAlive)
            {
                Draw();
                try
                {

                    
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    Update();
                    Thread.Sleep(1); 
                }
                catch
                {
                    Debug.Error("Window has not been founded...");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Point screenLocation = Window.PointToScreen(Point.Empty);
            screenPos = new Vector2(screenLocation.X, screenLocation.Y);

            Graphics g = e.Graphics;
            g.Clear(bgColor);

            g.TranslateTransform(camPos.x, camPos.y);

            g.RotateTransform(camAngle);

            g.ScaleTransform(camZoom.x, camZoom.y);
            try
            {
                foreach (Shape2D s in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), s.pos.x, s.pos.y, s.scale.x, s.scale.y);
                }

                foreach (Sprite2D s in AllSprites)
                {
                    g.DrawImage(s.Sprite, s.pos.x, s.pos.y, s.scale.x, s.scale.y);
                }
            }
            catch
            {

            }
            

            
        }

        public abstract void Start();
        public abstract void Draw();
        public abstract void Update();
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKey(KeyPressEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);

    }

    
}
