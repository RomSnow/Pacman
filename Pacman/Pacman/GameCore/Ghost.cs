using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace Pacman.GameCore
{
    public class Ghost : FieldItem, IMovable
    {
        private bool IsGhostAlive { get; set; }
        private Map map;
        private Point location;
        private MoveDirection direction;

        public Ghost(Map map, Point point)
        {
            this.map = map;
            location = point;
            IsGhostAlive = true;
        }

        public void Move(out FieldItem collisionObject)
        {
            ChangeDirection();
            if (direction == MoveDirection.Right)
            {
                map.Field[(int)location.Y, (int)location.X] = new Empty();
                location = new Point(location.X + 1, location.Y);
                collisionObject = map.Field[(int)location.Y, (int)location.X];
                map.Field[(int)location.Y, (int)location.X] = this;
            }
            else if (direction == MoveDirection.Left)
            {
                map.Field[(int)location.Y, (int)location.X] = new Empty();
                location = new Point(location.X - 1, location.Y);
                collisionObject = map.Field[(int)location.Y, (int)location.X];
                map.Field[(int)location.Y, (int)location.X] = this;
            }
            else if (direction == MoveDirection.Down)
            {
                map.Field[(int)location.Y, (int)location.X] = new Empty();
                location = new Point(location.X, location.Y + 1);
                collisionObject = map.Field[(int)location.Y, (int)location.X];
                map.Field[(int)location.Y, (int)location.X] = this;
            }
            else if (direction == MoveDirection.Up)
            {
                map.Field[(int)location.Y, (int)location.X] = new Empty();
                location = new Point(location.X, location.Y - 1);
                collisionObject = map.Field[(int)location.Y, (int)location.X];
                map.Field[(int)location.Y, (int)location.X] = this;
            }
            else
            {
                collisionObject = map.Field[(int)location.Y, (int)location.X];
            }
        }

        private void ChangeDirection()
        {
            if (direction == MoveDirection.Right &&
                map.Field[(int)location.Y, (int)location.X + 1] is Wall)
            {
                direction = ChooseFreeWay(map.Field, location);
            }
            else if (direction == MoveDirection.Left &&
                map.Field[(int)location.Y, (int)location.X - 1] is Wall)
            {
                direction = ChooseFreeWay(map.Field, location);
            }
            else if (direction == MoveDirection.Down &&
                map.Field[(int)location.Y + 1, (int)location.X] is Wall)
            {
                direction = ChooseFreeWay(map.Field, location);
            }
            else if (direction == MoveDirection.Up &&
                map.Field[(int)location.Y - 1, (int)location.X] is Wall)
            {
                direction = ChooseFreeWay(map.Field, location);
            }
        }

        private MoveDirection ChooseFreeWay(FieldItem[,] field, Point point)
        {
            var listOfWays = new List<MoveDirection>();
            for (var i = -1; i <= 1; i++)
                for (var j = -1; j <= 1; j++)
                {
                    if (i != j && (i == 0 || j == 0) &&
                        !(field[(int)point.Y + j, (int)point.X + i] is Wall))
                    {
                        listOfWays.Add(MakeDirection(new Point(i, j)));
                    }
                }
            var r = new Random();
            var number = r.Next(0, listOfWays.Count - 1);
            return listOfWays.Skip(number).FirstOrDefault();
        }

        private MoveDirection MakeDirection(Point point)
        {
            if (point.X == 1)
                return MoveDirection.Right;
            else if (point.X == -1)
                return MoveDirection.Left;
            else if (point.Y == 1)
                return MoveDirection.Down;
            else
                return MoveDirection.Up;
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