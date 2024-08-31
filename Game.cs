using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

public class Game : GameWindow
{
    private Escenario _escenario;

    public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
        _escenario = new Escenario();
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.5f, 0.7f, 1.0f, 1.0f);
        GL.Enable(EnableCap.DepthTest);

        // Crear objetos
        Objeto letraT = new Objeto();
        letraT.AgregarParte(new Parte
        {
            Vertices = new float[]
            {
                // x, y, z,   r, g, b
                -0.5f,  0.4f, 0f,  1.0f, 0.0f, 0.0f, // 0: Frente - Rojo
                 0.5f,  0.4f, 0f, 1.0f, 0.0f, 0.0f, // 1: Frente - Rojo
                 0.5f,  0.3f, 0f, 1.0f, 0.0f, 0.0f, // 2: Frente - Rojo
                -0.5f,  0.3f, 0f, 1.0f, 0.0f, 0.0f, // 3: Frente - Rojo

                -0.15f, -0.4f, 0f, 1.0f, 1.0f, 0.0f, // 4: Atrás - Amarillo
                 0.15f, -0.4f, 0f,  1.0f, 1.0f, 0.0f, // 5: Atrás - Amarillo
                 0.15f,  0.3f, 0f, 1.0f, 1.0f, 0.0f, // 6: Atrás - Amarillo
                -0.15f,  0.3f, 0f, 1.0f, 1.0f, 0.0f, // 7: Atrás - Amarillo

                // x, y, z,   r, g, b
                 -0.5f,  0.4f, -0.1f,  1.0f, 0.0f, 0.0f, // 0: Frente - Rojo
                 0.5f,  0.4f, -0.1f, 1.0f, 0.0f, 0.0f, // 1: Frente - Rojo
                 0.5f,  0.3f, -0.1f, 1.0f, 0.0f, 0.0f, // 2: Frente - Rojo
                -0.5f,  0.3f, -0.1f, 1.0f, 0.0f, 0.0f, // 3: Frente - Rojo

                -0.15f, -0.4f, -0.1f,  1.0f, 1.0f, 0.0f, // 4: Atrás - Amarillo
                0.15f, -0.4f, -0.1f, 1.0f, 1.0f, 0.0f, // 5: Atrás - Amarillo
                 0.15f,  0.3f, -0.1f, 1.0f, 1.0f, 0.0f, // 6: Atrás - Amarillo
                -0.15f,  0.3f, -0.1f ,1.0f, 1.0f, 0.0f, // 7: Atrás - Amarillo

            },
            Indices = new uint[]
            {
   
                0, 1, 2, 2, 3, 0, // Frente
                8, 9, 10, 10, 11, 8, // Atrás
                0, 1, 9, 9, 8, 0, // Lado superior
                1, 2, 10, 10, 9, 1, // Lado derecho
                2, 3, 11, 11, 10, 2, // Lado inferior
                3, 0, 8, 8, 11, 3, // Lado izquierdo

                // Barra vertical (middle)
                4, 5, 6, 6, 7, 4, // Frente
                12, 13, 14, 14, 15, 12, // Atrás
                4, 5, 13, 13, 12, 4, // Lado izquierdo
                5, 6, 14, 14, 13, 5, // Lado derecho
                6, 7, 15, 15, 14, 6, // Lado superior
                7, 4, 12, 12, 15, 7  // Lado inferior
            }
        });
        letraT.EstablecerPosicion(new Vector3(1f, 1f, 1.5f));

        _escenario.AgregarObjeto("LetraT", letraT);

        _escenario.Inicializar();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //var modelMatrix1 = Matrix4.CreateTranslation(new Vector3(-1f, 0f, -3f)); // Posición del primer cubo
        //var modelMatrix2 = Matrix4.CreateTranslation(new Vector3(1f, 0f, -3f)); // Posición del segundo cubo

        _escenario.Dibujar(Matrix4.LookAt(new Vector3(2.0f, 2.0f, 2.0f), Vector3.Zero, Vector3.UnitY),
                            Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f));

        SwapBuffers();
    }
}
