using System;
using System.Windows.Forms;

namespace Proyecto_Grafos
{
    public partial class Form2 : Form
    {        
        GrafosClass grafos = new GrafosClass();
        RutasResultado rutasResultado = new RutasResultado();

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = rutasResultado.GetRutaDijkstra();
            label2.Text = rutasResultado.GetRutaFloyd();
            label3.Text = rutasResultado.GetRutaWarshall();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}