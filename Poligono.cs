public class Poligono
{
    public List<Punto> Puntos { get; set; }

    public Poligono()
    {
        Puntos = new List<Punto>();
    }

    public void AgregarPunto(Punto punto)
    {
        Puntos.Add(punto);
    }
}
