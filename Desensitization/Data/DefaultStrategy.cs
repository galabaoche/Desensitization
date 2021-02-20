using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desensitization.Data
{
    public class DefaultStrategy : IDesensitizationStrategy
    {
        public int Order { get; set; } = 10000;
        public string Name { get; set; } = string.Empty;
    }
}
