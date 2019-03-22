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
    /// <summary>
    /// Class que contém os métodos de ca
    /// </summary>
    public class NaveModel
    {

        private Model model; // Variável para carregar o modelo 3d da nave

        public NaveModel(ContentManager content, string path)
        {
            //Load do modelo da nave
            model = content.Load<Model>(path);
        }

        /// <summary>
        /// Método usado para Desenhar o modelo e ambiente 3d do jogo
        /// </summary>
        /// <param name="model">modelo 3d da nave</param>
        /// <param name="world">mundo de jogo</param>
        /// <param name="view"></param>
        /// <param name="projection"></param>
        public void Draw(Matrix World, Matrix View, Matrix Projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = World;
                    effect.View = View;
                    effect.Projection = Projection;
                }
                mesh.Draw();
            }
            
        }

    }
}
