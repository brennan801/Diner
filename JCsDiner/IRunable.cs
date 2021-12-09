using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public interface IRunable
    {
        public void Run1(Restaurant resturant, int beatNumber);
        public void PrintStats();
    }
}
