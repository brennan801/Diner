using System.Collections.Generic;

namespace JCsDiner
{
    public class Party
    {
        public IEnumerable<Customer> customers { get; internal set; }
    }
}