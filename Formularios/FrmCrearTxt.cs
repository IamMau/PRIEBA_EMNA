using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Formularios
{
    public partial class FrmCrearTxt : Form
    {
        Clases.DB db = new Clases.DB();
        private TextBox textBoxNombre;
        private TextBox textBoxPromedio;
        public FrmCrearTxt()
        {
            InitializeComponent();
        }

        private int contador = 0;

        private void BtnCrear_Click(object sender, EventArgs e)
        {
            contador++;

            if (contador % 2 == 1)
            {
                Label labelNombre = new Label();
                labelNombre.Text = "Nombre";
                labelNombre.Location = new Point(50, 30);

                textBoxNombre = new TextBox();
                textBoxNombre.Name = "TxtNombre";
                textBoxNombre.Location = new Point(50, 50);
                textBoxNombre.Size = new Size(100, 20);

                this.Controls.Add(labelNombre);
                this.Controls.Add(textBoxNombre);
            }
            else
            {
                Label labelPromedio = new Label();
                labelPromedio.Text = "Promedio";
                labelPromedio.Location = new Point(50, 80);

                textBoxPromedio = new TextBox();
                textBoxPromedio.Name = "TxtPromedio";
                textBoxPromedio.Location = new Point(50, 100);
                textBoxPromedio.Size = new Size(100, 20);

                this.Controls.Add(labelPromedio);
                this.Controls.Add(textBoxPromedio);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = textBoxNombre.Text;
            string promedio = textBoxPromedio.Text;

            string campos = "NOMBRE, PROMEDIO";
            string valores = $"'{nombre}', '{promedio}'";

            db.Save("ESTUDIANTES", campos, valores);

            MessageBox.Show("Se guardó con éxito en la base de datos");

            // Limpiar los campos
            textBoxNombre.Text = "";
            textBoxPromedio.Text = "";
        }

    }
}
