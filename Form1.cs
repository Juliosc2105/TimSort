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
        /// Метод записывающий числа из файла в массив...
        /// </summary>
        /// <param name="read">Поток чтения документа...</param>
        /// <returns>Одномерный массив...</returns>
        private double[] ConvertInFile(StreamReader read)
        {
            string s = null;
            List<double> mass = new List<double>();
            while ((s = read.ReadLine()) != null) mass.Add(Convert.ToDouble(s));
            return mass.ToArray();
        }

        /// <summary>
        /// Метод записи сортированного массива в файл...
        /// </summary>
        /// <param name="mass">Сортированный массив...</param>
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
                MessageBox.Show("Файл неккоректен");
            }

            if (mass.Length == 0) work = false;

            if (work)
            {
                Sort sort = new Sort(mass);
                double[] output = sort.Sorting().ToArray();
                CreateSortFile(output);
                MessageBox.Show("Массив отсортирован,готовый файл лежит в папке с программой...");
            }
        }

        private StreamReader read;

        private void open_button_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Текстовый файл|*.txt";
            openFileDialog1.Title = "Выберите текстовый файл для сортировки...";
            textBox1.Clear();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                read = new StreamReader(openFileDialog1.FileName);
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
