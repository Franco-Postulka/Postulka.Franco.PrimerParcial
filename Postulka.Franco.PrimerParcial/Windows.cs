﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Windows : SistemaOperativo
    {
        private EEdicionWindows edicion;
        private bool virtualizacionPermitida;

        public EEdicionWindows Edicion { get; set; }
        public bool VirtualizacionPermitida { get; set; }

        public Windows()
        {

        }
        public Windows(string nombre, string version, double espacio, EEstadoSoporte soporte, EEdicionWindows edicion, bool virtualizacion)
            :base(nombre,version,espacio,soporte)
        {
            /// <summary>
            /// constructor con todos los parametros
            /// </summary>
            this.edicion = edicion;
            this.virtualizacionPermitida = virtualizacion;
        }
        public Windows( string version, double espacio, EEstadoSoporte soporte, EEdicionWindows edicion, bool virtualizacion)
            : this("Windows", version, espacio, soporte, edicion, virtualizacion)
        {
            /// <summary>
            /// constructor sin nombre como parametro (se asigna como Windows)
            /// </summary>
        }
        public Windows(string nombre, string version, double espacio, EEstadoSoporte soporte, EEdicionWindows edicion)
            : this(nombre, version, espacio, soporte, edicion, true)
        {
            /// <summary>
            /// constructor sin virtualizacion como parametro (lo asigna como true)
            /// </summary>
        }
        public Windows( string version, double espacio, EEstadoSoporte soporte, EEdicionWindows edicion) 
            : this("Windows", version, espacio, soporte, edicion, true)
        {
            /// <summary>
            /// constructor sin nombre y sin virtualizacion como parametro (lo asigna como true y Windows)
            /// </summary>
        }

        public override string DevolverInformacionEspecifica()
        {
            string aceptavirtualizacion = "No";
            if (this.VirtualizacionPermitida)
            {
                aceptavirtualizacion = "si";
            }

            return $"El SO {this.Nombre} tiene las siguientes caracteristicas:\n" +
                $"Version: {this.Version}\n" +
                $"Edicion: {this.Edicion}\n" +
                $"Soporte: {this.Soporte}\n" +
                $"Ocupa:{this.EspacioGB}" +
                $"Permite virtualizacion: {aceptavirtualizacion}";
        }
        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(Windows unwindows, Windows otrowindows)
        {
            return (unwindows.Version == otrowindows.Version && unwindows.Edicion == otrowindows.Edicion);
        }
        public static bool operator !=(Windows unwindows, Windows otrowindows)
        {
            return !(unwindows == otrowindows);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            else
            {
                return (this == (Windows)obj);
            }
        }
    }
}
