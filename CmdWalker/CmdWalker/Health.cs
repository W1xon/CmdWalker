namespace CmdWalker
{
    internal class Health
    {
        private int _max;
        private int _current;
        public Health(int max)
        {
            _max = max;
            _current = max;
        }
        public bool TryTakeDamage(int damage)
        {
            _current -= damage;
            if (_current < 0) _current = 0;
            return _current > 0;
        }
        public void Reset() => _current = 0;
        public bool IsZero => _current == 0;
    }
}
