using LonelyHill.Audio;
using LonelyHill.Math;
using LonelyHill.UI;
using LonelyHill.Utlity;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryGame
{
    public class MainMenu
    {
        private Text title = null;
        private Text load = null;
        private Text newgame = null;
        private Text settings = null;
        private Text credits = null;
        private Text quit = null;

        private string selectedItem = "newgame";

        public void Primary()
        {
            if (title == null)
            {
                title = new Text();
                title.font = Program.font;
                title.text = "FactoryGame";
                Program.AddRenderable(title);

                load = new Text();
                load.font = Program.font;
                load.text = "Load game";
                load.FontColor = new Vector4(100.0f, 100.0f, 100.0f, 255.0f);
                Program.AddRenderable(load);

                newgame = new Text();
                newgame.font = Program.font;
                newgame.text = ">New Game";
                Program.AddRenderable(newgame);

                settings = new Text();
                settings.font = Program.font;
                settings.text = "Settings";
                Program.AddRenderable(settings);

                credits = new Text();
                credits.font = Program.font;
                credits.text = "Credits";
                Program.AddRenderable(credits);

                quit = new Text();
                quit.font = Program.font;
                quit.text = "Quit";
                Program.AddRenderable(quit);
            }

            float scaleY = Program.GetEngine().GetScaleVector().y;

            title.transform.Position = new Vector3(20.0f, -6.0f * scaleY);
            title.Scale = 3.6f * scaleY;

            load.transform.Position = new Vector3(22.0f, 100.0f * scaleY);
            load.Scale = 1.6f * scaleY;

            newgame.transform.Position = new Vector3(22.0f, 140.0f * scaleY);
            newgame.Scale = 1.6f * scaleY;

            settings.transform.Position = new Vector3(22.0f, 180.0f * scaleY);
            settings.Scale = 1.6f * scaleY;

            credits.transform.Position = new Vector3(22.0f, 220.0f * scaleY);
            credits.Scale = 1.6f * scaleY;

            quit.transform.Position = new Vector3(22.0f, 260.0f * scaleY);
            quit.Scale = 1.6f * scaleY;

            if (load.IsHovered)
            {
                //Not yet!
            }
            else if (newgame.IsHovered)
            {
                if (selectedItem != "newgame")
                {
                    Program.buttonHoverAudio.Play();
                }

                selectedItem = "newgame";

                if (Program.GetEngine().mouse.LeftButtonDown)
                {
                    Program.buttonPressAudio.Play();
                    Program.location = Location.NewGame;
                }
            }
            else if (settings.IsHovered)
            {
                if (selectedItem != "settings")
                {
                    Program.buttonHoverAudio.Play();
                }

                selectedItem = "settings";

                if (Program.GetEngine().mouse.LeftButtonDown)
                {
                    Program.buttonPressAudio.Play();
                    Program.settingsMenu.Visible = true;
                }
            }
            else if (credits.IsHovered)
            {
                if (selectedItem != "credits")
                {
                    Program.buttonHoverAudio.Play();
                }

                selectedItem = "credits";
            }
            else if (quit.IsHovered)
            {
                if (selectedItem != "quit")
                {
                    Program.buttonHoverAudio.Play();
                }

                selectedItem = "quit";

                if (Program.GetEngine().mouse.LeftButtonDown)
                {
                    Program.buttonPressAudio.Play();
                    Program.IsRunning = false;
                }
            }

            switch (selectedItem)
            {
                case "loadgame":
                    load.text = ">Load Game";
                    newgame.text = "New Game";
                    settings.text = "Settings";
                    credits.text = "Credits";
                    quit.text = "Quit";
                    break;
                case "newgame":
                    load.text = "Load Game";
                    newgame.text = ">New Game";
                    settings.text = "Settings";
                    credits.text = "Credits";
                    quit.text = "Quit";
                    break;
                case "settings":
                    load.text = "Load Game";
                    newgame.text = "New Game";
                    settings.text = ">Settings";
                    credits.text = "Credits";
                    quit.text = "Quit";
                    break;
                case "credits":
                    load.text = "Load Game";
                    newgame.text = "New Game";
                    settings.text = "Settings";
                    credits.text = ">Credits";
                    quit.text = "Quit";
                    break;
                case "quit":
                    load.text = "Load Game";
                    newgame.text = "New Game";
                    settings.text = "Settings";
                    credits.text = "Credits";
                    quit.text = ">Quit";
                    break;
            }
        }

        public void Cleanup()
        {
            if (title != null)
            {
                Program.GetEngine().rendererClass.Remove(title);
                title = null;

                Program.GetEngine().rendererClass.Remove(load);
                load = null;

                Program.GetEngine().rendererClass.Remove(newgame);
                newgame = null;

                Program.GetEngine().rendererClass.Remove(settings);
                settings = null;

                Program.GetEngine().rendererClass.Remove(credits);
                credits = null;

                Program.GetEngine().rendererClass.Remove(quit);
                quit = null;
            }
        }
    }
}
