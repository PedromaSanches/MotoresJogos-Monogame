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
        private Random var = new Random();

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
            position = new Vector3(0, 0, 0);
            this.World = Matrix.CreateTranslation(position);
            state = true;
        }

        //Construtor sem parâmetros
        public Nave()
        {
            naveModel = null;
            position = new Vector3(var.Next(0, 10), var.Next(0, 10), var.Next(0, 10));
            this.World = Matrix.CreateTranslation(position);
            state = true;
        }

        //Construtor por copia
        public Nave(Nave nave)
        {
            this.naveModel = nave.naveModel;
            this.position = new Vector3(var.Next(0, 10), var.Next(0, 10), var.Next(0, 10));
            this.World = Matrix.CreateTranslation(position);
            this.state = nave.state;
        }

        public Vector3 Position()
        {
            return this.position;
        }

        /// <summary>
        /// Método usado para Desenhar o modelo e ambiente 3d do jogo
        /// </summary>
        void INavePool.Draw()
        {
            if (state) {

                naveModel.Draw(World, Camera.View, Camera.Projection);
                //Criar BoundingSphere
                BoundingSphere auxiliar = new BoundingSphere();
                auxiliar.Center = position;
                naveModel.BoundingSphere = auxiliar;

            }
        }

        public void DrawME()
        {
            if (state)
            {

                naveModel.Draw(World, Camera.View, Camera.Projection);
                //Criar BoundingSphere
                BoundingSphere auxiliar = new BoundingSphere();
                auxiliar.Center = position;
                naveModel.BoundingSphere = auxiliar;

            }
        }

        void INavePool.Initialize()
        {
            position = new Vector3(var.Next(0, 10), var.Next(0, 10), var.Next(0, 10));
            this.World = Matrix.CreateTranslation(position);
            state = true;
        }

        void INavePool.Release()
        {
            state = false;
        }

        void INavePool.SetNaveModel(NaveModel nm)
        {
            naveModel = nm;
        }
    }
}
