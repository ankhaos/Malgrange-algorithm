using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Мальгранжа
{
    internal class GraphData
    {
        string? _count;
        List<int> _connectedvertices;
        public GraphData()
        {
            _connectedvertices = new List<int>();
        }
        public string Count { get { return _count; } set { _count = value;} }
        public List<int> Connectedvertices { get { return _connectedvertices; } set { _connectedvertices = value; } }
    }
}
