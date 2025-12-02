using System;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_Grafos
{
    public partial class Form1 : Form
    {
        GrafosClass grafos = new GrafosClass();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            grafos.mapaIndices.Keys.ToList().ForEach(key =>
            {
               listDesde.Items.Add(key);
               listHasta.Items.Add(key);
            });
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            grafos.Dijkstra(listDesde.Text.Trim(), listHasta.Text.Trim());
            grafos.Warshall();
            grafos.Floyd();
            this.Hide();
            Form2 form = new Form2();
            form.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}