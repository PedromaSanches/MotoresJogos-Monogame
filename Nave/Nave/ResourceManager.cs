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
    class ResourceManager
    {
        private Nave nave_template;
        public Nave NaveTemplate
        {
            get { return nave_template; }
            set { nave_template = value; }
        }

        private NaveModel nave_model;
        public NaveModel MyProperty
        {
            get { return nave_model; }
            set { nave_model = value; }
        }

        private NavePool<Nave> pool;
        public NavePool<Nave> Pool
        {
            get { return pool; }
            set { pool = value; }
        }

        public ResourceManager(ContentManager content)
        {
            // TODO: use this.Content to load your game content here
            nave_model = new NaveModel(content, "Models/Ship1/p1_saucer");
            nave_template = new Nave(nave_model); // Variável para carregar o modelo 3d da nave

            pool = new NavePool<Nave>(10);
            Vector3 worldPos = new Vector3(0, 0, 0);

            for (int i=0; i<pool.Size(); i++)
            {
                pool.SetModel(i, nave_model);
            }
        }

    }
}
