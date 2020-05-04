using NUnit.Framework;
using System.Collections.Generic;
using System.Windows;

namespace Pacman.GameCore
{
    public class Player : FieldItem, IPlayer
    {
        public MoveDirection Directon { get; set; }
        private Point Location { get; set; }
        private Map map;
        private HashSet<Point> coinsLocations;
        private HashSet<Point> bigCoinsLocations;
        private int TimeToBoostEnd;

        public Player(Map map, Point point)
        {
            this.map = map;
            Location = point;
            coinsLocations = map.CoinsLocations;
            bigCoinsLocations = map.BigCoinsLocations;
        }

        public void Move(out FieldItem collisionObject)
        {
            if (map.IsPlayerBoost && TimeToBoostEnd == 0)
            {
                map.IsPlayerBoost = false;
            }
            else if (map.IsPlayerBoost)
            {
                TimeToBoostEnd -= 1;
            }
        }

        public void Collision(FieldItem obj)
        {
            if (obj is Coin)
            {
                map.Score += 50;
                coinsLocations.Remove(Location);
            }
            if (obj is BigCoin)
            {
                map.Score += 200;
                bigCoinsLocations.Remove(Location);
                map.IsPlayerBoost = true;
                TimeToBoostEnd = 10;
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