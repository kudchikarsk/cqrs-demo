namespace Logic.Utils
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; }
    }
}