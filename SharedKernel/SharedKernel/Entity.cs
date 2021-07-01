using SharedKernel.Events;
using System;
using System.Collections.Generic;

namespace SharedKernel
{
    public abstract class Entity
    {
        public int Id { get; }

        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents?.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents?.Clear();

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Entity other)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (GetType() != obj.GetType())
                return false;
            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() => (GetUnproxiedType(this).ToString() + Id).GetHashCode();

        private static Type GetUnproxiedType(object obj)
        {
            const string EFCoreProxyPrefix = "Castle.Proxies.";

            var type = obj.GetType();
            var typeString = type.ToString();

            if (typeString.Contains(EFCoreProxyPrefix))
                return type.BaseType;
            return type;
        }
    }
}
