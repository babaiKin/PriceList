using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kent.Boogaart.KBCsv;

namespace WindowsFormsApplication1
{
	public partial class Form1:Form
	{
        
		public Form1()
		{
            //TopMost = true;
            InitializeComponent();
            label1.Text = "Прейскурант ННЦСМ 2017г.";
            this.KeyPreview = true;
            

        }

        DataSet dataSt = new DataSet();    // Будущий датасет для данных
        string pathCSV = Application.StartupPath + @"\file.csv";
        string pathLog = Application.StartupPath + @"\log.txt";

        private void Form1_Load(object sender, EventArgs e)
		{
            using (CsvReader reader = new CsvReader(pathCSV)) // Открыли файл КСВ
			{
				reader.ValueSeparator = Char.Parse(";");    // Обязательно указать, какой символ будет являться разделителем в КСВ-файле
				reader.ReadHeaderRecord();  // Этот метод надо вызвать, прежде чем что-либо делать с файлом КСВ. Он считывает заголовок
				reader.Fill(dataSt);    // Закинули в наш ДатаСет новую таблицу со всеми данными из файла
			}
            dataGridView1.Font = new Font("Arial", 20, GraphicsUnit.Pixel);

            dataGridView1.DataSource = dataSt.Tables[0];    // Заполнение датагрида данными
            
            // Свойства датагрида //
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Width = 50;
            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dataGridView1.Columns[2].Width = 50;
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.ReadOnly = true;
            // Запрет сортировки по столбцу (запрет нажатия на шапку столбца)
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Сортировка по первому столбцу
            this.dataGridView1.Sort(this.dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void dgView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            var strExpr = "[Наименование] LIKE '%%'";
            var dv = dataSt.Tables[0].DefaultView;
            dv.RowFilter = strExpr;
            var newDS = new DataSet();
            var newDT = dv.ToTable();
            newDS.Tables.Add(newDT);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var strExpr = "[Наименование] LIKE '%" + textBox1.Text + "%' OR [Тип СИ] LIKE '%" + textBox1.Text + "%' OR [Номер ГосРеестра] LIKE '%" + textBox1.Text + "%'";
            var dv = dataSt.Tables[0].DefaultView;
            dv.RowFilter = strExpr;
            var newDS = new DataSet();
            var newDT = dv.ToTable();
            newDS.Tables.Add(newDT);
            newDS = null;

            // создание log-файла и запись в него
            DateTime localDate = DateTime.Now;
            string fileName = pathLog; //пишем полный путь к файлу

            if (File.Exists(fileName) != true) //проверяем есть ли такой файл, если его нет, то создаем
            {  
                using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                {
                    sw.WriteLine("log-файл для просмотра активности посетителей");
                    sw.WriteLine("=============================================");
                    sw.WriteLine(localDate + " | " + textBox1.Text); //пишем строчку, или пишем что хотим
                }
            }
            else //если файл есть, то откываем его и пишем в него 
            {   
                using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Open, FileAccess.Write)))
                {
                    (sw.BaseStream).Seek(0, SeekOrigin.End); //идем в конец файла и пишем строку или пишем то, что хотим
                    sw.WriteLine(localDate + " | " + textBox1.Text);
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if ((e.Shift && e.Control) && e.KeyCode == Keys.P)
            {
                Form2 form = new Form2();
                form.ShowDialog();//в модальном режиме ,то что надо :)

                //MessageBox.Show("Тест");
                e.Handled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
