using System;
using System.Windows;

namespace Pacman.GameCore
{
    public class Coin : FieldItem
    {
        private Point Location { get; set; }

        private Map map;

        public Coin(Map map, Point point)
        {
            this.map = map;
            Location = point;
        }
    }

    public class BigCoin : FieldItem
    {
        private Point Location { get; set; }

        private Map map;

        public BigCoin(Map map, Point point)
        {
            this.map = map;
            Location = point;
        }
    }
}