using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM_simple
{
    public class BoundaryCondition
    {
        public int Type { get; private set; }
        public F Value { get; private set; }
        public int Vertix { get; private set; }

        public BoundaryCondition(int type, F value, int vertix)
        {
            Type = type;
            Value = value;
            Vertix = vertix;
        }
    }
}
