using Microsoft.Xna.Framework;

namespace Nave.Commands
{
    class MoveRight : Command
    {
        public override void Execute()
        {
            Move(1.0f);
        }

        public override void Execute(float value)
        {
            Move(value);
        }

        private void Move(float value)
        {
            Vector3 moveVector = new Vector3(value, 0, 0);
            Camera.AddToCameraPosition(moveVector);
        }
    }
}
