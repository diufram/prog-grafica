using OpenTK.Mathematics;

public class Triangulo : GameObject
{

    private static readonly float[] _vertices =
{
    0.0f,  0.5f, 0.0f, // Vértice 1 (superior)
   -0.5f, -0.5f, 0.0f, // Vértice 2 (inferior izquierdo)
    0.5f, -0.5f, 0.0f  // Vértice 3 (inferior derecho)
};

    private static readonly uint[] _indices =
    {
    0, 1, 2 // Único triángulo
};



    private static readonly float[] _colors =
    {
    1.0f, 0.0f, 0.0f, // Rojo
    0.0f, 1.0f, 0.0f, // Verde
    0.0f, 0.0f, 1.0f  // Azul
        };

    public Triangulo(Vector3 position) : base(_vertices, _indices, _colors, position) { }

}