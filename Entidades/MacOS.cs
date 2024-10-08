﻿using Entidades.Enumerados;

namespace Entidades
{
    public class MacOS : SistemaOperativo
    {
        private bool integracionIcloud;
        private bool compatibleConProcesadorApple;

        public bool IntegracionIcloud { get { return this.integracionIcloud; } set { this.integracionIcloud = value; } }
        public bool CompatibleConProcesadorApple { get { return this.compatibleConProcesadorApple; } set { this.compatibleConProcesadorApple = value; } }

        public MacOS()
        {

        }
        public MacOS(string nombre, string version, double espacio, bool compatibleApple, EEstadoSoporte soporte, bool integracionIcloud)
            : base(nombre, version, espacio, soporte)
        {
            /// <summary>
            /// constructor con todos los parametros
            /// </summary>
            this.integracionIcloud = integracionIcloud;
            this.compatibleConProcesadorApple = compatibleApple;
        }
        public MacOS(string nombre, string version, double espacio, bool compatibleApple, EEstadoSoporte soporte)
            : this(nombre, version, espacio, compatibleApple, soporte, false)
        {
            /// <summary>
            /// constructor sin el parametro integracionIcloud (se asigna false)
            /// </summary>
        }
        public MacOS(string nombre, string version, double espacio, EEstadoSoporte soporte, bool integracionIcloud)
            : this(nombre, version, espacio, false, soporte, integracionIcloud)
        {
            /// <summary>
            /// constructor sin el parametro compatibleApple (se asigna false )
            /// </summary>
        }

        public MacOS(string nombre, string version, double espacio, EEstadoSoporte soporte)
            : this(nombre, version, espacio, false, soporte, false)
        {
            /// <summary>
            /// constructor sin los parametros compatibleApple e integracionIcloud (se asigna false  y false)
            /// </summary>
        }


        public override string DevolverInformacionEspecifica()
        {
            string procesador = "Intel";
            if (this.CompatibleConProcesadorApple)
            {
                procesador = "Apple";
            }
            string tienICloud = "No";
            if (this.IntegracionIcloud)
            {
                tienICloud = "Si";
            }

            return $"El SO MacOS tiene las siguientes caracteristicas:\n" +
                $"Nombre: {this.Nombre}\n" +
                $"Version: {this.Version}\n" +
                $"Ocupa: {this.EspacioGB} GB\n" +
                $"Soporte actual: {this.Soporte}\n" +
                $"Marca de procesado compatible: {procesador} \n" +
                $"Posee integracion con Icloud: {tienICloud}";
        }

        public override string Descargar()
        {
            return $"Sistema operativo MacOS {this.Nombre} instalado";
        }

        public override string ToString()
        {
            string icloud = "sin";
            if (this.IntegracionIcloud)
            {
                icloud = "con";
            }
            string compatibleApple = "incompatible";
            if (this.compatibleConProcesadorApple)
            {
                compatibleApple = "compatible";
            }
            return base.ToString() + $" MacOS, {icloud} Icloud, {compatibleApple} con procesador Apple";
        }
        public static bool operator ==(MacOS unMac, MacOS otroMac)
        {
            return (unMac.Nombre == otroMac.Nombre && unMac.Version == otroMac.Version);
        }
        public static bool operator !=(MacOS unMac, MacOS otroMac)
        {
            return !(unMac == otroMac);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            else
            {
                return (this == (MacOS)obj);
            }
        }

        public static explicit operator String(MacOS mac)
        {
            return mac.DevolverInformacionEspecifica();
        }
    }
}
