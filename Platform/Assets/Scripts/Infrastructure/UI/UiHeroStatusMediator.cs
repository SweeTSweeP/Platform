using System;

namespace Infrastructure.UI
{
    public class UiHeroStatusMediator : IUiHeroStatusMediator
    {
        public event Action PlayerHealed;
        public event Action PlayerDamaged;
        public event Action PlayerDied;

        public void NotifyPlayerHealed() => 
            PlayerHealed?.Invoke();

        public void NotifyPlayerDamaged() =>
            PlayerDamaged?.Invoke();

        public void NotifyPlayerDied() =>
            PlayerDied?.Invoke();
    }
}