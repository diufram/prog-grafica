using OpenTK.Mathematics;

namespace Converter.converter
{
    public class CoordinateConverter
    {
        private readonly int _windowWidth;
        private readonly int _windowHeight;

        public CoordinateConverter(int windowWidth, int windowHeight)
        {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
        }

        // Convierte coordenadas relativas a absolutas en 2D (para la pantalla)
        public Vector3 RelativeToAbsolute(Vector3 relativePosition)
        {
            float x = relativePosition.X * _windowWidth;
            float y = relativePosition.Y * _windowHeight;
            // Mantener la componente Z igual
            float z = relativePosition.Z;
            return new Vector3(x, y, z);
        }

        // Convierte coordenadas absolutas a relativas en 2D (para la pantalla)
        public Vector3 AbsoluteToRelative(Vector3 absolutePosition)
        {
            float x = absolutePosition.X / _windowWidth;
            float y = absolutePosition.Y / _windowHeight;
            // Mantener la componente Z igual
            float z = absolutePosition.Z;
            return new Vector3(x, y, z);
        }
    }
}
