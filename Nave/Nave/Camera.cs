using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nave.Commands;

/********* Código fornecido pelo professor *********/
namespace Nave
{

        enum TipoCamera
        {
            FPS,
            Free,
            thirdPerson
        }

        static class Camera
        {

            //Matrizes World, View e Projection
            static public Matrix View, Projection;

            //Posição da camara
            static private Vector3 position;

            //Rotação horizontal
            static float leftrightRot = 0f;
            //Rotação vertical
            static float updownRot = 0f;
            //BoundingFrustum da camâra
            static public BoundingFrustum frustum;
            //Tamanho do "mundo"
            public static int worldSize = 1000;
            //Near e far plane
            static public float nearPlane = 0.1f;
            static public float farPlane = worldSize;

            static RasterizerState rasterizerStateSolid;
            static RasterizerState rasterizerStateWireFrame;

            static KeyboardState keyStateAnterior;
            static MouseState mouseStateAnterior;

            static public bool drawNormals = false;

            static public TipoCamera tipoCamera;



        /// <summary>
        /// Inicializa os componentes da camara
        /// </summary>
        /// <param name="graphics">Instância de GraphicsDevice</param>
        static public void Initialize(GraphicsDevice graphics)
            {

                tipoCamera = TipoCamera.Free;
                //Posição inicial da camâra
                position = new Vector3(0, 0, 30);
                //Inicializar as matrizes world, view e projection
                UpdateViewMatrix();
                Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), graphics.Viewport.AspectRatio, nearPlane, farPlane);
                //Coloca o rato no centro do ecrã
                Mouse.SetPosition(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
            }

            /// <summary>
            /// Calcula e atualiza a ViewMatrix para cada frame, consoante a posição e rotação da camâra
            /// </summary>
            static private Matrix cameraRotation;
            static private Vector3 cameraOriginalTarget, cameraRotatedTarget, cameraFinalTarget, cameraOriginalUpVector,
               cameraRotatedUpVector;
            static private void UpdateViewMatrix()
            {

                switch (tipoCamera)
                {
                    case TipoCamera.FPS:

                        //TODO
                        break;
                    case TipoCamera.Free:
                        //Cálculo da matriz de rotação
                        cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                        //Target
                        cameraOriginalTarget = new Vector3(0, 0, -1);
                        cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
                        cameraFinalTarget = position + cameraRotatedTarget;
                        //Cálculo do vector Up
                        cameraOriginalUpVector = new Vector3(0, 1, 0);
                        cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);
                        //Matriz View
                        View = Matrix.CreateLookAt(position, cameraFinalTarget, cameraRotatedUpVector);
                        break;
                    case TipoCamera.thirdPerson:

                        //TODO
                        break;
                    default:
                        break;
                }

                //Atualiza o frustum
                frustum = new BoundingFrustum(View * Projection);
            }

            /// <summary>
            /// Atualiza a posição da camâra
            /// </summary>
            /// <param name="vectorToAdd"></param>
            static public void AddToCameraPosition(Vector3 vectorToAdd, float moveSpeed)
            {
                Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
                position += moveSpeed * rotatedVector;
                UpdateViewMatrix();
            }

            static public void Update()
            {
                 UpdateViewMatrix();
            }


            static public void SetCameraType(TipoCamera tipo)
            {
                tipoCamera = tipo;
            }
            static public TipoCamera GetCameraType()
            {
                return tipoCamera;
            }

            static public Vector3 GetCameraPosition()
            {
                return position;
            }

            static public void SetNewRotations(float rotX, float rotY)
            {
                leftrightRot -= rotX;
                updownRot -= rotY;
            }
        }

}
