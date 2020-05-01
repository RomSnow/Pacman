using System.Windows;

namespace Pacman.GameCore
{
    public interface IMovable
    { 
        void Move(out FieldItem collisionObject); //Осуществляется перемещение объекта по карте
        void Collision(FieldItem obj); //Обработка взаимодействия с объектами
    }
}