using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Server=DESKTOP-58N9F6C;Database=bilgi;Trusted_Connection=True;";
        // yukarýdaki satýrdaki deðerleri kendi veritabaný bilgilerinizle deðiþtirin.

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // önce veritabanýndan tüm verileri çekelim
            DataTable siniflar = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM siniflar "; // TabloAdi yerine gerçek tablo adýný yazýn
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(siniflar);
            }


            // sonra verileri sýnýf numarasýna göre gruplayarak her sýnýfýn not ortalamasýný hesaplayalým
            var result = from row in siniflar.AsEnumerable()
                         group row by row.Field<int>("sinif_no") into g
                         select new
                         {
                             SinifNo = g.Key,
                             NotOrtalamasi = g.Average(row => row.Field<int>("not"))
                         };

            // son olarak hesaplanan not ortalamalarýný bir liste içinde saklayalým
            List<string> notOrtalamalari = new List<string>();
            foreach (var item in result)
            {
                notOrtalamalari.Add($"Sýnýf {item.SinifNo}: {item.NotOrtalamasi}");
            }

            // ve listbox'a yazdýralým
            listBox1.Items.AddRange(notOrtalamalari.ToArray());
        }

        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(156, 57);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.Size = new System.Drawing.Size(150, 104);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(710, 369);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private ListBox listBox1;

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
