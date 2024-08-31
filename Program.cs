using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace T 
  {
public class Program {
        public static void Main(string[] args)
    {
        var gameWindowSettings = new GameWindowSettings
        {
            UpdateFrequency = 120.0
        };
        var nativeWindowSettings = new NativeWindowSettings
        {
            Title = "OpenTK 3D Cube",
            Flags = ContextFlags.ForwardCompatible
        };

        using (var game = new Game(gameWindowSettings, nativeWindowSettings))
        {
            game.Run();
        }
    }
}
  }