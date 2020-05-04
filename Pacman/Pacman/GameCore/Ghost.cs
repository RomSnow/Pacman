using System.Collections.Generic;
using System.Windows;

namespace Pacman.GameCore
{
    public class Ghost : FieldItem, IMovable
    {
        private bool IsGhostAlive { get; set; }
        private Map Map { get; set; }
        private Point Location { get; set; }
        private MoveDirection Direction { get; set; }

        public Ghost(Map map, Point point)
        {
            Map = map;
            Location = point;
            IsGhostAlive = true;
        }

        public void Move(out FieldItem collisionObject)
        {
            throw new System.NotImplementedException();
            // Потребуется добавить возврат коинов на место, которое будет
            // простой проверкой, не явлется ли Saved(Big)Coin null'ом
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
            if (obj is Player && IsGhostEatable && IsGhostAlive)
            {
                IsGhostAlive = false;
                Map.Score += RewardForEating;
            }
            else if (obj is Player && !IsGhostEatable && IsGhostAlive)
            {
                Map.HealthPoints--;
                // Нужно ещё дописать что-то типа ухода Пакмана в неуязвимость
                // на пару секунд, чтобы его не задамажили в одно мгновение
                
                //Если пакман погиб, то его надо перемещать на точку респауна
            }
        }
    }
}