using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TsEngine.TsEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TsEngine
{
    internal class Sceen0 : TsEngine.TsEngine
    {
        public Sceen0() : base(new Vector2(800, 600), Vector2.Center(800, 600), Color.White, "StealVideos.exe") { }
        

        Sprite2D player;
        Sprite2D bottom;
        Vector2 lastPos = Vector2.Zero();
        float platformY, platformY1;
        Sound jumpSFX = new Sound("Jump.wav");
        Sound dieSFX = new Sound("die.wav");
        bool keyA, keyD, isOnGround, isButtonPressed, dontCollide, isWillGo;
        
        float speed = 3f;
        int aCounter = 0;
        Sprite2D platform, platform1, button;
        List<Sprite2D> a1 = new List<Sprite2D>();
        List<Sprite2D> a2 = new List<Sprite2D>();
        float aY;
        Vector2 playersFirstPos = new Vector2();

        

        string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "m", "g", "g", "m", "g", "g", "g", "g", "g", "m", "g", "g", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", "d", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", ".", ".", ".", "m", ".", ".", "n",".", ".", ".", ".", ".", "n", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "n", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "a", ".", ".", ".", "a", ".", ".", ".", "a", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", "g", ".", ".", "g", ".", "t1", ".", ".", ".", "g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", "b", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g"},
            {"g", ".", "t0", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "m", ".", "g", ".", ".", "m", "g", ".", "m", "g", ".", ".", ".", ".", "g"},
            {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "m", ".", "g", "g", ".", "m", "g", "g", "m", "g", "m", "n", ".", "p", "g"},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
        };

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) keyA = true;
            if (e.KeyCode == Keys.D) keyD = true;

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
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.A) keyA = false;
            if(e.KeyCode == Keys.D) keyD = false;
        }

        public override void Start()
        {
            new Sprite2D(Vector2.Zero(), new Vector2(1920, 1080), "Game\\bg");
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == "g")
                    {
                        Random rnd = new Random();
                        if(rnd.Next(0, 2) == 0) new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\UI\\a", "wall(clone)", "wall");
                        else new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\UI\\b", "wall(clone)", "wall");
                    }
                    else if (Map[i, j] == "t0")
                    {
                        platform = new Sprite2D(new Vector2(j * 64, i * 64 + 32), new Vector2(66, 20), "Game\\Tile\\red", "wall(clone)", "wall");
                        platformY = platform.pos.y;
                    }
                    else if (Map[i, j] == "t1")
                    {
                        platform1 = new Sprite2D(new Vector2(j * 64, i * 64 + 32), new Vector2(66, 20), "Game\\Tile\\red", "wall(clone)", "wall");
                        platformY1 = platform1.pos.y;
                    }
                    else if (Map[i, j] == "b")
                    {
                        button = new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(128, 128), "Game\\UI\\button_red");
                    }
                    else if (Map[i,j] == "a")
                    {
                        if(aCounter %2 == 0)
                        {
                            a1.Add(new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\Tile\\bumper", "bump(clone)", "bumper"));
                            
                        }
                        else
                        {
                            a2.Add(new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\Tile\\bumper", "bump(clone)", "bumper"));
                            aY = i * 64;
                        }
                        aCounter++;
                        
                    }
                    else if (Map[i, j] == "n")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\Tile\\bumper", "bump(clone)", "bumper");
                    }
                    else if (Map[i, j] == "m")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64 - 2), new Vector2(66, 66), "Game\\Tile\\copkutusu", "cop(clone)", "cop");
                    }
                    else if (Map[i, j] == "p")
                    {
                        player = new Sprite2D(new Vector2(j * 64, i *64), new Vector2(62, 46), "Game\\Characters\\Alien", "player", "player");
                        bottom = new Sprite2D(Vector2.Zero(), new Vector2(62, 1), "Game\\blank");
                        playersFirstPos.x = player.pos.x;
                        playersFirstPos.y = player.pos.y;
                        this.setScreenLocation(new Vector2(player.pos.x - 700, player.pos.y - 520));
                    }
                    else if (Map[i, j] == "d")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(66, 66), "Game\\Tile\\DOSYA", "dosya", "dosya");
                    }
                }
            }

            
            isButtonPressed = false;
        }

        public override void Update()
        {
            bottom.pos.x = player.pos.x;
            bottom.pos.y = player.pos.y + 47;
            if (keyA) player.pos.x -= speed;
            if (keyD) player.pos.x += speed;
            Sprite2D wall = player.IsCollidingWithTag("wall");
            Sprite2D wall1 = bottom.IsCollidingWithTag("wall");
            Sprite2D bumper = player.IsCollidingWithTag("bumper");
            Sprite2D cop = player.IsCollidingWithTag("cop");
            Sprite2D dosya = player.IsCollidingWithTag("dosya");

            if (dosya != null)
            {
                closeSceen0();
            }

            if (wall1 != null)
            {
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }
            
            if (wall != null)
            {
                if (!dontCollide)
                {
                    player.pos.x = lastPos.x;
                    player.pos.y = lastPos.y;
                }
                player.velocity.y = 0;
            }
            else
            {
                if(!dontCollide)
                {
                    lastPos.x = player.pos.x;
                    lastPos.y = player.pos.y;
                    player.velocity.y += .2f;
                }
            }

            if(bumper != null && wall == null)
            {
                player.velocity.y = -7f;
            }
            
            if(cop != null)
            {
                dieSFX.playSound();
                player.pos.x = playersFirstPos.x;
                player.pos.y = playersFirstPos.y;
                this.setScreenLocation(new Vector2(player.pos.x - 700, player.pos.y - 520));
            }

            player.ApplyVelocity();


            //------------------------------------------------------

            

            if (button.IsCursorCollidingWith())
            {
                if (isMouseClicked)
                {
                    isMouseClicked = false;
                    isButtonPressed = !isButtonPressed;
                    Debug.Log(isButtonPressed.ToString());
                    if (button.directory == "Game\\UI\\button_red") button.directory = "Game\\UI\\button_green";
                    else button.directory = "Game\\UI\\button_red";
                    button.restartImage();
                }
            }
            else
            {
                isMouseClicked = false;
            }


            if (isButtonPressed)
            {
                if(platform.pos.y > platformY - 300)
                {
                    if (wall1 == platform)
                    {
                        dontCollide = true;
                        isOnGround = true;
                        player.pos.y -= 1;
                        lastPos.x = player.pos.x;
                        lastPos.y = player.pos.y - 3;
                        player.velocity.y = 0;
                    }
                    else
                    {
                        dontCollide = false;
                    }
                    platform.pos.y -= 1;

                }
                else
                {
                    dontCollide = false;
                    
                }

                if (platform1.pos.y > platformY1 - 300)
                {
                    if (wall1 == platform1)
                    {
                        dontCollide = true;
                        isOnGround = true;
                        player.pos.y -= 1;
                        lastPos.x = player.pos.x;
                        lastPos.y = player.pos.y - 3;
                        player.velocity.y = 0;
                    }
                    else
                    {
                        dontCollide = false;
                    }
                    platform1.pos.y -= 1;
                }
            }
            else
            {
                if (platform.pos.y < platformY)
                {
                    platform.pos.y += 1f;
                }
                if (platform1.pos.y < platformY1)
                {
                    platform1.pos.y += 1f;
                }
                dontCollide = false;
            }

            //------------------------------------------------------

            foreach(Sprite2D an1 in a1)
            {
                if(isWillGo)an1.pos.y += 1;
                else an1.pos.y -= 1;
                if(an1.pos.y >= aY + 100)
                {
                    isWillGo = false;
                }
                else if(an1.pos.y <= aY - 100)
                {
                    isWillGo = true;
                }
            }
            foreach(Sprite2D an2 in a2)
            {
                if (isWillGo) an2.pos.y -= 1;
                else an2.pos.y += 1;
            }
        }

        public override void Draw() {
            camPos.x = -screenPos.x;
            camPos.y = -screenPos.y;
        }

        public override void GetKey(KeyPressEventArgs e){}

    }
}
