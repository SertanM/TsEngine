using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TsEngine.TsEngine
{
    
    public class Sound
    {
        public readonly SoundPlayer mySound;

        public Sound(string soundAdress)
        {
            
            string a = Assembly.GetEntryAssembly().Location;
            a = a.Substring(0, a.LastIndexOf("T"));
            string path = a + $"Assets/Sounds/{soundAdress}";
            mySound = new SoundPlayer(path);
        }

        public void playSoundInLoop()
        {
            mySound.PlayLooping();
        }

        public void playSound()
        {
            mySound.Play();
        }

        public void stopSound()
        {
            mySound.Stop();
        }
    }
}
