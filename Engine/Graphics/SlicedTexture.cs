using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Graphics
{
    public enum SliceLocation
    {
        LeftTop,
        CenterTop,
        RightTop,
        LeftCenter,
        CenterCenter,
        RightCenter,
        LeftBottom,
        CenterBottom,
        RightBottom
    }

    public class SlicedTexture : Texture
    {
        private Dictionary<SliceLocation, Slice> slices;

        public SlicedTexture(string path) : base(path)
        {
            JObject sliceData = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(path + ".json"));

            slices = new Dictionary<SliceLocation, Slice>();
            slices.Add(SliceLocation.LeftTop, HandleSlice(sliceData.GetValue("left-top").ToString()));
            slices.Add(SliceLocation.CenterTop, HandleSlice(sliceData.GetValue("center-top").ToString()));
            slices.Add(SliceLocation.RightTop, HandleSlice(sliceData.GetValue("right-top").ToString()));
            slices.Add(SliceLocation.LeftCenter, HandleSlice(sliceData.GetValue("left-center").ToString()));
            slices.Add(SliceLocation.CenterCenter, HandleSlice(sliceData.GetValue("center-center").ToString()));
            slices.Add(SliceLocation.RightCenter, HandleSlice(sliceData.GetValue("right-center").ToString()));
            slices.Add(SliceLocation.LeftBottom, HandleSlice(sliceData.GetValue("left-bottom").ToString()));
            slices.Add(SliceLocation.CenterBottom, HandleSlice(sliceData.GetValue("center-bottom").ToString()));
            slices.Add(SliceLocation.RightBottom, HandleSlice(sliceData.GetValue("right-bottom").ToString()));
        }

        public SDL.SDL_Rect GetSliceSrcRect(SliceLocation location)
        {
            Slice slice = slices[location];

            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            rect.x = slice.originX;
            rect.y = slice.originY;
            rect.w = slice.getWidth();
            rect.h = slice.getHeight();

            return rect;
        }

        public SDL.SDL_Rect TransformSliceRect(SDL.SDL_Rect rect, SliceLocation location)
        {
            Slice slice = slices[location];

            rect.w = slice.getWidth();
            rect.h = slice.getHeight();

            return rect;
        }

        private Slice HandleSlice(string sliceStr)
        {
            string[] sliceSplit = sliceStr.Split(' ');

            Slice slice = new Slice();
            slice.originX = int.Parse(sliceSplit[0]);
            slice.originY = int.Parse(sliceSplit[2]);
            slice.destinationX = int.Parse(sliceSplit[1]);
            slice.destinationY = int.Parse(sliceSplit[3]);

            return slice;
        }

        public Slice GetSlice(SliceLocation location)
        {
            return slices[location];
        }
    }

    public struct Slice
    {
        public int originX;
        public int originY;
        public int destinationX;
        public int destinationY;

        public int getWidth()
        {
            return destinationX - originX;
        }

        public int getHeight()
        {
            return destinationY - originY;
        }
    }
}
