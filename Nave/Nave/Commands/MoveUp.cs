using Microsoft.Xna.Framework;

namespace Nave.Commands
{
    class MoveUp : Command
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
            Vector3 moveVector = new Vector3(0, value, 0);
            Camera.AddToCameraPosition(moveVector);
        }
    }
}
