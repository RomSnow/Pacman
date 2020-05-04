using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;

namespace Pacman.GameCore
{
    public class Player : FieldItem, IPlayer
    {
        public MoveDirection Directon { get; set; }
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
                timeToEndboost -= 0;
            }
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

            }
        }

        public void SetMoveDirection(MoveDirection direction)
        {
            Directon = direction;
        }
    }
}