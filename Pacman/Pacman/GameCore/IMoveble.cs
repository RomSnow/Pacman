namespace Pacman.GameCore
{
    public interface IMovable
    {
        IMovable Create(Map map); //Вызыватется для создания объекта
        void Move(); //Осуществляется перемещение объекта по карте
        void Collision(object obj); //Обработка взаимодействия с объектами
    }
}