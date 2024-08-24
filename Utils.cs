using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
public class Utils
{

    public Utils()
    {

    }

    public void Move(KeyboardState keyboardState, GameObject o)
    {
        // Actualizar matrices de transformación de los objetos

        if (keyboardState.IsKeyDown(Keys.Left))
            o.ModelMatrix *= Matrix4.CreateRotationY(-0.01f);
        if (keyboardState.IsKeyDown(Keys.Right))
            o.ModelMatrix *= Matrix4.CreateRotationY(0.01f);
        if (keyboardState.IsKeyDown(Keys.Up))
            o.ModelMatrix *= Matrix4.CreateRotationX(0.01f);
        if (keyboardState.IsKeyDown(Keys.Down))
            o.ModelMatrix *= Matrix4.CreateRotationX(-0.01f);

        // Variables para la velocidad de movimiento de la cámara
        /*         float deltaTime = (float)e.Time;
                float cameraSpeed = 5.0f * deltaTime;
                if (keyboardState.IsKeyDown(Keys.Q))
                    _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -cameraSpeed));
                if (keyboardState.IsKeyDown(Keys.E))
                    _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, cameraSpeed));
                if (keyboardState.IsKeyDown(Keys.A))
                    _viewMatrix *= Matrix4.CreateTranslation(new Vector3(-cameraSpeed, 0.0f, 0.0f));
                if (keyboardState.IsKeyDown(Keys.D))
                    _viewMatrix *= Matrix4.CreateTranslation(new Vector3(cameraSpeed, 0.0f, 0.0f));
                if (keyboardState.IsKeyDown(Keys.W))
                    _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, cameraSpeed, 0.0f));
                if (keyboardState.IsKeyDown(Keys.S))
                    _viewMatrix *= Matrix4.CreateTranslation(new Vector3(0.0f, -cameraSpeed, 0.0f));
         */
    }
}