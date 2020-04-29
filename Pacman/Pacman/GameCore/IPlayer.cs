namespace Pacman.GameCore
{
    public interface IPlayer : IMovable
    {
        void SetMoveDirection(MoveDirection direction);
    }
}