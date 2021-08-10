using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Todo.Domain.Entities 
{
    public class TodoItem : Entity 
    {
        [BsonRepresentation(BsonType.String)]
        public new Guid Id
        {
            get => base.Id;
            set => base.Id = value;
        }

        public string Title { get; private set; }
        public bool Done { get; private set; }
        public DateTime Date { get; private set; }
        public string User { get; private set; }
        public TodoItem(string title, DateTime date, string user)           
        {
            Title = title;
            Done = false;
            Date = date;
            User = user;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void MarkAsDone()
        {
            Done = true;
        }
        
        public void MarkAsUndone()
        {
            Done = false;
        }
    }
}