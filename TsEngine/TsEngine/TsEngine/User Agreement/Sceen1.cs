using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TsEngine.TsEngine;

namespace TsEngine
{
    internal class Sceen1 : TsEngine.TsEngine
    {
        public Sceen1() : base(new Vector2(400, 300), Vector2.Zero(), Color.White, "Pasword-Stealler.exe") { }
        
        Sprite2D player;
        int oldI = 0;
        Sound die = new Sound("die.wav");

        string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", ".", ".", ".", "g", "g", ".", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", "g", "g", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", "g", "g", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", "g"},
            {"g", ".", "g", ".", "g", "g", "g", "g", "g", "g", "g", ".", "g", ".", ".", "g", ".", "g", "g", "g", "g", "g", "g", "g", ".", "g", ".", ".", ".", "g"},
            {"g", ".", "g", ".", ".", ".", ".", ".", ".", ".", "g", ".", "g", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", "g", ".", "g", "g", "g", "g", "g"}, 
            {"g", ".", "g", "g", "g", "g", "g", ".", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", ".", "g", ".", "g", "g", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", "g", ".", "g", ".", "g", ".", ".", ".", ".", ".", ".", ".", "g", ".", "g", ".", ".", ".", "g", "g", "g", ".", "1", "g"},
            {"g", "g", "g", "g", "g", ".", "g", ".", ".", ".", "g", ".", ".", ".", "g", "g", "g", ".", "g", ".", "g", ".", ".", ".", ".", ".", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", "g", ".", "g", "g", "g", "g", "g", ".", "g", "g", "g", ".", "g", ".", "g", ".", "g", "g", "g", "g", "g", ".", ".", "g"},
            {"g", ".", ".", ".", ".", "g", "g", ".", ".", ".", ".", "g", ".", ".", ".", ".", "g", ".", "g", ".", "g", ".", ".", ".", ".", ".", "g", ".", ".", "g"},
            {"g", ".", ".", "g", "g", ".", "g", "g", "g", ".", ".", "g", "g", "g", "g", ".", "g", ".", "g", ".", "g", ".", ".", ".", ".", ".", "g", ".", ".", "g"},
            {"g", ".", ".", ".", "g", ".", "g", ".", "g", "g", ".", "g", ".", ".", "g", ".", "g", ".", "g", ".", "g", "g", "g", "g", "g", ".", "g", ".", ".", "g"},
            {"g", ".", ".", ".", "g", ".", "g", ".", ".", "g", ".", "g", ".", ".", "g", ".", "g", ".", "g", ".", ".", ".", ".", ".", "g", ".", "g", ".", ".", "g"},
            {"g", ".", ".", ".", "g", ".", "g", ".", "g", "g", ".", "g", "g", "g", "g", ".", "g", ".", "g", "g", "g", "g", "g", ".", "g", ".", "g", "g", "g", "g"},
            {"g", "g", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", ".", "g"},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
        };

        public override void GetKeyDown(KeyEventArgs e)
        {

        }

        public override void GetKeyUp(KeyEventArgs e)
        {
        }
        public override void Start()
        {
            new Sprite2D(Vector2.Zero(), new Vector2(1920, 1080), "Game\\bg");
            player = new Sprite2D(Vector2.Zero(), new Vector2(62 * 0.75f, 46 * 0.75f), "Game\\Characters\\Alien", "player", "player");
            Debug.Log("MAP DATA: ");
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    
                    if(i == oldI) Console.Write(Map[i, j]);
                    else Console.WriteLine(Map[i, j]);
                    oldI = i;
                    if (Map[i, j] == "g")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\Characters\\AntiVirus\\AntiVirus", "antiVirus(clone)", "antiVirus");
                    }
                    else if (Map[i, j] == "2")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\Tile\\DOSYA", "win(clone)", "win");
                    }
                }
            }


        }

        public override void Update()
        {
            
        }

        public override void Draw() {

            if (player.IsCollidingWithTag("antiVirus") != null)
            {
                die.playSound();
                this.setScreenLocation(Vector2.Zero());
            }
            if(player.IsCollidingWithTag("win") != null)
            {
                YouWin();
            }
            if (screenPos.x <= -200)
            {
                this.setScreenLocation(Vector2.Zero());
            }

            camPos.x = -screenPos.x;
            camPos.y = -screenPos.y;
            player.pos.x = screenPos.x + (400f / 2f) - (player.scale.x);
            player.pos.y = screenPos.y + (300f / 2f) - (player.scale.y);

            
        }

        public override void GetKey(KeyPressEventArgs e){}

    }
}
