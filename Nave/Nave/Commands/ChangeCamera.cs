using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nave.Commands
{
    class ChangeCamera : Command
    {
        public override void Execute()
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

        public override void Execute(float value, float speed)
        {
        }
    }
}
