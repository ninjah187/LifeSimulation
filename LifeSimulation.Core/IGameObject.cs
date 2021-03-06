﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSimulation.Core
{
    public interface IGameObject
    {
        Guid Id { get; }

        Point Position { get; set; }
        double Size { get; set; }

        void Update();
    }
}
