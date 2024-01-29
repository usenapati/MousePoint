using System;

namespace Events
{
    public class EnvironmentEvents
    {
        public event Action<string> OnItemGained;
        public void GoldGained(string item)
        {
            OnItemGained?.Invoke(item);
        }
    }
}
