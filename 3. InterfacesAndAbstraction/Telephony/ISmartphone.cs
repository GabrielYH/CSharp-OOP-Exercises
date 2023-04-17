using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    public interface ISmartphone
    {
        void CallNumber(string number);
        void BrowseWeb(string website);
    }
}
