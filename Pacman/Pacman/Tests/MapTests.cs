using System;
using System.Collections.Generic;
using System.Windows;
using NUnit.Framework;
using Pacman.GameCore;

namespace Pacman.Tests
{
    [TestFixture]
    public static class MapTests
    {
        private static Dictionary<char, Func<Map, Point, FieldItem>> converDict = 
            new Dictionary<char, Func<Map, Point, FieldItem>>()
        {
            {'P', (Map map, Point point) => new Player(map, point)},
            {'#', (map, point) => new Wall()},
            {'G', (Map map, Point point) => new Ghost(map, point)},
            {'.', (Map map, Point point) => new Coin(point)},
            {'*', (Map map, Point point) => new BigCoin(point)},
            {'R', (Map map, Point point) => new Respawn(point)}
        };

        [Test]
        public static void TestMapCreator()
        {
            var fieldString =
                @"#####
#.P.#
#####";

            var normalField = new FieldItem[,]
                {
                    {new Wall(), new Wall(), new Wall(), new Wall(), new Wall()}, 
                    {new Wall(), new Coin(new Point(1, 1)), new Player(
                        new Map(fieldString, converDict, 0), new Point(2,1)), new Coin( new Point(3, 1)), new Wall()},
                    {new Wall(), new Wall(), new Wall(), new Wall(), new Wall()}
                };

            var m= new Map(fieldString, converDict, 0).Field;
            
            Assert.AreEqual(normalField, new Map(fieldString, converDict, 0).Field);
        }
        
    }
}