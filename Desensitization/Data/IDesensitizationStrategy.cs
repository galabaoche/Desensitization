using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desensitization.Data
{
    public interface IDesensitizationStrategy
    {
        int Order { get; set; }
        string Name { get; set; }
    }
}
