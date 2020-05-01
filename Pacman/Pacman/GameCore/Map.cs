using System;
using System.Collections.Generic;
using System.Data;
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

        public int EnemyCount { get; set; }

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

        public void Update(MoveDirection direction)
        {
            player.SetMoveDirection(direction);
            foreach (var person in persons)
            {
                person.Move(out var collisionItem);
                person.Collision(collisionItem);
                if (HealthPoints == 0 || EnemyCount == 0)
                {
                    IsGameOver = true;
                    return;
                }
            }
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
                    SetItems(field[l, c]);
                }
            }

            return field;
        }

        private void SetItems(FieldItem item)
        {
            if (item is IMovable)
            {
                if (item is Player)
                    player = (Player) item;
                else
                    EnemyCount++;
                
                persons.Add((IMovable) item);
            }
        }

    }
}