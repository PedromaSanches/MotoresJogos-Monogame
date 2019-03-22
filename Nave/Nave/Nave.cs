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
        public Nave(ContentManager content)
        {
           naveModel = new NaveModel(content, "Models/Ship1/p1_saucer");
            
            this.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            state = true;
        }

        //Construtor sem parâmetros
        public Nave()
        {

        }

        /// <summary>
        /// Método usado para Desenhar o modelo e ambiente 3d do jogo
        /// </summary>
        /// <param name="model">modelo 3d da nave</param>
        /// <param name="world">mundo de jogo</param>
        /// <param name="view"></param>
        /// <param name="projection"></param>
        public void Draw()
        {
            if (state) {

                naveModel.Draw(World, Camera.View, Camera.Projection);
                
            }
        }

        void INavePool.Initialize()
        {
            this.World = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            state = true;
        }

        void INavePool.Release()
        {
            state = false;
        }
    }
}
