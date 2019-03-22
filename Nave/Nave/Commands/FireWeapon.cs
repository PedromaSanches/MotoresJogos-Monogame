using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nave.Commands
{
    class FireWeapon : Command
    {
        public override void Execute()
        {
            Fire();
        }

        public override void Execute(float value)
        {
            Fire();
        }

        private void Fire()
        {
            //Put the code where of firing a bullet
        }
    }
}
