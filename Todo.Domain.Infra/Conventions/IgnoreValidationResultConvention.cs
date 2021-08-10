using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Todo.Domain.Infra.Conventions
{
    internal class IgnoreValidationResultConvention : ConventionBase, IMemberMapConvention
    {
        public void Apply(BsonMemberMap memberMap)
        {
            if (memberMap.MemberName == "ValidationResult")
                memberMap.SetShouldSerializeMethod(o => false);
        }
    }
}
