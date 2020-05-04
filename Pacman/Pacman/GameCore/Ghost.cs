using System.Collections.Generic;
using System.Windows;

namespace Pacman.GameCore
{
    public class Ghost : FieldItem, IMovable
    {
        private bool IsGhostAlive { get; set; }
        private Map map;
        private Point location;
        private MoveDirection Direction { get; set; }

        public Ghost(Map map, Point point)
        {
            this.map = map;
            location = point;
            IsGhostAlive = true;
        }

        public void Move(out FieldItem collisionObject)
        {
            throw new System.NotImplementedException();
        }

        private void ChangeDirection()
        {
            var dict = new Dictionary<int, MoveDirection>
            {
                { 0, MoveDirection.Left },
                { 1, MoveDirection.Right },
                { 2, MoveDirection.Up },
                { 3, MoveDirection.Down }
            };
            // Добавить рандом, который выберет свободное направление
            // или направление на Пакмана
        }

        public void Collision(FieldItem obj)
        {
            if (obj is Player && map.IsPlayerBoost && IsGhostAlive)
            {
                IsGhostAlive = false;
            }
        }

        public Point GetLocation()
        {
            return location;
        }
    }
}