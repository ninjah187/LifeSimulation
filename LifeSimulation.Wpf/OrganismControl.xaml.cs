﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LifeSimulation.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LifeSimulation.Wpf
{
    /// <summary>
    /// Interaction logic for OrganismControl.xaml
    /// </summary>
    public partial class OrganismControl : OrganismControlBase
    {
        public OrganismControl(IOrganism organism, Brush fill)
            : base(organism)
        {
            InitializeComponent();

            MainEllipse.Fill = fill;
        }
    }
}
