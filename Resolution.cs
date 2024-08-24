using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
namespace Resolution
{
    public class ScreenResolution
    {
        public  Vector2i GetPrimaryMonitorResolution()
        {
            unsafe
            {
                // Inicializa GLFW si aún no lo está
                GLFW.Init();

                // Obtiene el monitor principal
                var monitor = GLFW.GetPrimaryMonitor();


                // Obtiene la resolución del monitor
                GLFW.GetMonitorWorkarea(monitor, out var xPos, out var yPos, out var width, out var height);

                // Finaliza GLFW
                GLFW.Terminate();

                return new Vector2i(width, height);
            }
        }
    }
}
