using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nave.Commands;

namespace Nave
{
    static class InputInterpeter
    {

        //Rotação horizontal
        static float leftrightRot = 0f;
        //Rotação vertical
        static float updownRot = 0f;
        //Velocidade da rotação
        const float rotationSpeed = 0.3f;
        //Velocidade do movimento com o rato
        static float moveSpeed = 5f;
        //Estado do rato

        static private MouseState originalMouseState;
        static RasterizerState rasterizerStateSolid;
        static RasterizerState rasterizerStateWireFrame;

        static KeyboardState keyStateAnterior;
        static MouseState mouseStateAnterior;

        static private Command buttonFire;
        static private Command buttonFoward;
        static private Command buttonBack;
        static private Command buttonLeft;
        static private Command buttonRight;
        static private Command buttonUp;
        static private Command buttonDown;

        static public void Initialize(GraphicsDevice graphics)
        {
            //Criar e definir o resterizerState a utilizar para desenhar a geometria
            rasterizerStateSolid = new RasterizerState();
            //Desenha todas as faces, independentemente da orientação
            rasterizerStateSolid.CullMode = CullMode.None;
            rasterizerStateSolid.MultiSampleAntiAlias = true;
            rasterizerStateSolid.FillMode = FillMode.Solid;
            rasterizerStateSolid.SlopeScaleDepthBias = 0.1f;
            graphics.RasterizerState = rasterizerStateSolid;

            rasterizerStateWireFrame = new RasterizerState();
            //Desenha todas as faces, independentemente da orientação
            rasterizerStateWireFrame.CullMode = CullMode.None;
            rasterizerStateWireFrame.MultiSampleAntiAlias = true;
            rasterizerStateWireFrame.FillMode = FillMode.WireFrame;

            buttonFire = new FireWeapon();
            buttonFoward = new MoveFoward();
            buttonBack = new MoveBack();
            buttonLeft = new MoveLeft();
            buttonRight = new MoveRight();
            buttonUp = new MoveUp();
            buttonDown = new MoveDown();
            originalMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Implementa os controlos da camâra
        /// </summary>
        /// <param name="amount">Tempo decorrido desde o ultimo update</param>
        /// <param name="graphics">Instância de graphicsDevice</param>
        static public void ProcessInput(float amount, GraphicsDevice graphics)
        {
            //Movimento do rato
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState != originalMouseState)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                float yDifference = currentMouseState.Y - originalMouseState.Y;
                Camera.SetNewRotations(rotationSpeed * xDifference * amount, rotationSpeed * yDifference * amount);
            }

            Mouse.SetPosition(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);

            if (currentMouseState.ScrollWheelValue > mouseStateAnterior.ScrollWheelValue)
            {
                moveSpeed += (currentMouseState.ScrollWheelValue - mouseStateAnterior.ScrollWheelValue) / 100;
            }
            if (currentMouseState.ScrollWheelValue < mouseStateAnterior.ScrollWheelValue)
            {
                if (moveSpeed > 0.5f) moveSpeed -= (mouseStateAnterior.ScrollWheelValue - currentMouseState.ScrollWheelValue) / 500;
                if (moveSpeed < 0.2f) moveSpeed = 0.2f;
            }

            //Controlos do teclado
            Vector3 moveVector = new Vector3(0, 0, 0);
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
                buttonFoward.Execute(amount, moveSpeed);
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
                buttonBack.Execute(amount, moveSpeed);
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
                buttonRight.Execute(amount, moveSpeed);
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
                buttonLeft.Execute(amount, moveSpeed);
            if (keyState.IsKeyDown(Keys.Q))
                buttonUp.Execute(amount, moveSpeed);
            if (keyState.IsKeyDown(Keys.E))
                buttonDown.Execute(amount, moveSpeed);
            //Tiro
            if (keyState.IsKeyDown(Keys.Space))
                buttonFire.Execute();
            //Change the rendertype
            if (keyState.IsKeyDown(Keys.O) && !keyStateAnterior.IsKeyDown(Keys.O))
            {
                if (graphics.RasterizerState == rasterizerStateSolid)
                    graphics.RasterizerState = rasterizerStateWireFrame;
                else
                    graphics.RasterizerState = rasterizerStateSolid;
            }
            //change type of camera
            if (keyState.IsKeyDown(Keys.C) && !keyStateAnterior.IsKeyDown(Keys.C))
            {
                if (Camera.GetCameraType() == TipoCamera.FPS)
                {
                    Camera.SetCameraType(TipoCamera.Free);
                }
                else
                {
                    Camera.SetCameraType(TipoCamera.FPS);
                }
            }

            if (keyState.IsKeyDown(Keys.Add))
            {
                moveSpeed += 0.5f;
            }
            if (keyState.IsKeyDown(Keys.Subtract))
            {
                if (moveSpeed > 0.5f) moveSpeed -= 0.5f;
            }

            keyStateAnterior = keyState;
            mouseStateAnterior = currentMouseState;
        }

        /// <summary>
        /// Atualiza os parâmetros
        /// </summary>
        /// <param name="gameTime">Instância de gameTime</param>
        /// <param name="graphics">Instância de graphicsDevice</param>
        static public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            ProcessInput(timeDifference, graphics);
        }

    }
}
