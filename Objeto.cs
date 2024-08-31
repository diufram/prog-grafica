using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

public class Objeto
{
    private readonly List<Parte> _partes;
    private int _vertexArrayObject;
    private int _vertexBufferObject;
    private int _indexBufferObject;
    private Matrix4 _modelMatrix;
    private int _shaderProgram;
    private int _modelMatrixLocation;
    private int _viewMatrixLocation;
    private int _projectionMatrixLocation;

    public Objeto()
    {
        _partes = new List<Parte>();
        _modelMatrix = Matrix4.Identity;
        InicializarShader();
    }

    public void EstablecerPosicion(Vector3 posicion)
    {
        _modelMatrix = Matrix4.CreateTranslation(posicion);
    }



    private void InicializarShader()
    {
        // Vertex Shader
        string vertexShaderCode = @"
        #version 330 core
        layout(location = 0) in vec3 aPosition;
        layout(location = 1) in vec3 aColor;

        uniform mat4 model;
        uniform mat4 view;
        uniform mat4 projection;

        out vec3 vertexColor;

        void main(void)
        {
            gl_Position = projection * view * model * vec4(aPosition, 1.0);
            vertexColor = aColor;
        }";
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderCode);
        GL.CompileShader(vertexShader);
        CheckShaderCompilation(vertexShader, "Vertex");

        // Fragment Shader
        string fragmentShaderCode = @"
        #version 330 core
        in vec3 vertexColor;
        out vec4 fragColor;

        void main(void)
        {
            fragColor = vec4(vertexColor, 1.0);
        }";
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderCode);
        GL.CompileShader(fragmentShader);
        CheckShaderCompilation(fragmentShader, "Fragment");

        // Shader Program
        _shaderProgram = GL.CreateProgram();
        GL.AttachShader(_shaderProgram, vertexShader);
        GL.AttachShader(_shaderProgram, fragmentShader);
        GL.LinkProgram(_shaderProgram);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
    }

    public void AgregarParte(Parte parte)
    {
        _partes.Add(parte);
        parte.Inicializar();
    }

    public void Inicializar()
    {
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);

        foreach (var parte in _partes)
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, parte.Vertices.Length * sizeof(float), parte.Vertices, BufferUsageHint.StaticDraw);

            _indexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, parte.Indices.Length * sizeof(uint), parte.Indices, BufferUsageHint.StaticDraw);
        }

        GL.UseProgram(_shaderProgram);
        _modelMatrixLocation = GL.GetUniformLocation(_shaderProgram, "model");
        _viewMatrixLocation = GL.GetUniformLocation(_shaderProgram, "view");
        _projectionMatrixLocation = GL.GetUniformLocation(_shaderProgram, "projection");
    }

    public void Dibujar( Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {

        GL.UseProgram(_shaderProgram);
        GL.BindVertexArray(_vertexArrayObject);

        GL.UniformMatrix4(_modelMatrixLocation, false, ref _modelMatrix);
        GL.UniformMatrix4(_viewMatrixLocation, false, ref viewMatrix);
        GL.UniformMatrix4(_projectionMatrixLocation, false, ref projectionMatrix);

        foreach (var parte in _partes)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.DrawElements(PrimitiveType.Triangles, parte.Indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_vertexBufferObject);
        GL.DeleteBuffer(_indexBufferObject);
        GL.DeleteVertexArray(_vertexArrayObject);
        GL.DeleteProgram(_shaderProgram);
    }

    private void CheckShaderCompilation(int shader, string shaderType)
    {
        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == (int)All.False)
        {
            string infoLog = GL.GetShaderInfoLog(shader);
            Console.WriteLine($"Error compiling {shaderType} shader: {infoLog}");
        }
    }
}
