﻿using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryListImplement;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;


namespace AircraftFactoryView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();

            currentContainer.RegisterType<IPartLogic, PartLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAircraftLogic, AircraftLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainLogic, MainLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }

    }
}
