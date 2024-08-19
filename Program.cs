using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Game : GameWindow
{
    private int _shaderProgram;

    private Matrix4 _projectionMatrix;
    private Matrix4 _viewMatrix;
    private GameObject _object1;
    private GameObject _cube2;


    public Game() : base(GameWindowSettings.Default, new NativeWindowSettings
    {
        ClientSize = new Vector2i(800, 600),
        Title = "Prog Grafica"
    })
    {
        VSync = VSyncMode.On;


        Vector3 position = new Vector3(1.0f, 1.0f, 0f);
        Vector3 position2 = new Vector3(2.0f, 2.0f, 0f);
        _object1 = new LetraT(position);

        _cube2 = new Triangulo(position2);

    }

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(Color4.CornflowerBlue);
        GL.Enable(EnableCap.DepthTest);

        _shaderProgram = CreateShaderProgram();

        // Configurar las matrices de proyección y vista en el shader
        _projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);
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

        // Actualizar matrices de transformación de los objetos
        float deltaTime = (float)e.Time;
        if (keyboardState.IsKeyDown(Keys.Left))
            _object1.ModelMatrix *= Matrix4.CreateRotationY(-0.01f);
        if (keyboardState.IsKeyDown(Keys.Right))
            _object1.ModelMatrix *= Matrix4.CreateRotationY(0.01f);
        if (keyboardState.IsKeyDown(Keys.Up))
            _object1.ModelMatrix *= Matrix4.CreateRotationX(0.01f);
        if (keyboardState.IsKeyDown(Keys.Down))
            _object1.ModelMatrix *= Matrix4.CreateRotationX(-0.01f);


        // Variables para la velocidad de movimiento de la cámara
        float cameraSpeed = 5.0f * deltaTime;
        if (keyboardState.IsKeyDown(Keys.Q))
            _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -cameraSpeed));
        if (keyboardState.IsKeyDown(Keys.E))
            _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, cameraSpeed));
        if (keyboardState.IsKeyDown(Keys.A))
            _viewMatrix *= Matrix4.CreateTranslation(new Vector3(-cameraSpeed, 0.0f, 0.0f));
        if (keyboardState.IsKeyDown(Keys.D))
            _viewMatrix *= Matrix4.CreateTranslation(new Vector3(cameraSpeed, 0.0f, 0.0f));
        if (keyboardState.IsKeyDown(Keys.W))
            _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, cameraSpeed, 0.0f));
        if (keyboardState.IsKeyDown(Keys.S))
            _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, -cameraSpeed, 0.0f));

    // Actualizar matrices en el shader
    GL.UseProgram(_shaderProgram);
    GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref _viewMatrix);


        // Aplicar transformaciones adicionales si es necesario
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


        // Aplicar la matriz de proyección y vista al shader
        GL.UseProgram(_shaderProgram);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref _projectionMatrix);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref _viewMatrix);


        // Dibujar los objetos
        _object1.Draw(_shaderProgram);

       // _cube2.Draw(_shaderProgram);

        SwapBuffers();
    }

    private int CreateShaderProgram()
    {
        var vertexShaderSource = @"
            #version 330 core
            layout(location = 0) in vec3 aPosition;
            layout(location = 1) in vec3 aColor;

            out vec3 vertexColor;

            uniform mat4 projection;
            uniform mat4 view;
            uniform mat4 model;

            void main()
            {
                gl_Position = projection * view * model * vec4(aPosition, 1.0);
                vertexColor = aColor;
            }
        ";

        var fragmentShaderSource = @"
            #version 330 core
            in vec3 vertexColor;
            out vec4 FragColor;

            void main()
            {
                FragColor = vec4(vertexColor, 1.0);
            }
        ";

        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);
        CheckShaderCompile(vertexShader);

        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);
        CheckShaderCompile(fragmentShader);

        var shaderProgram = GL.CreateProgram();
        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);
        GL.LinkProgram(shaderProgram);
        CheckProgramLink(shaderProgram);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        return shaderProgram;
    }

    private void CheckShaderCompile(int shader)
    {
        GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
        if (status == 0)
        {
            var infoLog = GL.GetShaderInfoLog(shader);
            throw new Exception($"Error compiling shader: {infoLog}");
        }
    }

    private void CheckProgramLink(int program)
    {
        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int status);
        if (status == 0)
        {
            var infoLog = GL.GetProgramInfoLog(program);
            throw new Exception($"Error linking program: {infoLog}");
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);

        // Actualizar la matriz de proyección ortográfica
        float aspectRatio = Size.X / (float)Size.Y;
        _projectionMatrix = Matrix4.CreateOrthographicOffCenter(
            -aspectRatio, aspectRatio, // Límite izquierdo y derecho
            -1.0f, 1.0f, // Límite inferior y superior
            0.1f, 100.0f // Planos de recorte cerca y lejos
        );

        GL.UseProgram(_shaderProgram);
        GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref _projectionMatrix);
    }


    [STAThread]
    static void Main()
    {
        using (var game = new Game())
        {
            game.Run();
        }
    }
}