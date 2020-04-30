using System;
using System.Windows;

namespace Pacman.GameCore
{
    public class Coin : FieldItem
    {
        public int Value { get; set; }
        private bool IsPointAvailable { get; set; }
        private Point Location { get; set; }

        public Coin(Point point)
        {
            IsPointAvailable = true;
            Location = point;
            Value = 50;
        }

        public void CollectCoin()
        {
            if (IsPointAvailable)
            {
                Map.Score += Value;
                IsPointAvailable = false;
            }
        }
    }

    public class BigCoin : FieldItem
    {
        private int Value { get; set; }
        private bool IsPointAvailable { get; set; }
        private Point Location { get; set; }

        public BigCoin(Point point)
        {
            IsPointAvailable = true;
            Location = point;
            Value = 200;
        }

        public void CollectCoin()
        {
            if (IsPointAvailable)
            {
                Map.Score += Value;
                IsPointAvailable = false;
            }
        }
    }
}