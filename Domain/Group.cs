namespace Domain
{
    public class Group : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Hash ToHash()
        {
            return new Hash(Name);
        }
    }
}