using Raylib_cs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using TinyRasterizer;
using static System.Net.Mime.MediaTypeNames;

const int RENDER_WIDTH = 2000;
const int RENDER_HEIGHT = 2000;
const int WINDOW_WIDTH = RENDER_WIDTH;
const int WINDOW_HEIGHT = RENDER_HEIGHT;


FrameBuffer frameBuffer = new FrameBuffer(RENDER_WIDTH, RENDER_HEIGHT);

Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Tiny Rasterizer");

var model = new TinyRasterizer.Model(@"C:\Users\danie\source\repos\TinyRasterizer\resources\african_head.obj");

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

    foreach (var f in model.Faces)
    {
        int x1 = Util.float_to_int((f.Vertex1.X + 1) * RENDER_WIDTH / 2);
        int y1 = Util.float_to_int((f.Vertex1.Y + 1) * RENDER_HEIGHT / 2);

        int x2 = Util.float_to_int((f.Vertex2.X + 1) * RENDER_WIDTH / 2);
        int y2 = Util.float_to_int((f.Vertex2.Y + 1) * RENDER_HEIGHT / 2);

        int x3 = Util.float_to_int((f.Vertex3.X + 1) * RENDER_WIDTH / 2);
        int y3 = Util.float_to_int((f.Vertex3.Y + 1) * RENDER_HEIGHT / 2);

        if (x1 >= RENDER_WIDTH || y1 >= RENDER_HEIGHT || x1 < 2 || y1 < 2) continue;
        if (x2 >= RENDER_WIDTH || y2 >= RENDER_HEIGHT || x2 < 2 || y2 < 2) continue;
        if (x3 >= RENDER_WIDTH || y3 >= RENDER_HEIGHT || x3 < 2 || y3 < 2) continue;

        

        try { 
        frameBuffer.Line(x1, y1, x2, y2, Raylib_cs.Color.Black);
        frameBuffer.Line(x2, y2, x3, y3, Raylib_cs.Color.Black);
        frameBuffer.Line(x3, y3, x1, y1, Raylib_cs.Color.Black);

        //Raylib.DrawLine(x1, y1, x2, y2, Raylib_cs.Color.Red);
        //Raylib.DrawLine(x1, y1, x3, y3, Raylib_cs.Color.Blue);
        //Raylib.DrawLine(x2, y2, x3, y3, Raylib_cs.Color.Yellow);
        }
        catch (Exception e) { }
        
    }


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