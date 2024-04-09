using IniParser;
using IniParser.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace IPRangeCheckWinForms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }
        // Да-да...Знаю, что здесь можно применить паттерн "Медиатор" и "Команда" - не использовал из-за маленьких сроков
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string appPath = textBox1.Text;
                string appConf = textBox2.Text;
                string configConf = textBox3.Text;

                string appJson = $@"{Directory.GetCurrentDirectory()}\appsettings.json";
                string congIni = $@"{Directory.GetCurrentDirectory()}\config.ini";

                ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = appPath, Arguments = Extensions.GetDataGridViewArgimentCommandLine(dataGridView1) };


                if (radioButton1.Checked && appConf.Length > 0)
                    File.Copy(appConf, appJson, overwrite: true);
                else if (File.Exists(appJson))
                    File.Delete(appJson);


                if (radioButton2.Checked && configConf.Length > 0)
                    File.Copy(configConf, congIni, overwrite: true);
                else if (File.Exists(congIni))
                    File.Delete(congIni);

                if (radioButton3.Checked)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        string key = row.Cells[0].Value?.ToString();
                        string value = row.Cells[1].Value?.ToString();
                        if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                        {
                            startInfo.Environment[key] = value;
                        }
                    }
                }
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception)
            {

                MessageBox.Show("Неверные входные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                    dataGridView1.Enabled = true;
                    button1.Enabled = true;
                }
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "Конфигурационный файл (*.json)|*.json";
                openFileDialog1.Title = "Выберите конфигурационный фай";

                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;
                    textBox2.Text = filePath;
                    string jsonContent = File.ReadAllText(filePath);
                    try
                    {
                        JObject.Parse(jsonContent);
                        pictureBox1.Image = Properties.Resources.green_cross;
                        toolTip1.SetToolTip(pictureBox1, "Файл соответсвует правилам оформления JSON.");
                    }
                    catch (JsonReaderException ex)
                    {
                        pictureBox1.Image = Properties.Resources.red_cross;

                        toolTip1.SetToolTip(pictureBox1, "Файл не соответсвует правилам оформления JSON.");
                    }
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
                    DisableComponent(textBox3);
                    DisableComponent(dataGridView2);
                    DisableComponent(pictureBox2);

                    EnableComponent(pictureBox1);
                    EnableComponent(textBox2);
                }
                else if (radioButton == radioButton2)
                {
                    DisableComponent(textBox2);
                    DisableComponent(dataGridView2);
                    DisableComponent(pictureBox1);

                    EnableComponent(pictureBox2);
                    EnableComponent(textBox3);

                }
                else if (radioButton == radioButton3)
                {
                    DisableComponent(textBox2);
                    DisableComponent(textBox3);
                    DisableComponent(pictureBox1);
                    DisableComponent(pictureBox2);

                    EnableComponent(dataGridView2);
                }
            }
        }
        private void DisableComponent(Control control)
        {
            if (control is DataGridView dataGridView)
                dataGridView.Rows.Clear();
            if (control is TextBox textBox)
                textBox.Clear();
            if (control is PictureBox pictureBox)
                pictureBox.Image = null;

            control.Enabled = false;
        }
        private void EnableComponent(Control control)
        {
            control.Enabled = true;
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "Конфигурационный файл (*.ini)|*.ini";
                openFileDialog1.Title = "Выберите конфигурационный фай";

                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;
                    textBox3.Text = filePath;
                    FileIniDataParser iniParser = new FileIniDataParser();
                    try
                    {
                        iniParser.ReadFile(filePath);
                        pictureBox2.Image = Properties.Resources.green_cross;
                        toolTip1.SetToolTip(pictureBox2, "Файл соответсвует правилам оформления INI.");
                    }
                    catch (ParsingException ex)
                    {
                        pictureBox2.Image = Properties.Resources.red_cross;

                        toolTip1.SetToolTip(pictureBox2, "Файл не соответсвует правилам оформления INI.");
                    }

                }
            }
        }
    }
}
