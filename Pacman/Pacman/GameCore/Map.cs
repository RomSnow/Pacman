using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace Pacman.GameCore
{
    public class Map
    {
        public FieldItem[,] Field { get; set; }
        public static int Score { get; set; }
        public static int HealthPoints { get; set; }
        public static bool IsGameOver { get; set; }

        private Player player;

        private List<IMovable> persons;

        public Map(string fieldString, Dictionary<char, Func<Map, Point, FieldItem>> convertDict,
            int healthPoints)
        {
            IsGameOver = false;
            Score = 0;
            HealthPoints = healthPoints;
            Field = CreateFieldByString(fieldString, convertDict);
        }

        private FieldItem[,] CreateFieldByString(string fieldString,
            Dictionary<char, Func<Map, Point, FieldItem>> convertDict)
        {
            var lines = fieldString.Split('\n')
                .Select(s => s.Trim('\r'))
                .ToArray<string>();
            var field = new FieldItem[lines.Length, lines[0].Length];

            for (var l = 0; l < lines.Length; l++)
            {
                for (var c = 0; c < lines[0].Length; c++)
                {
                    field[l, c] = convertDict[lines[l][c]](this, new Point(c, l));
                }
            }

            return field;
        }

    }
}