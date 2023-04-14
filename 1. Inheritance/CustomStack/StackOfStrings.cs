using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        {
            return this.Count == 0;
        }

        public Stack<string> AddRange(List<string> elements)
        {
            foreach (var item in elements)
            {
                this.Push(item);
            }
            return this;
        }
    }
}
