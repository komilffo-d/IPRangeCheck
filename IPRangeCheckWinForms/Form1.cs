using System.Diagnostics;
using System.Windows.Forms;

namespace IPRangeCheckWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "Консольное приложение (*.exe)|*.exe";
                openFileDialog1.Title = "Выберите консольное приложение";

                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {

                    textBox1.Text = openFileDialog1.FileName;
                }
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "Конфигурационный файл (appsettings.json)|appsettings.json";
                openFileDialog1.Title = "Выберите конфигурационный фай";

                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {

                    textBox2.Text = openFileDialog1.FileName;
                }
            }
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                if (radioButton == radioButton1)
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Enabled = false;
                }
                else if (radioButton == radioButton2)
                {
                    textBox2.Clear();
                    dataGridView2.Enabled = true;
                }
            }
        }
    }
}
