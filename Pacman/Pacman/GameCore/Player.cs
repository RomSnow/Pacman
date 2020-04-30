using System.Windows;

namespace Pacman.GameCore
{
    public class Player : IPlayer
    {
        public IMovable Create(Map map, Point coordinates)
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void Collision(object obj)
        {
            throw new System.NotImplementedException();
        }

        public void SetMoveDirection(MoveDirection direction)
        {
            throw new System.NotImplementedException();
        }
    }
}