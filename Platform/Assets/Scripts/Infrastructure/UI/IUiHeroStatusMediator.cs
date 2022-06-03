using System;

namespace Infrastructure.UI
{
    public interface IUiHeroStatusMediator
    {
        event Action PlayerHealed;
        event Action PlayerDamaged;
        event Action PlayerDied;
        void NotifyPlayerHealed();
        void NotifyPlayerDamaged();
        void NotifyPlayerDied();
    }
}