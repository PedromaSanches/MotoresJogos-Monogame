using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Nave
{
    public class Nave : INavePool
    {

        private NaveModel naveModel; // Variável para carregar o modelo 3d da nave
        private Vector3 position;

        public Matrix World { get; set; }

        private bool state;
        public bool State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public Nave(NaveModel model)
        {
            naveModel = model;
            this.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            position = new Vector3(0, 0, 0);
            state = true;
        }

        //Construtor sem parâmetros
        public Nave()
        {

        }

        /// <summary>
        /// Método usado para Desenhar o modelo e ambiente 3d do jogo
        /// </summary>
        public void Draw()
        {
            if (state) {

                //Criar BoundingSphere
                BoundingSphere auxiliar = new BoundingSphere();
                auxiliar.Center = position;
                naveModel.BoundingSphere = auxiliar;

                //Frustum - Se a nave estiver contida na área visivel pela câmara, desenha a nave
                if (Camera.frustum.Contains(naveModel.BoundingSphere) != ContainmentType.Disjoint)
                {
                    naveModel.Draw(World, Camera.View, Camera.Projection);
                }

            }
        }

        void INavePool.Initialize()
        {
            position = new Vector3(0, 0, 0);
            this.World = Matrix.CreateTranslation(position);
            state = true;
        }

        void INavePool.Release()
        {
            state = false;
        }

        void INavePool.SetPosition(Vector3 position)
        {
            this.position = position;
            this.World = Matrix.CreateTranslation(this.position);
        }
    }
}
