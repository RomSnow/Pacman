using System.Windows;

namespace Pacman.GameCore
{
    public class FieldItem { }

    public class Wall : FieldItem { }

    public class Respawn : FieldItem
    {
        public readonly Point Location;
        public Respawn(Point point)
        {
            Location = point;
        }
    }

    public class Empty : FieldItem { }
}