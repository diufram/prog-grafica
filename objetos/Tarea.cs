using OpenTK.Mathematics;

public class LetraT : GameObject
{

    private static readonly float[] _vertices =
{
    // Barra horizontal (top)
    -0.5f,  0.4f, 0f, // Vértice 0
     0.5f,  0.4f, 0f, // Vértice 1
     0.5f,  0.3f, 0f, // Vértice 2
    -0.5f,  0.3f, 0f, // Vértice 3

    // Barra vertical (middle)
    -0.15f, -0.4f, 0f, // Vértice 4
     0.15f, -0.4f, 0f, // Vértice 5
     0.15f,  0.3f, 0f, // Vértice 6
    -0.15f,  0.3f, 0f, // Vértice 7

    // Barra horizontal (top) - Parte trasera
    -0.5f,  0.4f, -0.1f, // Vértice 8
     0.5f,  0.4f, -0.1f, // Vértice 9
     0.5f,  0.3f, -0.1f, // Vértice 10
    -0.5f,  0.3f, -0.1f, // Vértice 11

    // Barra vertical (middle) - Parte trasera
    -0.15f, -0.4f, -0.1f, // Vértice 12
     0.15f, -0.4f, -0.1f, // Vértice 13
     0.15f,  0.3f, -0.1f, // Vértice 14
    -0.15f,  0.3f, -0.1f  // Vértice 15
};

    private static readonly uint[] _indices =
    {
    // Barra horizontal (top)
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
};



    private static readonly float[] _colors =
    {
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo
  1.0f, 0.0f, 0.0f, 1.0f, // Rojo

        };

    public LetraT(Vector3 position) : base(_vertices, _indices, _colors, position) { }

}