local uiLayout = nil
local logoSpeed = 4;

function BeginIntroduction()
    --Construct the introduction.uil layout
    uiLayout = Layout.CreateUILayout(getContent() .. "ui/layout/introduction.uil")

    --Get 4 Primary images
    local lokefs3000 = uiLayout:RetrieveImage("lokefs3000Image")
    lokefs3000.Color.w = 0;
    local lonelyhill = uiLayout:RetrieveImage("lonelyhillengImage")
    lonelyhill.Color.w = 0;
    local lua = uiLayout:RetrieveImage("luaImage")
    lua.Color.w = 0;
    local sdl = uiLayout:RetrieveImage("sdlImage")
    sdl.Color.w = 0;

    --Load the intro music
    local bgIntroMus = AudioManager.Create(getContent() .. "sound/intro_mus.wav");

    Logger.Info("Starting introduction animation.")

    --Play the music with a 700ms fade in
    bgIntroMus:Play()

    wait(0.3)

    for i = 1, 255 / logoSpeed do
        lokefs3000.Color.w = i * logoSpeed;
        lokefs3000.Offset.y = (10 - (i * logoSpeed / 255 * 10));

        lonelyhill.Color.w = i * logoSpeed;
        lonelyhill.Offset.y = (10 - (i * logoSpeed / 255 * 10));

        lua.Color.w = i * logoSpeed;
        lua.Offset.y = (10 - (i * logoSpeed / 255 * 10));

        sdl.Color.w = i * logoSpeed;
        sdl.Offset.y = (10 - (i * logoSpeed / 255 * 10));
        wait(0.01)
    end

    --Small wait
    wait(1)

    --Disepear
    for i = 1, 255 / logoSpeed do
        lokefs3000.Color.w = 255 - i * logoSpeed;
        lonelyhill.Color.w = 255 - i * logoSpeed;
        lua.Color.w = 255 - i * logoSpeed;
        sdl.Color.w = 255 - i * logoSpeed;
        wait(0.01)
    end

    --Cleanup to remove memory clutter
    uiLayout:Cleanup();

    --Fade out the music with a 1200ms fade out
    bgIntroMus:Stop(true, 1200)
    wait(1.2)

    --Remove the audio from memory
    AudioManager.CleanAudio(bgIntroMus)

    SetLocation("menu")
end 

BeginIntroduction()

--End the thread
Logger.Info("Introduction animation has ended, aborting execution thread!")
AbortThread()