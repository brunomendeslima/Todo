using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Todo.Domain.Entities {

    public abstract class Entity : IEquatable<Entity>{

        [BsonIgnore]        
        public Guid Id { get; protected set; }   
        public Entity()
        { }

        public Entity(Guid id)
        {
            Id = id;
        }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}