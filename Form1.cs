using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe_Oyunu
{
    public partial class Form1 : Form
    {
        Timer pcZaman; 
        List<Button> butonlar;
        Random random = new Random();   //Randomu oluşturmamızın nedeni : 9 tane butondan herhangi birine tıkladık ve kaldı 8 tane. Bu 8 tanesinden birine de karşı tarafın rastgele tıklaması lazım.
        public enum oyuncular { X, O }   //enum sıralamayla alakalı
        oyuncular oyuncuX;
        int oyuncu = 0;
        int bilgisayar = 0;
        public Form1()
        {
            InitializeComponent();
            butonlar = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            pcZaman = new Timer(); 
            pcZaman.Interval = 1000; // 1 saniye 
            pcZaman.Tick += OyuncuSure;
            foreach (Button button in butonlar)
            {
                button.Click += oyuncuTikla; 
            }

            yenile();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
         

        }

        private void OyuncuSure(object sender, EventArgs e)
        {
            if (butonlar.Count > 0)  // Count : Toplam adedini belirten bir değer.
            {
                int index = random.Next(butonlar.Count);
                butonlar[index].Enabled = false;
                oyuncuX = oyuncular.O;    // Ben tıkladığım zaman oyuncuX X anlamına geliyor.Bilgisayar tıkladığı zaman oyuncuX O anlamına geliyor.
                butonlar[index].Text = oyuncuX.ToString();
                butonlar[index].BackColor = Color.Coral;
                butonlar.RemoveAt(index);   // Burada listeden siliyor.
                pcZaman.Stop();   // Durduruyoruz çünkü sırra karşı tarafa geçicek.
                oyunKontrol();
            }
        }

        private void yenileButon(object sender, EventArgs e)
        {
            yenile();
        }

        private void oyuncuTikla(object sender, EventArgs e)
        {
            var button = (Button)sender;   // Burada button nesnesinden bir kimlik aldık.
            oyuncuX = oyuncular.X;         // Oyuncunun ne olduğuna karar verdik. Oyuncu : X 
            button.Text = oyuncuX.ToString();  // Burada button'un text'ini değiştirdik.
            button.Enabled = false;    // Burada yaptığımız şey, sen oynadıktan sonra dur sıra bana geçsin anlamında. 
            button.BackColor = Color.BlueViolet;
            butonlar.Remove(button);  // Burada tıkladığımız butonu listeden çıkardık.Çünkü sıradaki oynayan kişi tekrar benim tıkladığıma tıklamasın diye.
            oyunKontrol();
            pcZaman.Start();  // Bizden sonra bilgisayarın oyuna başlayabilmesi için start dedik.
        }

        private void yenile()
        {
            butonlar = new List<Button> {button1 , button2 , button3 , button4 , button5 , button6 , button7 , button8 , button9 };

            foreach (Button x in butonlar)
            {
                x.Enabled = true;
                x.Text = "";
                x.BackColor = DefaultBackColor;
            }
        }

        private void oyunKontrol()
        {
            if(button1.Text == "X" && button2.Text == "X" && button3.Text == "X" || button4.Text == "X" && button5.Text == "X" && button6.Text == "X" || 
               button7.Text == "X" && button8.Text == "X" && button9.Text == "X" || button1.Text == "X" && button4.Text == "X" && button7.Text == "X" ||
               button2.Text == "X" && button5.Text == "X" && button8.Text == "X" || button3.Text == "X" && button6.Text == "X" && button9.Text == "X" ||
               button1.Text == "X" && button5.Text == "X" && button9.Text == "X" || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                pcZaman.Stop();
                MessageBox.Show("Kazanan Oyuncu!");
                oyuncu++;
                label1.Text = "Oyuncu : " + oyuncu;
                yenile();
            }
            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O" || button4.Text == "O" && button5.Text == "0" && button6.Text == "0" ||
                     button7.Text == "O" && button8.Text == "O" && button9.Text == "O" || button1.Text == "O" && button4.Text == "O" && button7.Text == "O" ||
                     button2.Text == "O" && button5.Text == "O" && button8.Text == "O" || button3.Text == "O" && button6.Text == "O" && button9.Text == "O" ||
                     button1.Text == "O" && button5.Text == "O" && button9.Text == "O" || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                    pcZaman.Stop();
                    MessageBox.Show("Kazanan Bilgisayar!");
                    bilgisayar++;
                    label2.Text = "Bilgisayar : " + bilgisayar;
                    yenile(); 
            }
            else if (butonlar.Count == 0)
            {
                pcZaman.Stop();
                MessageBox.Show("Berabere!");
                yenile();
            }
        }
    }
}
