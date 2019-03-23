using Microsoft.Xna.Framework;

namespace Nave.Commands
{
    class MoveUp : Command
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
            Vector3 moveVector = new Vector3(0, value, 0);
            Camera.AddToCameraPosition(moveVector, speed);
        }
    }
}
