using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Um método que escreve números de um arquivo para um array...
        /// </summary>
        /// <param name="read">Fluxo de leitura do documento...</param>
        /// <returns>Matriz unidimensional...</returns>
        private double[] ConvertInFile(StreamReader read)
        {
            string s = null;
            List<double> mass = new List<double>();
            while ((s = read.ReadLine()) != null) mass.Add(Convert.ToDouble(s));
            return mass.ToArray();
        }

        /// <summary>
        /// Método para gravar uma matriz classificada em um arquivo...
        /// </summary>
        /// <param name="mass">Matriz ordenada...</param>
        private void CreateSortFile(double[] mass)
        {
            string date = DateTime.Now.ToString("F");
            date = date.Replace(":", "-");
            StreamWriter write = new StreamWriter(Directory.GetCurrentDirectory() + "\\" + date + ".txt");
            foreach (double digit in mass)
            {
                write.WriteLine(digit);
            }
            write.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool work = true;
            double[] mass=new double[0];
            try
            {
                mass = ConvertInFile(read);
            }
            catch
            {
                work = false;
                MessageBox.Show("O arquivo é necrótico");
            }

            if (mass.Length == 0) work = false;

            if (work)
            {
                Sort sort = new Sort(mass);
                double[] output = sort.Sorting().ToArray();
                CreateSortFile(output);
                MessageBox.Show("O array está ordenado, o arquivo finalizado está na pasta com o programa...");
            }
        }

        private StreamReader read;

        private void open_button_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Arquivo de texto|*.txt";
            openFileDialog1.Title = "Selecione um arquivo de texto para classificar...";
            textBox1.Clear();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                read = new StreamReader(openFileDialog1.FileName);
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
