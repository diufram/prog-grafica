using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


public class Game : GameWindow
{

    private ShaderProgram _shader;

    private Utils _u;
    private int _shaderProgram;
    private Matrix4 _projectionMatrix;
    private Matrix4 _viewMatrix;
    private GameObject _object1;



    public Game(Vector2i monitorSize) : base(GameWindowSettings.Default, new NativeWindowSettings
    {
        ClientSize = monitorSize,
        // Title = "Prog Grafica"
    })
    {
        _shader = new ShaderProgram();
        _u = new Utils();
        VSync = VSyncMode.On;
        _object1 = new LetraT(0, 0, 0);

    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(Color4.CornflowerBlue);
        GL.Enable(EnableCap.DepthTest);
        _shaderProgram = _shader.CreateShaderProgram();
        UpdateProjectionMatrix();
        _viewMatrix = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);
        GL.UseProgram(_shaderProgram);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref _projectionMatrix);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref _viewMatrix);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        var keyboardState = KeyboardState;
        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }
        _u.Move(keyboardState, _object1);
        // Actualizar matrices en el shader
        GL.UseProgram(_shaderProgram);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref _viewMatrix);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.UseProgram(_shaderProgram);
        _object1.Draw(_shaderProgram);
        SwapBuffers();
    }



    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        UpdateProjectionMatrix();
        GL.UseProgram(_shaderProgram);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref _projectionMatrix);
    }

    private void UpdateProjectionMatrix()
    {
        _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
            MathHelper.DegreesToRadians(45.0f),
           Size.X / (float)Size.Y,           
            0.1f,                            
            100.0f                           
        );
    }


    [STAThread]
    static void Main()
    {
        Vector2i monitorSize = new Vector2i(800, 600);
        using (var game = new Game(monitorSize))
        {
            game.Run();
        }
    }
}
