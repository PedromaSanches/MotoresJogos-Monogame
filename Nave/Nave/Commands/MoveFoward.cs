using Microsoft.Xna.Framework;


namespace Nave.Commands
{
    class MoveFoward : Command
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
            //Move foward
            Vector3 moveVector = new Vector3(0, 0, -value);
            Camera.AddToCameraPosition(moveVector, speed);
        }
    }
}
