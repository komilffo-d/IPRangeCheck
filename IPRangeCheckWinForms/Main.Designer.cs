namespace IPRangeCheckWinForms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            Key = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            textBox3 = new TextBox();
            dataGridView2 = new DataGridView();
            EnvKey = new DataGridViewTextBoxColumn();
            EnvValue = new DataGridViewTextBoxColumn();
            textBox2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new Point(32, 640);
            button1.Name = "button1";
            button1.Size = new Size(417, 23);
            button1.TabIndex = 0;
            button1.Text = "Запустить приложение";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(32, 39);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(435, 23);
            textBox1.TabIndex = 1;
            textBox1.Click += textBox1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Key, Value });
            dataGridView1.Enabled = false;
            dataGridView1.Location = new Point(32, 107);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(435, 160);
            dataGridView1.TabIndex = 2;
            // 
            // Key
            // 
            Key.HeaderText = "Параметр";
            Key.Name = "Key";
            // 
            // Value
            // 
            Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Value.HeaderText = "Значение";
            Value.Name = "Value";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(pictureBox2);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(dataGridView2);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Location = new Point(32, 308);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(435, 310);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(20, 186);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(138, 34);
            checkBox3.TabIndex = 10;
            checkBox3.Text = "Переменные среды \r\nпроцесса\r\n";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(16, 117);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(138, 34);
            checkBox2.TabIndex = 9;
            checkBox2.Text = "Конфигурационный\r\n файл config.ini";
            toolTip1.SetToolTip(checkBox2, "Файл скопируется в каталог приложения");
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(16, 38);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(142, 34);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Конфигурационный\r\n файл appsetting.json";
            toolTip1.SetToolTip(checkBox1, "Файл скопируется в каталог приложения");
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(404, 120);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(20, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(404, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(20, 20);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(176, 117);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(222, 23);
            textBox3.TabIndex = 5;
            textBox3.Click += textBox3_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { EnvKey, EnvValue });
            dataGridView2.Location = new Point(176, 185);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.Size = new Size(222, 94);
            dataGridView2.TabIndex = 3;
            // 
            // EnvKey
            // 
            EnvKey.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            EnvKey.HeaderText = "Параметр";
            EnvKey.Name = "EnvKey";
            // 
            // EnvValue
            // 
            EnvValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            EnvValue.HeaderText = "Значение";
            EnvValue.Name = "EnvValue";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(176, 35);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(222, 23);
            textBox2.TabIndex = 2;
            textBox2.Click += textBox2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 9);
            label1.Name = "label1";
            label1.Size = new Size(292, 15);
            label1.TabIndex = 4;
            label1.Text = "Этап 1. Выберите путь к консольному приложению";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 89);
            label2.Name = "label2";
            label2.Size = new Size(388, 15);
            label2.TabIndex = 5;
            label2.Text = "Этап 2. Назначьте параметры консоли в формате --parameter и value";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 290);
            label3.Name = "label3";
            label3.Size = new Size(366, 15);
            label3.TabIndex = 6;
            label3.Text = "Этап 3. Выберите и назначьте дополнительный источник данных";
            // 
            // toolTip1
            // 
            toolTip1.Tag = "";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(479, 722);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Main";
            Text = "Запуск консольного приложения";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private OpenFileDialog openFileDialog1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private GroupBox groupBox1;
        private TextBox textBox2;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn Key;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewTextBoxColumn EnvKey;
        private DataGridViewTextBoxColumn EnvValue;
        private Label label1;
        private Label label2;
        private Label label3;
        private ToolTip toolTip1;
        private TextBox textBox3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
    }
}
