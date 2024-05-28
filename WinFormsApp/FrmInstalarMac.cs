﻿using Entidades;
using System.Globalization;

namespace WinFormsApp
{
    public partial class FrmInstalarMac : FrmInstalar
    {
        public CheckBox CheckIntegracionIcloud
        {
            get { return this.checkIntegracion; }
            set { this.checkIntegracion = value; }
        }

        public CheckBox CheckCompatibleApple
        {
            get { return this.checkCompatibleApple; }
            set { this.checkCompatibleApple = value; }
        }
        public FrmInstalarMac(List<SistemaOperativo> lista) : base(lista)
        {
            InitializeComponent();
        }

        protected override void btnInstalar_Click(object sender, EventArgs e)
        {
            string nombre = this.txtNombre.Text;
            string version = this.txtVersion.Text;
            double espacio;

            string txtEspacio = this.txtEspacio.Text.Replace(',', '.');
            bool validacion_espacio = double.TryParse(txtEspacio, NumberStyles.Any, CultureInfo.InvariantCulture, out espacio);
            bool compatible_Apple = this.checkCompatibleApple.Checked;
            EEstadoSoporte soporte = (EEstadoSoporte)this.cboEstado.SelectedItem;
            bool integracionIcloud = this.checkIntegracion.Checked;
            if (!validacion_espacio)
            {
                MessageBox.Show("Error al ingresar la cantidad de GB de espacio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (version == "")
            {
                MessageBox.Show("No ingresó nada en el campo Version.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (nombre == "")
            {
                MessageBox.Show("No ingresó nada en el campo Nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if(compatible_Apple == false && integracionIcloud == false)
                {
                    MacOS mac = new MacOS(nombre, version, espacio, soporte);
                    this.sistemaOperativo = mac;
                }
                else if(compatible_Apple == false)
                {
                    MacOS mac = new MacOS(nombre, version, espacio, soporte, integracionIcloud);
                    this.sistemaOperativo = mac;
                }
                else if(integracionIcloud == false)
                {
                    MacOS mac = new MacOS(nombre, version, espacio, compatible_Apple, soporte);
                    this.sistemaOperativo = mac;
                }
                else
                {
                    MacOS mac = new MacOS(nombre, version, espacio, compatible_Apple, soporte, integracionIcloud);
                    this.sistemaOperativo = mac;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
