using Raylib_cs;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using TinyRaycaster;
using static System.Net.Mime.MediaTypeNames;

const int RENDER_WIDTH = 2048;
const int RENDER_HEIGHT = 1024;
const int WINDOW_WIDTH = RENDER_WIDTH;
const int WINDOW_HEIGHT = RENDER_HEIGHT;


FrameBuffer frameBuffer = new FrameBuffer(RENDER_WIDTH, RENDER_HEIGHT);

Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Tiny Rasterizer");

long frameCount = 0;

Stopwatch sw = new Stopwatch();
double frameTime = sw.Elapsed.TotalMilliseconds;


while (!Raylib.WindowShouldClose())
{
    sw.Restart();


    if (Raylib.IsKeyDown(KeyboardKey.Up))
    {
        
    }

    if (Raylib.IsKeyDown(KeyboardKey.Down))
    {
        
    }
    
    if (Raylib.IsKeyDown(KeyboardKey.Right))
    {
        
    }
    
    if (Raylib.IsKeyDown(KeyboardKey.Left))
    {

    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Raylib_cs.Color.White);

    render();

    Raylib.EndDrawing();

    sw.Stop();
    frameTime = sw.Elapsed.TotalMilliseconds;

    if (frameCount % 10 == 0) //a cada 50 frames mostra o ultimo frametime
    {
        Raylib.SetWindowTitle(string.Format("{0}ms. FPS: {1}", frameTime, 1000.0f / frameTime));
    }



    frameCount++;
}

Raylib.CloseWindow();

void render()
{
    frameBuffer.Clear();

    

    

    Raylib_cs.Image i2 = new Raylib_cs.Image
    {
        Format = PixelFormat.UncompressedR8G8B8A8,
        Width = RENDER_WIDTH,
        Height = RENDER_HEIGHT,
        Mipmaps = 1
    };

    Texture2D t2 = Raylib.LoadTextureFromImage(i2);

    unsafe
    {
        fixed (byte* bPtr = &frameBuffer.Buffer[0])
        {
            Raylib.UpdateTexture(t2, bPtr);
            Raylib.DrawTexture(t2, 0, 0, Raylib_cs.Color.White);
        }
    }
}