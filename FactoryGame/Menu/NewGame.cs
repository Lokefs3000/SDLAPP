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
    public class NewGame
    {
        private Text title = null;
        private Text gameNameTitle = null;
        private SlicedTextBox gameName = null;
        private Text gameSeedTitle = null;
        private SlicedTextBox gameSeed = null;
        private Text createGame = null;
        private Text goBack = null;

        private string selectedItem = "create";

        public void Primary()
        {
            if (title == null)
            {
                title = new Text();
                title.font = Program.font;
                title.text = "New game";
                title.Align = TextAlign.Center;
                Program.AddRenderable(title);

                gameNameTitle = new Text();
                gameNameTitle.font = Program.font;
                gameNameTitle.text = "World name:";
                gameNameTitle.Align = TextAlign.Center;
                Program.AddRenderable(gameNameTitle);

                gameName = new SlicedTextBox();
                gameName.Width = 500;
                gameName.Height = 35;
                gameName.slicedimage.slicedtexture = Program.gray_textbox;
                gameName.SetFont(Program.font);
                gameName.templateText = "Suitable game name here..";
                gameName.text = "New World";
                gameName.ImageColor = new Vector4(100.0f, 100.0f, 100.0f, 255.0f);
                gameName.primaryText.Scale = 1.5f;
                Program.AddRenderable(gameName);

                gameSeedTitle = new Text();
                gameSeedTitle.font = Program.font;
                gameSeedTitle.text = "World seed:";
                gameSeedTitle.Align = TextAlign.Center;
                Program.AddRenderable(gameSeedTitle);

                gameSeed = new SlicedTextBox();
                gameSeed.Width = 500;
                gameSeed.Height = 35;
                gameSeed.slicedimage.slicedtexture = Program.gray_textbox;
                gameSeed.SetFont(Program.font);
                gameSeed.templateText = "Suitable game name here..";
                gameSeed.text = "New World";
                gameSeed.ImageColor = new Vector4(100.0f, 100.0f, 100.0f, 255.0f);
                gameSeed.primaryText.Scale = 1.5f;
                Program.AddRenderable(gameSeed);

                createGame = new Text();
                createGame.font = Program.font;
                createGame.text = "Create game";
                createGame.Align = TextAlign.Center;
                Program.AddRenderable(createGame);

                goBack = new Text();
                goBack.font = Program.font;
                goBack.text = "Cancel";
                goBack.Align = TextAlign.Center;
                Program.AddRenderable(goBack);
            }

            float scaleY = Program.GetEngine().GetScaleVector().y;
            float scaleX = Program.GetEngine().GetScaleVector().x;
            Vector2 size = Program.GetEngine().GetSize();

            title.transform.Position = new Vector3(size.x / 2, -6.0f * scaleY);
            title.Scale = 2.6f * scaleY;

            gameNameTitle.transform.Position = new Vector3(size.x / 2, 110.0f * scaleY);
            gameNameTitle.Scale = 1.7f * scaleY;

            gameName.transform.Position = new Vector3(size.x / 2.0f - gameName.Width / 2.0f, 160.0f * scaleY, 0.0f);
            gameName.Height = (int)(35.0f * scaleY);
            gameName.Width = (int)(500.0f * scaleX);
            gameName.primaryText.Scale = 1.5f * scaleY;

            gameSeedTitle.transform.Position = new Vector3(size.x / 2, 220.0f * scaleY);
            gameSeedTitle.Scale = 1.7f * scaleY;

            gameSeed.transform.Position = new Vector3(size.x / 2.0f - gameSeed.Width / 2.0f, 270.0f * scaleY, 0.0f);
            gameSeed.Height = (int)(35.0f * scaleY);
            gameSeed.Width = (int)(500.0f * scaleX);
            gameSeed.primaryText.Scale = 1.5f * scaleY;

            createGame.transform.Position = new Vector3(size.x / 2, 570.0f * scaleY);
            createGame.Scale = 1.5f * scaleY;

            goBack.transform.Position = new Vector3(size.x / 2, 615.0f * scaleY);
            goBack.Scale = 1.5f * scaleY;

            if (createGame.IsHovered)
            {
                if (selectedItem != "create")
                {
                    if (!Program.buttonHoverAudio.IsPlaying())
                    {
                        Program.buttonHoverAudio.Play();
                    }
                }

                selectedItem = "create";

                if (Program.GetEngine().mouse.LeftButtonDown)
                {
                    Program.location = Location.Loading;
                    Program.buttonPressAudio.Play();
                }
            }
            if (goBack.IsHovered)
            {
                if (selectedItem != "back")
                {
                    if (!Program.buttonHoverAudio.IsPlaying())
                    {
                        Program.buttonHoverAudio.Play();
                    }
                }

                selectedItem = "back";

                if (Program.GetEngine().mouse.LeftButtonDown)
                {
                    Program.location = Location.Menu;
                    Program.buttonPressAudio.Play();
                }
            }

            switch (selectedItem)
            {
                case "create":
                    createGame.text = ">Create game";
                    goBack.text = "Cancel";
                    break;
                case "back":
                    createGame.text = "Create game";
                    goBack.text = ">Cancel";
                    break;
            }
        }

        public void Cleanup()
        {
            if (title != null)
            {
                Program.GetEngine().rendererClass.Remove(title);
                title = null;

                Program.GetEngine().rendererClass.Remove(gameNameTitle);
                gameNameTitle = null;

                Program.GetEngine().rendererClass.Remove(gameName);
                gameName.Cleanup();
                gameName = null;

                Program.GetEngine().rendererClass.Remove(gameSeedTitle);
                gameSeedTitle = null;

                Program.GetEngine().rendererClass.Remove(gameSeed);
                gameSeed.Cleanup();
                gameSeed = null;

                Program.GetEngine().rendererClass.Remove(createGame);
                createGame = null;

                Program.GetEngine().rendererClass.Remove(goBack);
                goBack = null;
            }
        }
    }
}
