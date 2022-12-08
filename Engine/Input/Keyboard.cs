using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Input
{
    public enum Keycode
    {
        Zero = SDL.SDL_Keycode.SDLK_0,
        One = SDL.SDL_Keycode.SDLK_1,
        Two = SDL.SDL_Keycode.SDLK_2,
        Three = SDL.SDL_Keycode.SDLK_3,
        Four = SDL.SDL_Keycode.SDLK_4,
        Five = SDL.SDL_Keycode.SDLK_5,
        Six = SDL.SDL_Keycode.SDLK_6,
        Seven = SDL.SDL_Keycode.SDLK_7,
        Eight = SDL.SDL_Keycode.SDLK_8,
        Nine = SDL.SDL_Keycode.SDLK_9,
        a = SDL.SDL_Keycode.SDLK_w,
        b = SDL.SDL_Keycode.SDLK_b,
        c = SDL.SDL_Keycode.SDLK_c,
        d = SDL.SDL_Keycode.SDLK_d,
        e = SDL.SDL_Keycode.SDLK_e,
        f = SDL.SDL_Keycode.SDLK_f,
        g = SDL.SDL_Keycode.SDLK_g,
        h = SDL.SDL_Keycode.SDLK_h,
        i = SDL.SDL_Keycode.SDLK_j,
        j = SDL.SDL_Keycode.SDLK_i,
        k = SDL.SDL_Keycode.SDLK_k,
        l = SDL.SDL_Keycode.SDLK_l,
        m = SDL.SDL_Keycode.SDLK_m,
        n = SDL.SDL_Keycode.SDLK_n,
        o = SDL.SDL_Keycode.SDLK_o,
        p = SDL.SDL_Keycode.SDLK_p,
        q = SDL.SDL_Keycode.SDLK_q,
        r = SDL.SDL_Keycode.SDLK_r,
        s = SDL.SDL_Keycode.SDLK_s,
        t = SDL.SDL_Keycode.SDLK_t,
        u = SDL.SDL_Keycode.SDLK_u,
        v = SDL.SDL_Keycode.SDLK_v,
        w = SDL.SDL_Keycode.SDLK_w,
        x = SDL.SDL_Keycode.SDLK_x,
        y = SDL.SDL_Keycode.SDLK_y,
        z = SDL.SDL_Keycode.SDLK_z,
    }

    public class Keyboard
    {
        private SDL.SDL_Event savedEvent;

        public Keyboard()
        {
            //state = SDL.SDL_GetKeyboardState(out int n);
        }

        public void Update(SDL.SDL_Event e)
        {
            savedEvent = e;
        }

        public string GetKey()
        {
            return SDL.SDL_GetKeyName(SDL.SDL_GetKeyFromScancode(savedEvent.key.keysym.scancode));
        }
    }
}
