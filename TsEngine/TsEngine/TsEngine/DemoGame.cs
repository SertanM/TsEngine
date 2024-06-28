using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TsEngine.TsEngine;

namespace TsEngine
{
    class DemoGame : TsEngine.TsEngine
    {
        public DemoGame() : base(new Vector2(600, 600), Vector2.Center(600, 600) ,Color.FromArgb(34,32,52), "Demo"){    }

        Sprite2D player;
        Vector2 lastPos = Vector2.Zero();
        Sound jumpSFX = new Sound("Jump.wav");
        Sound coinSFX = new Sound("Coin.wav");
        bool keyA, keyD, isOnGround;
        bool isFlip;
        

        string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "c", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", ".", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", "g", "g", ".", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", "c", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "k", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", "g", ".", ".", ".", ".", "k", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", "g", "g", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", "g", "g", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", "p", "g", "g", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", "g", "g", "g", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", "g", "g", "g", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", "g", "g", "g", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
        };


        public override void Start()
        {
            camZoom = Vector2.One();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for(int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == "g")
                    {
                        new Sprite2D(new Vector2(j * 64, i *64), new Vector2(66, 66), "T", "wall(clone)", "wall");
                    }
                    if (Map[i, j] == "p")
                    {
                        player = new Sprite2D(new Vector2(j * 64 + 9, i * 64 + 9), new Vector2(32, 32), "T", "Player", "player");
                    }
                    if (Map[i, j] == "k")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(52, 52), "kral", "kral(clone)", "kral");
                    }
                }
            }
        
        }
        int coin = 0;
        public override void Update()
        {
            
            
            Sprite2D wall = player.IsCollidingWithTag("wall");
            if (player.IsCollidingWithTag("wall") != null)
            {
                player.pos.x = lastPos.x;
                player.pos.y = lastPos.y;
                player.velocity.y = 0;
                if (wall.pos.y > player.pos.y)
                {
                    isOnGround = true;
                }
            }
            else
            {
                lastPos.x = player.pos.x;
                lastPos.y = player.pos.y;
                if(player.velocity.y!=0f) isOnGround = false;
                player.velocity.y += .2f;
                
            }
            Sprite2D Coin = player.IsCollidingWithTag("coin");
            if (Coin != null)
            {
                Coin.DestroySelf();
                coin++;
            }

            if(player.IsCollidingWithTag("kral") != null){
                player.velocity.y = -10f;
            }

            if (keyA) player.pos.x -= 4f;
            if (keyD) player.pos.x += 4f;

            player.ApplyVelocity();
            
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (isOnGround)
                {
                    jumpSFX.playSound();
                    player.pos.y = lastPos.y;
                    player.pos.x = lastPos.x;
                    player.velocity.y -= 7f;
                }
            }


            if (e.KeyCode == Keys.A) { keyA = true;
                if (!isFlip)
                {
                    isFlip = true;
                    player.FlipImageY();
                }
            }
            if (e.KeyCode == Keys.D){   keyD = true;
                if(isFlip)
                {
                    isFlip = false; 
                    player.FlipImageY();
                }
            }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) keyA = false;
            if (e.KeyCode == Keys.D) keyD = false;
        }

        public override void Draw() {
            camPos.x = -screenPos.x;
            camPos.y = -screenPos.y;
        }

        public override void GetKey(KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
