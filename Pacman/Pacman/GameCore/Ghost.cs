using System.Collections.Generic;
using System.Windows;

namespace Pacman.GameCore
{
    public class Ghost : FieldItem, IMovable
    {
        private bool IsGhostAlive { get; set; }
        private bool IsGhostEatable { get; set; }
        private int RewardForEating { get; set; }
        private Map Map { get; set; }
        private Point Location { get; set; }
        private MoveDirection Direction { get; set; }
        private Coin SavedCoin { get; set; }
        private BigCoin SavedBigCoin { get; set; }

        public Ghost(Map map, Point point)
        {
            Map = map;
            Location = point;
            RewardForEating = 200;
            IsGhostEatable = false;
            IsGhostAlive = true;
        }

        public void Move()
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

        public void Collision(object obj)
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
            }
            else if (obj is Coin)
            {
                SavedCoin = (Coin)obj;
            }
            else if (obj is BigCoin)
            {
                SavedBigCoin = (BigCoin)obj;
            }
        }
    }
}