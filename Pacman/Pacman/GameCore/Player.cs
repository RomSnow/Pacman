using System.Windows;

namespace Pacman.GameCore
{
    public class Player : FieldItem, IPlayer
    {
        private Point Location { get; set; }
        private MoveDirection Directon { get; set; }
        private Map Map { get; set; }

        public Player(Map map, Point point)
        {
            Location = point;
            Map = map;
        }

        public void Move()
        {
            throw new System.NotImplementedException();
            // Реализация должна быть довольно простой -
            // перемещаемся туда, куда указывает Direction
        }

        public void Collision(object obj)
        {
            throw new System.NotImplementedException();
            // Не забыть об обработке сбора коинов
            // Подумать о том, где уменьшать жизни игрока (тут или в Ghost.cs)
        }

        public void SetMoveDirection(MoveDirection direction)
        {
            Directon = direction;
        }
    }
}