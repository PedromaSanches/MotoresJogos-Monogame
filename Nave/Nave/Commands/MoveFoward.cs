using Microsoft.Xna.Framework;


namespace Nave.Commands
{
    class MoveFoward : Command
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
            //Move foward
            Vector3 moveVector = new Vector3(0, 0, -value);
            Camera.AddToCameraPosition(moveVector);
        }
    }
}
