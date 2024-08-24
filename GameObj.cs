using Converter.converter;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class GameObject
{
    public int VertexArray { get; private set; }
    public int VertexBuffer { get; private set; }
    public int ColorBuffer { get; private set; }
    public int IndexBuffer { get; private set; }
    public Vector3 Position { get; private set; }



    private float[] _vertices;
    private uint[] _indices;
    private float[] _colors;
    public Matrix4 _modelMatrix = Matrix4.Identity;
    private Vector3 _posicion;

    private int _x;
    private int _y;
    private int _z;



    public float[] Vertices
    {
        get => _vertices;
        set => _vertices = value;
    }
    public uint[] Indices
    {
        get => _indices;
        set => _indices = value;
    }
    public Matrix4 ModelMatrix
    {
        get => _modelMatrix;
        set => _modelMatrix = value;
    }

    public GameObject(float[] vertices, uint[] indices, float[] colors, int x, int y, int z)
    {
        _vertices = vertices;
        _indices = indices;
        _colors = colors;
        var converter = new CoordinateConverter(800, 600);
        _x = x;
        _y = y;
        _z = z;
        Vector3 posicion = new Vector3(x, y, z);
        _posicion = posicion;
        _modelMatrix = Matrix4.CreateTranslation(converter.AbsoluteToRelative(posicion));
        InitializeBuffers();
    }

    public void SetPosition(Vector3 position)
    {
        _modelMatrix = Matrix4.CreateTranslation(position);
    }
    private void InitializeBuffers()
    {
        VertexArray = GL.GenVertexArray();
        VertexBuffer = GL.GenBuffer();
        ColorBuffer = GL.GenBuffer();
        IndexBuffer = GL.GenBuffer();

        GL.BindVertexArray(VertexArray);

        // Configurar el buffer de vértices
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        /*         // Configurar el buffer de colores
                GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBuffer);
                GL.BufferData(BufferTarget.ArrayBuffer, _colors.Length * sizeof(float), _colors, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(1); */

        // Configurar el buffer de colores
        GL.BindBuffer(BufferTarget.ArrayBuffer, ColorBuffer);
        GL.BufferData(BufferTarget.ArrayBuffer, _colors.Length * sizeof(float), _colors, BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0); // Asegúrate de usar 4 para RGBA
        GL.EnableVertexAttribArray(1);

        // Configurar el buffer de índices
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void Draw(int shaderProgram)
    {
        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(VertexArray);

        // Obtener la ubicación del uniform 'model'
        int modelLocation = GL.GetUniformLocation(shaderProgram, "model");

        // Enviar la matriz al shader. Usar un campo privado en lugar de una propiedad.
        GL.UniformMatrix4(modelLocation, false, ref _modelMatrix);

        // Dibujar los elementos
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}