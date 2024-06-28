using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TsEngine.TsEngine;

namespace TsEngine
{
    public class MainMenu : TsEngine.TsEngine
    {
        public MainMenu() : base(new Vector2(800, 600), Vector2.Center(800, 600), Color.White, "Are You Accept?") { }

        
        public override void Draw(){}

        public override void GetKeyDown(KeyEventArgs e){}

        public override void GetKeyUp(KeyEventArgs e){}

        Sprite2D yes, no;

        public override void Start()
        {
            new Sprite2D(new Vector2(20, 70), new Vector2(493, 76), "Game\\UI\\USER AGREMENT");
            yes = new Sprite2D(new Vector2(50, 200), new Vector2(226, 120), "Game\\UI\\YES");
            no = new Sprite2D(new Vector2(60, 350), new Vector2(56, 30), "Game\\UI\\NO");
        }

        public override void Update()
        {
            if(isMouseClicked)
            {
                if (yes.IsCursorCollidingWith())
                {
                    closeMainmenu();

                }
                else
                {
                    isMouseClicked = false;
                }
                if(no.IsCursorCollidingWith())
                {
                    Environment.Exit(0);
                }
            }
            
        }

        public override void GetKey(KeyPressEventArgs e)
        {
        }

    }
}