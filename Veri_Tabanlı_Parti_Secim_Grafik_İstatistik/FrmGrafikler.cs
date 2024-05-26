using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Veri_Tabanlı_Parti_Secim_Grafik_İstatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=KAAN\SQLEXPRESS;Initial Catalog=DbSecimProje;Integrated Security=True;");
            baglanti.Open();

            //ilçe adlarını combobxa çekme
            SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0]);
            }
            baglanti.Close();


            //grafiğe toplam sonuçları getirme
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) FROM TBLILCE", baglanti);
            SqlDataReader sqlDataReader2 = komut2.ExecuteReader();
            while (sqlDataReader2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", sqlDataReader2[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", sqlDataReader2[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", sqlDataReader2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", sqlDataReader2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", sqlDataReader2[4]);
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=KAAN\SQLEXPRESS;Initial Catalog=DbSecimProje;Integrated Security=True;");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLILCE WHERE ILCEAD=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", comboBox1.Text);
            SqlDataReader sqlDataReader = komut.ExecuteReader();
            while (sqlDataReader.Read())
            {
                progressBar1.Value = int.Parse(sqlDataReader[2].ToString());
                progressBar2.Value = int.Parse(sqlDataReader[3].ToString());
                progressBar3.Value = int.Parse(sqlDataReader[4].ToString());
                progressBar4.Value = int.Parse(sqlDataReader[5].ToString());
                progressBar5.Value = int.Parse(sqlDataReader[6].ToString());

                lblA.Text = sqlDataReader[2].ToString();
                lblB.Text = sqlDataReader[3].ToString();
                lblC.Text = sqlDataReader[4].ToString();
                lblD.Text = sqlDataReader[5].ToString();
                lblE.Text = sqlDataReader[6].ToString();
            }
            baglanti.Close();
        }

        
    }
}
