using System;
using System.Windows;

namespace Pacman.GameCore
{
    public class Coin : FieldItem
    {
        public int Value { get; set; }

        public Coin(Point point)
        {
            throw new NotImplementedException();
        }

        public void CollectCoin()
        {
            throw new NotImplementedException();
        }
    }

    public class BigCoin : FieldItem
    {
        private int Value { get; set; }

        public BigCoin(Point point)
        {
            
        }

        public void CollectCoin()
        {
            throw new NotImplementedException();
        }
    }
}