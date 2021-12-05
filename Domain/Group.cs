namespace Reconciler.Domain
{
    public class Group
    {
        public string GroupName { get; set; }

        public Hash ToHash()
        {
            return new Hash(GroupName);
        }
    }
}