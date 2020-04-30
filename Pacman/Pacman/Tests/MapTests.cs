using System.Collections.Generic;
using NUnit.Framework;
using Pacman.GameCore;

namespace Pacman.Tests
{
    [TestFixture]
    public static class MapTests
    {
        private static Dictionary<char, FieldItems> converDict = new Dictionary<char, FieldItems>()
        {
            {'P', FieldItems.Player},
            {'#', FieldItems.Wall},
            {'G', FieldItems.Ghost},
            {'.', FieldItems.Coin},
            {'*', FieldItems.BigCoin},
            {'R', FieldItems.Respawn}
        };

        [Test]
        public static void TestMapCreator()
        {
            var fieldString =
                @"#####
#.P.#
#####";

            var normvalField = new FieldItems[,]
                {
                    {FieldItems.Wall, FieldItems.Wall, FieldItems.Wall, FieldItems.Wall,FieldItems.Wall }, 
                    {FieldItems.Wall, FieldItems.Coin, FieldItems.Player, FieldItems.Coin, FieldItems.Wall },
                    {FieldItems.Wall, FieldItems.Wall, FieldItems.Wall, FieldItems.Wall,FieldItems.Wall }
                };
            
            Assert.AreEqual(new Map(fieldString, converDict, 0).Field,
                normvalField);
        }
        
    }
}