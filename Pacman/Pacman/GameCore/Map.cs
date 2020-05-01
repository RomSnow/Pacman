using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Linq;
using System.Text;

namespace Pacman.GameCore
{
    public class Map
    {
        public FieldItem[,] Field { get; set; }
        public static int Score { get; set; }
        public static int HealthPoints { get; set; }
        public static bool IsGameOver { get; set; }

        private static Dictionary<char, Func<Map, Point, FieldItem>> convertDict = 
            new Dictionary<char, Func<Map, Point, FieldItem>>()
            {
                {'P', (Map map, Point point) => new Player(map, point)},
                {'#', (map, point) => new Wall()},
                {'G', (Map map, Point point) => new Ghost(map, point)},
                {'.', (Map map, Point point) => new Coin(point)},
                {'*', (Map map, Point point) => new BigCoin(point)},
                {'R', (Map map, Point point) => new Respawn(point)}
            };
        public int EnemyCount { get; set; }

        private Player player;

        private List<IMovable> persons;

        public Map() { }

        public Map(string fieldString, int healthPoints)
        {
            IsGameOver = false;
            Score = 0;
            HealthPoints = healthPoints;
            persons = new List<IMovable>();
            Field = CreateFieldByString(fieldString);
        }

        public void Update()
        {
            Update(player.Directon);
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

        private FieldItem[,] CreateFieldByString(string fieldString)
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

        public string FieldToSting()
        {
            var fildString = "";
            for (var i = 0; i < Field.GetLength(0); i++)
            {
                var strbuild = new StringBuilder();
                strbuild.Append('\n');
                for (var j = 0; j < Field.GetLength(1); j++)
                {
                    var sym = ' ';
                    if (Field[i, j] is Wall)
                        sym = '#';
                    else if (Field[i, j] is Player)
                        sym = 'P';
                    else if (Field[i, j] is Ghost)
                        sym = 'G';
                    else if (Field[i, j] is Coin)
                        sym = '.';
                    else if (Field[i, j] is BigCoin)
                        sym = '*';
                    else if (Field[i, j] is Respawn)
                        sym = 'R';
                    strbuild.Append(sym);
                }
                fildString += strbuild.ToString();
            }
            
            return fildString.Remove(0, 1);
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