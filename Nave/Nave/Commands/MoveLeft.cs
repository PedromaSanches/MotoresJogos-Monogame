using Microsoft.Xna.Framework;

namespace Nave.Commands
{
    class MoveLeft : Command
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
            Vector3 moveVector = new Vector3(-value, 0, 0);
            Camera.AddToCameraPosition(moveVector, speed);
        }
    }
}
