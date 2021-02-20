using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desensitization.Data
{
    public class PropertyRule
    {
        public ObjectId _id { get; set; }
        public string StrategyName { get; set; }
        public string PropertyFullName { get; set; }
        public string RuleName { get; set; }
        public string RuleJson { get; set; }
        public string Properties { get; set; }
        // public int MyProperty { get; set; }
    }
}
