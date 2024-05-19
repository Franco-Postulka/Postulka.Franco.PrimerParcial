﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postulka.Franco.PrimerParcial
{
    internal class Linux:SistemaOperativo
    {
        private DistribucionLinux distribucion;
        private bool interfazGrafica;

        public DistribucionLinux Distribucion { get; }
        public bool InterfazGrafica { get;}

        public Linux(string nombre, string version, double espacioGB, EstadoSoporte soporte, DistribucionLinux distribucion, bool interfaz): base(nombre, version, espacioGB, soporte)
        {
            this.distribucion = distribucion;
            this.interfazGrafica = interfaz;
        }

        //public override bool ComprobarAlmacenamientoNecesario()
        //{
        //    bool retorno = false;
        //    long espacio_disponible = 0;
        //    DriveInfo[] drives = DriveInfo.GetDrives();

        //    foreach (DriveInfo drive in drives)
        //    {
        //        espacio_disponible += drive.AvailableFreeSpace;
        //    }

        //    if(espacio_disponible > this.EspacioGB)
        //    {
        //        retorno = true;
        //    }
        //    return retorno;
        //}
    }



}
