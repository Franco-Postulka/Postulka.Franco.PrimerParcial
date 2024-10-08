﻿using Entidades;
using Entidades.Enumerados;
using System.Globalization;

namespace WinFormsApp
{
    public partial class FrmInstalarLinux : FrmInstalar 
    {
        public CheckBox CheckInterfazGrafica
        {
            get { return this.checkInterfaz; }
            set { this.checkInterfaz = value; }
        }

        public ComboBox CboDistribucion
        {
            get { return this.cboDistribucion; }
            set { this.cboDistribucion = value; }
        }
        public FrmInstalarLinux(List<SistemaOperativo> sistema) : base(sistema)
        {
            InitializeComponent();
            foreach (EDistribucionLinux distribucion in Enum.GetValues(typeof(EDistribucionLinux)))
            {
                this.cboDistribucion.Items.Add(distribucion);
            }
            this.cboDistribucion.SelectedItem = EDistribucionLinux.Ubuntu;
            this.txtNombre.Text = "Linux";
        }

       
        public override void btnInstalar_Click(object sender, EventArgs e)
        {
            try
            {
                this.validar_datos();
                string nombre = this.txtNombre.Text.Replace(" ","");
                string version = this.txtVersion.Text.Replace(" ","");
                double espacio = double.Parse(this.txtEspacio.Text.Replace(',', '.'),CultureInfo.InvariantCulture);// YA SE VALIDO EN base.validar_datos();
                EEstadoSoporte soporte = (EEstadoSoporte)this.cboEstado.SelectedItem;

                EDistribucionLinux distribucionLinux = (EDistribucionLinux)this.cboDistribucion.SelectedItem;
                bool interfaz = this.checkInterfaz.Checked;

                if (String.IsNullOrWhiteSpace(nombre) && interfaz == false)
                {
                    Linux linux = new Linux(version, espacio, soporte, distribucionLinux);
                    this.sistemaOperativo = linux;
                }
                else if (String.IsNullOrWhiteSpace(nombre))
                {
                    Linux linux = new Linux(version, espacio, soporte, distribucionLinux, interfaz);
                    this.sistemaOperativo = linux;
                }
                else if (interfaz == false)
                {
                    Linux linux = new Linux(nombre, version, espacio, soporte, distribucionLinux);
                    this.sistemaOperativo = linux;
                }
                else
                {
                    Linux linux = new Linux(nombre, version, espacio, soporte, distribucionLinux, interfaz);
                    this.sistemaOperativo = linux;

                }
                this.DialogResult = DialogResult.OK;
                this.Close();
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
