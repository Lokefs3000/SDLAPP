using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Audio
{
    public class AudioManager
    {
        public static Audio Create(string path)
        {
            Audio audio = new Audio(path);
            AudioVar.audioObjects.Add(audio);
            return audio;
        }

        public static void CleanAudio(Audio audio)
        {
            AudioVar.audioObjects.Remove(audio);
            audio.Cleanup();
        }

        public static void GlobalClean()
        {
            for (int i = 0; i < AudioVar.audioObjects.Count; i++)
            {
                AudioVar.audioObjects[i].Cleanup();
            }
        }
    }
}
