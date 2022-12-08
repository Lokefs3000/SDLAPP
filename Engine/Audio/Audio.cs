using LonelyHill.Core;
using LonelyHill.Math;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Audio
{
    public class Audio
    {
        public IntPtr audio { get; private set; }
        private int channel = 0;

        public float AudioVolume = 100.0f;

        public Audio(string path)
        {
            audio = SDL_mixer.Mix_LoadWAV(path);
            if (audio == null)
            {
                Engine.logger.error("Failed to load audio:", path, ", Mixer Error:", SDL_mixer.Mix_GetError());
                return;
            }

            while (true)
            {
                if (!AudioVar.channelsInUse.Contains(channel))
                    break;

                channel++;
            }

            AudioVar.channelsInUse.Add(channel);

            Engine.logger.info("Created audio on channel: " + channel);
        }

        public void Play(bool fade = false, int fadeInMS = 0)
        {
            int code = 0;

            if (fade)
                code = SDL_mixer.Mix_FadeInChannel(channel, audio, 0, fadeInMS);
            else
                code = SDL_mixer.Mix_PlayChannel(channel, audio, 0);

            if (code < 0)
            {
                Engine.logger.error("Failed to play audio, Mixer Error:", SDL_mixer.Mix_GetError());
            }
        }

        public void Stop(bool fade = false, int fadeOutMS = 0)
        {
            int code = 0;

            if (fade)
                code = SDL_mixer.Mix_FadeOutChannel(channel, fadeOutMS);
            else
                code = SDL_mixer.Mix_HaltChannel(channel);

            if (code < 0)
            {
                Engine.logger.error("Failed to stop audio, Mixer Error:", SDL_mixer.Mix_GetError());
            }
        }

        public void ChangeVolume(float volume = 100.0f)
        {
            if (!IsPlaying())
                return;

            if (SDL_mixer.Mix_Volume(channel, -1) == volume)
                return;

            AudioVolume = volume;
            int code = SDL_mixer.Mix_Volume(channel, (int)System.Math.Round(Engine.Instance.GlobalAudioVolume * (volume / 100.0f)));

            if (code < 0)
            {
                Engine.logger.error("Failed to change audio volume, Mixer Error:", SDL_mixer.Mix_GetError());
            }
        }

        public bool IsPlaying()
        {
            return SDL_mixer.Mix_Playing(channel) > 0;
        }

        public void Cleanup()
        {
            if (audio != null)
            {
                Engine.logger.info("Removing audio channel", channel.ToString());
                Stop();
                if (audio != null)
                    SDL_mixer.Mix_FreeChunk(audio);
                AudioVar.channelsInUse.Remove(channel);
            }
        }
    }
}
