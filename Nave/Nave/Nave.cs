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

        private Model model; // Variável para carregar o modelo 3d da nave
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
            //Load do modelo da nave
            model = content.Load<Model>("Models/Ship1/p1_saucer");
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
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = World;
                        effect.View = Camera.View;
                        effect.Projection = Camera.Projection;
                    }
                    mesh.Draw();
                }
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
