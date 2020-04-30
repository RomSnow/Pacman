using System.Windows;

namespace Pacman.GameCore
{
    public interface IMovable
    { 
        void Move(); //Осуществляется перемещение объекта по карте
        void Collision(object obj); //Обработка взаимодействия с объектами
    }
}