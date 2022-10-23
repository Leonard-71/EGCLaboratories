using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Lab2
{
    class Program : GameWindow
    {

        private const int XYZ_SIZE = 75;

        public Program() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

        }


        protected override void OnLoad(EventArgs e)
        {

            GL.ClearColor(Color.Blue);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, -2, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 22);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(12, 12, 12, 0, 0, 0, 4, 100, 0);
          
            GL.MatrixMode(MatrixMode.Modelview);

            GL.LoadMatrix(ref lookat);


        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (mouse.Y > 700) ///In momentul in care cursor se afla la o distanta mai mare de 700
            {                   ///corpul se va roti
                GL.Rotate(5, 0, 1, 0);

            }
            if (keyboard[Key.D]) ///Cand se va apasa pe tasta S viewport-ul se va modifica
            {                    ///corpul fiind afisat mai jos fata de axa OX
                GL.Viewport(0, -100, Width, Height);
            }

            if (keyboard[Key.A]) ///Cand se va apasa pe tasta W viewport-ul se va modifica
            {                    ///corpul fiind afisat mai sus fata de axa OX
                GL.Viewport(0, 100, Width, Height);
            }
            if (mouse[MouseButton.Right])///Prin efectuare de click dreapta viewport-ul va reveni
            {                            /// la forma initiala
                GL.Viewport(0, -2, Width, Height);
            }
            if (mouse[MouseButton.Left])
            {
                GL.Scale(11, 2,0); ///Efectuam o scalare
            }
       


        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            DrawCube();

            SwapBuffers();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.GreenYellow);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.DarkOliveGreen);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Black);

            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.IndianRed);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.BlanchedAlmond);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.End();
        }

        static void Main(string[] args)
        {

            using (Program example = new Program())
            {
                example.Run(30.0, 0.0);
            }
        }
    }

}
