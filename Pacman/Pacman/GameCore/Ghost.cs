using System.Windows;

namespace Pacman.GameCore
{
    public class Ghost : IMovable
    {
        private bool isGhostEatable;
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
    }
}