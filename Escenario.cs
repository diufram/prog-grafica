using OpenTK.Mathematics;
using System.Collections.Generic;

public class Escenario
{
    private readonly Dictionary<string, Objeto> _objetos;
    
    public Escenario()
    {
        _objetos = new Dictionary<string, Objeto>();
    }

    public void AgregarObjeto(string nombre, Objeto objeto)
    {
        _objetos[nombre] = objeto;
    }

    public void Inicializar()
    {
        foreach (var objeto in _objetos.Values)
        {
            objeto.Inicializar();
        }
    }

    public void Dibujar(Matrix4 viewMatrix, Matrix4 projectionMatrix)
    {
        foreach (var objeto in _objetos.Values)
        {
            // Define la matriz de modelo para cada objeto aquí, por ejemplo:
            // Puedes ajustar la matriz de modelo para posicionar y escalar el objeto.
           // var modelMatrix = Matrix4.CreateTranslation(new Vector3(0f, 0f, 0f)); // Ajusta la posición aquí

            objeto.Dibujar( viewMatrix, projectionMatrix);
        }
    }
}
