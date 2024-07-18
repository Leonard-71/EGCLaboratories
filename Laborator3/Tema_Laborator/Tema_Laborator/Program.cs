using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Tema_Laborator
{
    class Program : GameWindow
    {

        private const int XYZ_SIZE = 75;
        private Color obiect = Color.YellowGreen;
        private int n1 = 12;
        private Matrix4 lookat = Matrix4.LookAt(0, 12, 14, 0, 0, 0, 0, 1, 0);
        private Random r;
        public Program() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            Console.WriteLine("OpenGl versiunea: " + GL.GetString(StringName.Version));
            Title = "OpenGl versiunea: " + GL.GetString(StringName.Version) + " (mod imediat)";

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Blue);
            GL.Enable(EnableCap.DepthTest);

        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 2, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);


        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            if (keyboard[Key.C])
            {
                obiect = Randomizer();/// schimba random culoarea obiectului la apasarea tastei C
            }
            if (keyboard[Key.Escape])
            {
                Exit();
            }
            if (keyboard[Key.I])
            {
                obiect = Color.YellowGreen;///Apasand I obiectul primeste culoarea initiala 
            }
            if (mouse[MouseButton.Left])
            {
                lookat = Matrix4.LookAt(n1, 12, 14, 0, 0, 0, 0, 1, 0); /// click stanga= modifica unghiul prin scaderea valorii corespunzatoare pe axa OX
                n1--;
                OnResize(e);
            }
            if (mouse[MouseButton.Right])
            {
                lookat = Matrix4.LookAt(n1, 13, 14, 0, 0, 0, 0, 1, 0);///click dreapta= modifica unghiul prin cresterea valorii corespunzatoare pe axa OX
                OnResize(e);
                n1++;

            }
            if (keyboard[Key.Z])
            {
                lookat = Matrix4.LookAt(0, 12, 14, 0, 0, 0, 0, 1, 0);////Apasand tasta Z  ne intoarcem la valoriile initiale
                OnResize(e);
            }


        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            DrawTriangle(obiect);
            SwapBuffers();
        }



        private void DrawTriangle(Color obiect)
        {

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(obiect);
            GL.Vertex3(-7.2f, -7.2f, 0);
            GL.Vertex3(0.0f, 7.2f, 0);
            GL.Vertex3(7.2f, -7.2f, 0);
            GL.End();
        }

        public Color Randomizer()
        {
            r = new Random();
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);
            Color col = Color.FromArgb(genR, genG, genB);

            return col;
        }


        [STAThread]
        static void Main(string[] args)
        {

            using (Program example = new Program())
            {
                example.Run(20.0, 0.0);
            }
        }
    }

}
