using System;
using System.Windows;

namespace Pacman.GameCore
{
    public class Coin : FieldItem
    {
        public int Value { get; set; }
        private bool IsPointAvailable { get; set; }
        private Point Location { get; set; }

        private Map map;

        public Coin(Map map, Point point)
        {
            this.map = map;
            IsPointAvailable = true;
            Location = point;
            Value = 50;
        }

        public void CollectCoin()
        {
            if (IsPointAvailable)
            {
                map.Score += Value;
                IsPointAvailable = false;
            }
        }
    }

    public class BigCoin : FieldItem
    {
        private int Value { get; set; }
        private bool IsPointAvailable { get; set; }
        private Point Location { get; set; }

        private Map map;

        public BigCoin(Map map, Point point)
        {
            this.map = map;
            IsPointAvailable = true;
            Location = point;
            Value = 200;
        }

        public void CollectCoin()
        {
            if (IsPointAvailable)
            {
                map.Score += Value;
                IsPointAvailable = false;
            }
        }
    }
}