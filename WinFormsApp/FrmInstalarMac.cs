﻿using Entidades;
using Entidades.Enumerados;
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

        public override void btnInstalar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validar_datos();
                string nombre = this.txtNombre.Text.Trim();
                if (String.IsNullOrWhiteSpace(nombre))
                {
                    throw new NoValidadoExcepcion("No ingresó nada en el campo Nombre.");
                }
                else
                {
                    string version = this.txtVersion.Text.Replace(" ", "");
                    double espacio = double.Parse(this.txtEspacio.Text.Replace(',', '.'), CultureInfo.InvariantCulture);// YA SE VALIDO EN base.validar_datos();
                    EEstadoSoporte soporte = (EEstadoSoporte)this.cboEstado.SelectedItem;

                    bool compatible_Apple = this.checkCompatibleApple.Checked;
                    bool integracionIcloud = this.checkIntegracion.Checked;

                    if (compatible_Apple == false && integracionIcloud == false)
                    {
                        MacOS mac = new MacOS(nombre, version, espacio, soporte);
                        this.sistemaOperativo = mac;
                    }
                    else if (compatible_Apple == false)
                    {
                        MacOS mac = new MacOS(nombre, version, espacio, soporte, integracionIcloud);
                        this.sistemaOperativo = mac;
                    }
                    else if (integracionIcloud == false)
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
            catch (NoValidadoExcepcion ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
