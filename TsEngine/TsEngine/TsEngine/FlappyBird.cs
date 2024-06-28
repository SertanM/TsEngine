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
    class FlappyBird : TsEngine.TsEngine
    {
        public FlappyBird() : base(new Vector2(615, 517), Vector2.Zero(), Color.FromArgb(34, 32, 52), "FlappyBird") { }

        Sprite2D player, pipe0, pipe1, coin;

        public override void Start()
        {
            player = new Sprite2D(new Vector2(40, 60), new Vector2(32, 32));
        }

        public override void Update()
        {
            if (player != null) ControlPlayer();
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space) {
                player.velocity.y = -3f;
            }
        }

        void ControlPlayer()
        {
            player.velocity.y += .1f;
            if (player.pos.y <= 0f)
            {
                DeletePlayer();
            }

            player.ApplyVelocity();
        }

        void CreatePipe()
        {
            if(pipe0 != null)
            {
                pipe0.DestroySelf();
                pipe0 = null;
                pipe1.DestroySelf();
                pipe1 = null;

                if (coin != null)
                {
                    coin.DestroySelf();
                    coin = null;
                }

            }

            
        }

        void DeletePlayer()
        {
            if(player != null)
            {
                player.DestroySelf();
                player = null;
            }
        }

        public override void Draw() { }
        public override void GetKeyUp(KeyEventArgs e) { }

        public override void GetKey(KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
