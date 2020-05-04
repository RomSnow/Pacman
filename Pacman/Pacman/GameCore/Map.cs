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
        public int Score { get; set; }
        public int HealthPoints { get; set; }
        public bool IsGameOver { get; set; }
        public HashSet<Point> CoinsLocations { get; set; }
        public HashSet<Point> BigCoinsLocations { get; set; }
        public Point RespawnPoint { get; private set; }
        public int EnemyCount { get; set; }
        public bool IsPlayerBoost { get; set; }

        private Dictionary<char, Func<Map, Point, FieldItem>> convertDict = 
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
            Update(player.direction);
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
                .Where(s => s != "")
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
            if (item is IMovable movable)
            {
                if (movable is Player playerObj)
                    player = playerObj;
                else
                    EnemyCount++;
                
                persons.Add(movable);
            }
            else if (item is Respawn respawnObj)
                RespawnPoint = respawnObj.Location;
            else if (item is Coin coinObj)
                CoinsLocations.Add(coinObj.Location);
            else if (item is BigCoin bigCoinObj)
                BigCoinsLocations.Add(bigCoinObj.Location);
                
        }

        public override string ToString() 
        {
            var fieldString = "";
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
                fieldString += strbuild.ToString();
            }
            
            return fieldString.Remove(0, 1);
        }
    }
}