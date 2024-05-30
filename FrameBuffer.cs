using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyRasterizer;

namespace TinyRaycaster
{
    internal class FrameBuffer
    {
        private byte[] buffer;
        private int renderWidth;
        private int renderHeight;

        public byte[] Buffer { get { return buffer; } }


        public FrameBuffer(int _renderWidth, int _renderHeight)
        {
            renderWidth = _renderWidth;
            renderHeight = _renderHeight;

            buffer = new byte[_renderWidth * _renderHeight * 4]; //4 bytes per pixel
        }

        public void Clear()
        {
            Array.Clear(buffer, 0, buffer.Length);
        }

        public void SetPixel(int x, int y, Raylib_cs.Color pixel_color)
        {
            //we want the origin to be at the bottom left corner of the screen, which is why I flip the y value.
            int idx = (Math.Abs(y - renderHeight - 1) * renderWidth + x) * 4;

            buffer[idx] = pixel_color.R;
            buffer[idx + 1] = pixel_color.G;
            buffer[idx + 2] = pixel_color.B;
            buffer[idx + 3] = 255;
        }

        public void DrawRectangle(int x, int y, int w, int h, Raylib_cs.Color color)
        {

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    int cx = x + i;
                    int cy = y + j;

                    if (cx >= renderWidth || cy >= renderHeight) continue;

                    SetPixel(cx, cy, color);
                }
            }
        }

        public void Line(int x0, int y0, int x1, int y1, Raylib_cs.Color color)
        {
            //the author of the article made a number of optimizations to this function, which I did no implement.

            bool steep = false;

            if (Math.Abs(x0 - x1) < Math.Abs(y0 - y1)) // if the line is steep, we transpose the image 
            {
                (x0, y0) = (y0, x0);
                (x1, y1) = (y1, x1);

                
                steep = true;
            }

            if (x0 > x1)
            { // make it left−to−right 
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }

            for (int x = x0; x <= x1; x++)
            {
                float t = (x - x0) / (float)(x1 - x0);

                int y =  Util.float_to_int(y0 * (1.0f - t) + y1 * t);

                if (steep)
                {
                    SetPixel(y, x, color); // if transposed, de−transpose 
                }
                else
                {
                    SetPixel(x, y, color);
                }
            }
        }
    }
}
