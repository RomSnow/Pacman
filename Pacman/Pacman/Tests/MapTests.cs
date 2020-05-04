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
            {'.', (Map map, Point point) => new Coin(map, point)},
            {'*', (Map map, Point point) => new BigCoin(map, point)},
            {'R', (Map map, Point point) => new Respawn(point)},
            {' ', (Map map, Point point) => new Empty()}
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
                    {new Wall(), new Coin(new Map(),  new Point()), new Player(new Map(), new Point()), new Coin(new Map(),new Point()), new Wall()},
                    {new Wall(), new Wall(), new Wall(), new Wall(), new Wall()}
                };

            var m = new Map(fieldString, 0).Field;
            
            for (var i = 0; i < normalField.GetLength(0); i++)
                for (var j = 0; j < normalField.GetLength(1); j++)
                    Assert.True(normalField[i,j].GetType() == m[i, j].GetType());
        }

        [Test]
        public static void TestToString()
        {
            var startString = "#####\n#.P.#\n#####";
            var map = new Map(startString, 0);
            Assert.AreEqual(startString, map.ToString());
        }

        [Test]
        public static void DoesPlayerMove()
        {
            var mapString = "#####\n" +
                            "#..P#\n" +
                            "#####\n";
            var finalMapString = "#####\n" +
                                 "#P  #\n" +
                                 "#####";
            var map = new Map(mapString, 1);
            map.Update(MoveDirection.Left);
            map.Update();
            Assert.AreEqual(finalMapString, map.ToString());
            
        }

        [Test]
        public static void DoesGhostAndRespawnWork()
        {
            var mapString = "#####\n" +
                            "#G P#\n" +
                            "#  R#\n" +
                            "#####\n";
            var finalMap = "#####\n" +
                           "#  G#\n" +
                           "#  P#\n" +
                           "#####";
            var map = new Map(mapString, 2);
            map.Update(MoveDirection.Left);
            map.Update();
            Assert.AreEqual(finalMap, map.ToString());
            Assert.AreEqual(1, map.HealthPoints);
        }

        [Test]
        public static void DoesGhostEatCoin()
        {
            var mapString = "######\n" +
                            "#G..P#\n" +
                            "######\n";
            var finalMap = "######\n" +
                           "# .GP#\n" +
                           "######";
            var map = new Map(mapString, 0);
            map.Update(MoveDirection.Right);
            map.Update();
            Assert.AreEqual(finalMap, map.ToString());
        }
    }
}