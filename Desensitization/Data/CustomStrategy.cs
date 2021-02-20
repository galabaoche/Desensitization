using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desensitization.Data
{

    [BsonIgnoreExtraElements]
    public class CustomStrategy : IDesensitizationStrategy
    {
        public ObjectId _id { get; set; }
        public IDictionary<string,string> InlineConstraint { get; set; } 
        public int Order { get; set; } = 100;
        public string Name { get; set; }
    }
}
