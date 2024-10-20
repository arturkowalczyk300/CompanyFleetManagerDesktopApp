﻿using CompanyFleetManager.Models.Entities;
using CompanyFleetManager;
using System;
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

namespace CompanyFleetManagerDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for VehiclesView.xaml
    /// </summary>
    public partial class VehiclesView : UserControl
    {
        public VehiclesView()
        {
            InitializeComponent();
        }

        private Vehicle GetSelectedVehicle() => ListViewVehicles.SelectedItem as Vehicle;
    }
}