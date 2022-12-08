using LonelyHill.Core;
using LonelyHill.Math;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LonelyHill.Graphics
{
    public class SlicedImage : Image
    {
        public SlicedTexture slicedtexture;
        public float SliceScale = 2.0f;

        private void renderSingle(SliceLocation location, Vector3 offset)
        {
            var src = slicedtexture.GetSliceSrcRect(location);
            var dest = slicedtexture.TransformSliceRect(transform.ToRectDest(Width, Height), location);

            dest.w = (int)((float)dest.w * SliceScale);
            dest.h = (int)((float)dest.h * SliceScale);

            dest.x += (int)offset.x;
            dest.y += (int)offset.y;

            SDL.SDL_SetTextureColorMod(slicedtexture.texturePtr, (byte)Color.x, (byte)Color.y, (byte)Color.z);
            SDL.SDL_SetTextureAlphaMod(slicedtexture.texturePtr, (byte)Color.w);

            SDL.SDL_RenderCopy(Engine.Instance.renderer, slicedtexture.texturePtr, ref src, ref dest);

            SDL.SDL_SetTextureColorMod(slicedtexture.texturePtr, 255, 255, 255);
            SDL.SDL_SetTextureAlphaMod(slicedtexture.texturePtr, 255);
        }

        public override void Update()
        {
            if (slicedtexture != null && Engine.Instance != null)
            {
                bool x = (Engine.Instance.mouse.MouseX >= transform.Position.x && Engine.Instance.mouse.MouseX < transform.Position.x + Width * transform.Scale.x);
                bool y = (Engine.Instance.mouse.MouseY >= transform.Position.y && Engine.Instance.mouse.MouseY < transform.Position.y + Height * transform.Scale.y);

                IsHovered = (x && y);
            }

            base.Update();
        }

        public override void Render()
        {
            float topWidth = slicedtexture.GetSlice(SliceLocation.CenterTop).getWidth() * SliceScale;
            float bottomWidth = slicedtexture.GetSlice(SliceLocation.CenterBottom).getWidth() * SliceScale;

            float topWidthRows = Width / topWidth - 1;
            float bottomWidthRows = Width / bottomWidth - 1;

            float lefttopsize = slicedtexture.GetSlice(SliceLocation.LeftTop).getWidth() * SliceScale;
            float righttopsize = slicedtexture.GetSlice(SliceLocation.RightTop).getWidth() * SliceScale;

            float topWidthInc = topWidth;
            float bottomWidthInc = bottomWidth;

            float leftHeightInc = slicedtexture.GetSlice(SliceLocation.LeftCenter).getHeight() * SliceScale;

            float leftHeigtColums = Height / leftHeightInc - 1;

            float rightpos = Width - slicedtexture.GetSlice(SliceLocation.RightTop).getWidth() * SliceScale;
            float bottompos = Height - slicedtexture.GetSlice(SliceLocation.RightBottom).getHeight() * SliceScale;

            topWidthRows /= SliceScale / 2.0f;
            bottomWidthRows /= SliceScale / 2.0f;
            leftHeigtColums /= SliceScale;

            for (int i = 0; i < topWidthRows - 2; i++)
            {
                renderSingle(SliceLocation.CenterTop, Vector3.Zero() + new Vector3(lefttopsize + i * topWidthInc));
            }
            renderSingle(SliceLocation.CenterTop, Vector3.Zero() + new Vector3(rightpos - topWidthInc));

            for (int i = 0; i < bottomWidthRows - 2; i++)
            {
                renderSingle(SliceLocation.CenterBottom, Vector3.Zero() + new Vector3(lefttopsize + i * bottomWidthInc, bottompos));
            }
            renderSingle(SliceLocation.CenterBottom, Vector3.Zero() + new Vector3(rightpos - bottomWidthInc, bottompos));

            for (int i = 0; i < leftHeigtColums - 1; i++)
            {
                renderSingle(SliceLocation.LeftCenter, Vector3.Zero() + new Vector3(0.0f, lefttopsize + i * leftHeightInc));
            }
            renderSingle(SliceLocation.LeftCenter, Vector3.Zero() + new Vector3(0.0f, bottompos - leftHeightInc));

            for (int i = 0; i < leftHeigtColums - 1; i++)
            {
                renderSingle(SliceLocation.RightCenter, Vector3.Zero() + new Vector3(rightpos, lefttopsize + i * leftHeightInc));
            }
            renderSingle(SliceLocation.RightCenter, Vector3.Zero() + new Vector3(rightpos, bottompos - leftHeightInc));

            for (int i = 0; i < topWidthRows - 1; i++)
            {
                for (int j = 0; j < leftHeigtColums; j++)
                {
                    renderSingle(SliceLocation.CenterCenter, Vector3.Zero() + new Vector3(lefttopsize + i * bottomWidthInc, lefttopsize + j * leftHeightInc));
                }
            }

            renderSingle(SliceLocation.LeftTop, Vector3.Zero());
            renderSingle(SliceLocation.RightTop, Vector3.Zero() + new Vector3(rightpos, 0.0f, 0.0f));
            renderSingle(SliceLocation.LeftBottom, Vector3.Zero() + new Vector3(0.0f, bottompos, 0.0f));
            renderSingle(SliceLocation.RightBottom, Vector3.Zero() + new Vector3(rightpos, bottompos, 0.0f));
        }
    }
}
