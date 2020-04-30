using System.Windows;

namespace Pacman.GameCore
{
    public class FieldItem { }

    public class Wall : FieldItem { }

    public class Respawn : FieldItem {
        public Respawn(Point point)
        {
            throw new System.NotImplementedException();
        }
    }
}