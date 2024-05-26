using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; //veritabanı bağlantısı için kütüphane
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokKayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-B5OS695\\SQLEXPRESS; Initial Catalog=Stok; Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {//EKLE
            string t1 = textBox1.Text;  //malzeme kodu
            string t2 = textBox2.Text;  //malzeme adı
            string t3 = textBox3.Text;  //Yıllık satış
            string t4 = textBox4.Text;  //birim fiyat
            string t5 = textBox5.Text;  //minstok
            string t6 = textBox6.Text;  //tsuresi

            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Malzemeler (MalzemeKodu, MalzemeAdi, YillikSatis, BirimFiyat, MinStok, TSuresi) VALUES ('"+t1+"','"+t2+"','"+t3+"','"+t4+"','"+t5+"','"+t6+"')",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void listele() //veritabanındaki kayıtların güncellenmesi
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Malzemeler",baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {//SİL
            string t1 = textBox1.Text; //seçilen satırın malzeme kodunu al
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM Malzemeler WHERE MalzemeKodu='"+t1+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {//GÜNCELLE
            string t1 = textBox1.Text;  //malzeme kodu
            string t2 = textBox2.Text;  //malzeme adı
            string t3 = textBox3.Text;  //Yıllık satış
            string t4 = textBox4.Text;  //birim fiyat
            string t5 = textBox5.Text;  //minstok
            string t6 = textBox6.Text;  //tsuresi

            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE Malzemeler SET MalzemeKodu='"+t1+"', MalzemeAdi='"+t2+"', YillikSatis='"+t3+"', BirimFiyat='"+t4+"', MinStok='"+t5+"', TSuresi='"+t6+"' WHERE MalzemeKodu='"+t1+"'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
