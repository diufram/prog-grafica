// ShaderProgram.cs
using OpenTK.Graphics.OpenGL4;

public class ShaderProgram
{

    public ShaderProgram()
    {
    }
    public int CreateShaderProgram()
    {
        var vertexShaderSource = @"
          #version 330 core
layout(location = 0) in vec3 aPosition;  // Posición del vértice (en 3D)
layout(location = 1) in vec4 aColor;     // Color del vértice (RGBA)

out vec4 vertexColor;                    // Color del vértice a pasar al fragment shader

uniform mat4 projection; // Matriz de proyección
uniform mat4 view;       // Matriz de vista
uniform mat4 model;      // Matriz de modelo

void main()
{
    // Transformar la posición del vértice usando las matrices de proyección, vista y modelo
    gl_Position = projection * view * model * vec4(aPosition, 1.0);
    vertexColor = aColor;  // Pasar el color al fragment shader
}


        ";

        var fragmentShaderSource = @"
            #version 330 core
in vec4 vertexColor;  // Color del vértice recibido del vertex shader
out vec4 FragColor;   // Color final del fragmento

void main()
{
    FragColor = vertexColor;  // Asignar el color al fragmento
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
}
