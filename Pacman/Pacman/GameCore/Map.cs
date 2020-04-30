using System.Collections.Generic;
using System.Linq;

namespace Pacman.GameCore
{
    public class Map
    {
        public FieldItems[,] Field { get; set; }
        public int Points { get; set; }
        public int HealthPoints { get; set; }

        private Player _player;

        public Map(string fieldString, Dictionary<char, FieldItems> convertDict, int healthPoints)
        {
            Points = 0;
            HealthPoints = healthPoints;
            Field = CreateFieldByString(fieldString, convertDict);
        }

        private FieldItems[,] CreateFieldByString(string fieldString, Dictionary<char, FieldItems> convertDict)
        {
            var lines = fieldString.Split('\n')
                .Select(s => s.Trim('\r'))
                .ToArray<string>();
            var field = new FieldItems[lines.Length, lines[0].Length];

            for (var l = 0; l < lines.Length; l++)
            {
                for (var c = 0; c < lines[0].Length; c++)
                {
                    field[l, c] = convertDict[lines[l][c]];
                }
            }

            return field;
        }

    }
}