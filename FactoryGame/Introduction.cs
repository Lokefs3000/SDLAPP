using LonelyHill.Audio;
using LonelyHill.Core;
using LonelyHill.Graphics;
using LonelyHill.Math;
using LonelyHill.Scripting;
using LonelyHill.Utlity;
using System;
using System.Threading;

namespace FactoryGame
{
    public class Introduction
    {
        private bool IsPlaying = false;

        private Script animationScript = null;

        public void IntroductionPrimary()
        {
            if (animationScript == null && !IsPlaying)
            {
                animationScript = Program.GetEngine().scripting.CreateScript(FileSystem.GetSource() + "/content/script/ui/introduction.lua");
                animationScript.Call();

                Console.WriteLine("called");

                IsPlaying = true;
            }
        }
    }
}
