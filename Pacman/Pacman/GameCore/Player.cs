using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;

namespace Pacman.GameCore
{
    public class Player : FieldItem, IPlayer
    {
        public MoveDirection direction;
        private Point location;
        private Map map;
        private int timeToEndboost;
        private HashSet<Point> coinsLocations;
        private HashSet<Point> bigCoinsLocations;

        public Player(Map map, Point point)
        {
            location = point;
            this.map = map;
            coinsLocations = map.CoinsLocations;
            bigCoinsLocations = map.BigCoinsLocations;
        }

        public void Move(out FieldItem collisionObject)
        {
            if (map.IsPlayerBoost && timeToEndboost == 0)
            {
                map.IsPlayerBoost = false;
            }
            else if (map.IsPlayerBoost)
            {
                timeToEndboost -= 1;
            }
            if (direction == MoveDirection.Right && 
                !(map.Field[(int)location.Y, (int)location.X + 1] is Wall))
            {
                map.Field[(int)location.Y, (int)location.X] = new Empty();
                location = new Point(location.X + 1, location.Y);
                collisionObject = map.Field[(int)location.Y, (int)location.X];
                map.Field[(int)location.Y, (int)location.X] = this;
            }
            else if (direction == MoveDirection.Left &&
                !(map.Field[(int)location.Y, (int)location.X - 1] is Wall))
            {
                map.Field[(int)location.Y, (int)location.X] = new Empty();
                location = new Point(location.X - 1, location.Y);
                collisionObject = map.Field[(int)location.Y, (int)location.X];
                map.Field[(int)location.Y, (int)location.X] = this;
            }
            collisionObject = map.Field[(int)location.Y, (int)location.X];
        }

        public void Collision(FieldItem obj)
        {
            if (obj is Coin)
            {
                map.Score += 50;
                coinsLocations.Remove(location);
            }
            if (obj is BigCoin)
            {
                map.Score += 200;
                bigCoinsLocations.Remove(location);
                map.IsPlayerBoost = true;
                timeToEndboost = 10;
            }
            if (obj is Ghost)
            {
                if (!map.IsPlayerBoost)
                {
                    map.HealthPoints -= 1;
                    location = map.RespawnPoint;
                }
                else
                {
                    map.Score += 200;
                }
            }
        }

        public void SetMoveDirection(MoveDirection direction)
        {
            this.direction = direction;
        }

        public Point GetLocation()
        {
            return location;
        }
    }
}