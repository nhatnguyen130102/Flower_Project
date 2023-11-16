using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Repository
{
    public interface IDbSeeder
    {
        void Initialize();
    }
}
