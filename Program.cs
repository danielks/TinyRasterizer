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

const int RENDER_WIDTH = 1000;
const int RENDER_HEIGHT = 1000;
const int WINDOW_WIDTH = RENDER_WIDTH;
const int WINDOW_HEIGHT = RENDER_HEIGHT;

Vector3 light_direction = new Vector3(0, 0, -1f);

FrameBuffer frameBuffer = new FrameBuffer(RENDER_WIDTH, RENDER_HEIGHT);

Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "Tiny Rasterizer");

//var model = new TinyRasterizer.Model(@"C:\Users\danie\source\repos\TinyRasterizer\resources\african_head.obj");
var model = new TinyRasterizer.Model(@"C:\Users\danie\source\repos\TinyRasterizer\resources\Intergalactic_Spaceship-(Wavefront).obj");


long frameCount = 0;

Stopwatch sw = new Stopwatch();
double frameTime = sw.Elapsed.TotalMilliseconds;

Random rnd = new Random();

while (!Raylib.WindowShouldClose())
{
    sw.Restart();

    //light_direction.Z += 0.0001f;
    //light_direction.X += 0.0001f;


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
    Raylib.ClearBackground(Raylib_cs.Color.Black);

    

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

    int i = 0;

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
            //draw wireframe
        //frameBuffer.Line(x1, y1, x2, y2, Raylib_cs.Color.Red);
        //frameBuffer.Line(x2, y2, x3, y3, Raylib_cs.Color.Red);
        //frameBuffer.Line(x3, y3, x1, y1, Raylib_cs.Color.Red);

        var screenCoordsTriangle = new Vector3[3]
        {
            new Vector3(x1, y1, 0),
            new Vector3(x2, y2, 0),
            new Vector3(x3, y3, 0),
        };

            var worldCoordsTriangle = (new Vector3[3]
                {
                    f.Vertex1,
                    f.Vertex2,
                    f.Vertex3
                });

            worldCoordsTriangle = Util.OrderTriangleCounterClockwise(worldCoordsTriangle);



            var normal = Util.CalculateTriangleNormal(worldCoordsTriangle);
            //var intensity = (normal * light_direction).LengthSquared();
            if (i++ < 5)
            {
                Console.WriteLine("{0}, {1}, {2}", worldCoordsTriangle[0].X, worldCoordsTriangle[0].Y, worldCoordsTriangle[0].Z);
                Console.WriteLine("{0}, {1}, {2}", worldCoordsTriangle[1].X, worldCoordsTriangle[1].Y, worldCoordsTriangle[1].Z);
                Console.WriteLine("{0}, {1}, {2}", worldCoordsTriangle[2].X, worldCoordsTriangle[2].Y, worldCoordsTriangle[2].Z);

                Console.WriteLine("{0}, {1}, {2}", normal.X, normal.Y, normal.Z);
            }

            
            var intensity = Vector3.Dot(normal, light_direction); //intensity of light
            



            if (i++ < 5)
            {
                Console.WriteLine("intensity: {0}", intensity);
                
                
            }

            if (intensity > 0) { 
                int c = Util.float_to_int(intensity * 255);

                screenCoordsTriangle = Util.OrderTriangleCounterClockwise(screenCoordsTriangle);


                
                frameBuffer.FillTriangle(screenCoordsTriangle, new Raylib_cs.Color(c, c, c, 255));
                /*Raylib.DrawTriangle(
                    new Vector2(screenCoordsTriangle[0].X, screenCoordsTriangle[0].Y),
                    new Vector2(screenCoordsTriangle[1].X, screenCoordsTriangle[1].Y),
                    new Vector2(screenCoordsTriangle[2].X, screenCoordsTriangle[2].Y),
                    new Raylib_cs.Color(c, c, c, 255)
                    );*/

                //Raylib.DrawLine(x1, y1, x2, y2, Raylib_cs.Color.Red);
                //Raylib.DrawLine(x1, y1, x3, y3, Raylib_cs.Color.Blue);
                //Raylib.DrawLine(x2, y2, x3, y3, Raylib_cs.Color.Yellow);
            }



           
        }
        catch (Exception e) { }

        //break;
        
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