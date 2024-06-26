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
        }
        // ��-��...����, ��� ����� ����� ��������� ������� "��������" � "�������" - �� ����������� ��-�� ��������� ������
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


                if (checkBox1.Checked && appConf != appJson && appConf.Length > 0)
                    File.Copy(appConf, appJson, overwrite: true);
                else if (!checkBox1.Checked && File.Exists(appJson))
                    File.Delete(appJson);

                if (checkBox2.Checked && configConf != congIni && configConf.Length > 0)
                    File.Copy(configConf, congIni, overwrite: true);
                else if (!checkBox2.Checked && File.Exists(congIni))
                    File.Delete(congIni);

                if (checkBox3.Checked)
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        string key = row.Cells[0].Value?.ToString();
                        string value = row.Cells[1].Value?.ToString();
                        if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                            startInfo.Environment[key] = value;
                    }
                }
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
            }
            catch (Exception)
            {

                MessageBox.Show("�������� ������� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "���������� ���������� (*.exe)|*.exe";
                openFileDialog1.Title = "�������� ���������� ����������";

                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {

                    textBox1.Text = openFileDialog1.FileName;
                    EnableComponent(dataGridView1);
                    EnableComponent(button1);
                }
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "���������������� ���� (appsettings.json)|appsettings.json";
                openFileDialog1.Title = "�������� ���������������� ���";

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
                        checkBox1.Checked = true;
                        toolTip1.SetToolTip(pictureBox1, "���� ������������ �������� ���������� JSON.");
                    }
                    catch (JsonReaderException ex)
                    {
                        pictureBox1.Image = Properties.Resources.red_cross;

                        toolTip1.SetToolTip(pictureBox1, "���� �� ������������ �������� ���������� JSON.");
                    }
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
                openFileDialog1.Filter = "���������������� ���� (config.ini)|config.ini";
                openFileDialog1.Title = "�������� ���������������� ���";

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
                        checkBox2.Checked = true;
                        toolTip1.SetToolTip(pictureBox2, "���� ������������ �������� ���������� INI.");
                    }
                    catch (ParsingException ex)
                    {
                        pictureBox2.Image = Properties.Resources.red_cross;

                        toolTip1.SetToolTip(pictureBox2, "���� �� ������������ �������� ���������� INI.");
                    }

                }
            }
        }
    }
}
