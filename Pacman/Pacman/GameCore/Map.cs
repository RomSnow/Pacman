using System.Collections.Generic;

namespace Pacman.GameCore
{
    public class Map
    {
        public FieldItems[,] Field { get; set; }
        public int Points { get; set; }
        public int HealthPoints { get; set; }

        public Map(string fieldString, Dictionary<char, FieldItems> convertDict, int healthPoints)
        {
            Points = 0;
            HealthPoints = healthPoints;
            Field = CreateFieldByString(fieldString, convertDict);
        }

        private FieldItems[,] CreateFieldByString(string fieldString, Dictionary<char, FieldItems> convertDict)
        {
            var lines = fieldString.Split('\n');
            var field = new FieldItems[lines[0].Length, lines.Length];

            for (var l = 0; l < lines.Length; l++)
            {
                for (var c = 0; c < lines[0].Length; c++)
                {
                    field[c, l] = convertDict[lines[l][c]];
                }
            }

            return field;
        }

    }
}