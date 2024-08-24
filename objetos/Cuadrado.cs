using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
public class Cuadrado : GameObject
{

    private static readonly float[] _vertices =
{
    // Vértices del cubo (8 vértices)
    -0.5f, -0.5f, -0.1f, // Vértice 1
    0.5f, -0.5f, -0.1f,
    0.5f, 0.5f, -0.1f,
    -0.5f, 0.5f, -0.1f,

    -0.5f, -0.5f, 0.5f, // Vértice 5
    0.5f, -0.5f, 0.5f,
    0.5f, 0.5f, 0.5f,
    -0.5f, 0.5f, 0.5f
};

    private static readonly uint[] _indices =
    {
 0, 1, 2, 2, 3, 0, // Cara frontal
    4, 5, 6, 6, 7, 4, // Cara trasera
    0, 1, 5, 5, 4, 0, // Cara inferior
    2, 3, 7, 7, 6, 2, // Cara superior
    0, 3, 7, 7, 4, 0, // Cara izquierda
    1, 2, 6, 6, 5, 1  // Cara derecha
};



    private static readonly float[] _colors =
    {
    1.0f, 0.0f, 0.0f, // Rojo
    0.0f, 1.0f, 0.0f, // Verde
    0.0f, 0.0f, 1.0f, // Azul
    1.0f, 1.0f, 0.0f, // Amarillo

    1.0f, 0.0f, 1.0f, // Magenta
    0.0f, 1.0f, 1.0f, // Cyan
    0.5f, 0.5f, 0.5f, // Gris
    1.0f, 1.0f, 1.0f  // Blanco
        };

    public Cuadrado(int x, int y,int z) : base(_vertices,_indices ,_colors,x,y,z) { }

}