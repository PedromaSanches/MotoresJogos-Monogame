using Microsoft.Xna.Framework;

namespace Nave.Commands
{
    class MoveBack : Command
    {
        public override void Execute()
        {
        }

        public override void Execute(float value, float speed)
        {
            Move(value, speed);
        }

        private void Move(float value, float speed)
        {
            //Move back
            Vector3 moveVector = new Vector3(0, 0, value);
            Camera.AddToCameraPosition(moveVector, speed);
        }
    }
}
