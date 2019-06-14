using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guns
{
    interface IGun
    {
        int PatronsLoaded { get; set; }
        int patronsAtAll { get; set; }

        void Shoot();
    }
}
