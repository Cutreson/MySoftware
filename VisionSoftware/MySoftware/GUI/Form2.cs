using MySoftware.CSV;
using System;
using WeifenLuo.WinFormsUI.Docking;

namespace MySoftware.GUI
{
    public partial class Form2 : DockContent
    {
        private WriterCSV writerCSV;
        public Form2()
        {
            InitializeComponent();
            writerCSV = new WriterCSV();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            DataCSV.dataWrite[0] = textBox1.Text;
            DataCSV.dataWrite[1] = textBox2.Text;
            DataCSV.dataWrite[2] = textBox3.Text;
            Console.WriteLine(string.Format("{0},{1},{2}", DataCSV.dataWrite[0], DataCSV.dataWrite[1], DataCSV.dataWrite[2]));
            writerCSV.WriteFileCSV();
        }
    }
}
