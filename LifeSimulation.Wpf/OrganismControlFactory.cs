using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LifeSimulation.Core;

namespace LifeSimulation.Wpf
{
    public abstract class OrganismControlBaseFactory
    {
        public abstract OrganismControlBase CreateOrganismControl(IOrganism organism, Brush fill);
    }

    public class OrganismControlFactory : OrganismControlBaseFactory
    {
        public override OrganismControlBase CreateOrganismControl(IOrganism organism, Brush fill)
            => new OrganismControl(organism, fill);
    }

    public class OrganismControlDebugFactory : OrganismControlBaseFactory
    {
        public override OrganismControlBase CreateOrganismControl(IOrganism organism, Brush fill)
            => new OrganismControlDebug(organism, fill);
    }
}
