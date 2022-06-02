namespace Infrastructure.States
{
    public interface IGameStateMachine
    {
        void CastState<T>() where T : class, IState;
    }
}