using System;

namespace Reconciler.Domain
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Hash ToHash()
        {
            return new Hash(Name);
        }
    }
}