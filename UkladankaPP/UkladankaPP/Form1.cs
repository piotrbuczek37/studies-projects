using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UkladankaPP
{
    public partial class Form1 : Form
    {
        private int ticks;

        private int punkty = 100000;

        private int klikniecia = 0;

        private Button[] tablicaPrzyciskow; 

        public Form1()
        {
            InitializeComponent();

            timer1.Start();

            losujCyfryNaPrzyciskach();           
        }
        
        private void losujCyfryNaPrzyciskach()
        {
            tablicaPrzyciskow = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8 };

            int liczbaNaPrzycisku;
            Random losowa = new Random();

            List<int> liczby = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            Button[] widocznePrzyciski = new Button[] { button1, button2, button4, button5, button6, button7, button8, button9 };

            foreach (Button przycisk in widocznePrzyciski)
            {
                liczbaNaPrzycisku = losowa.Next(0, liczby.Count);

                przycisk.Text = liczby[liczbaNaPrzycisku].ToString();

                liczby.RemoveAt(liczbaNaPrzycisku);
            }

            timer1.Start();

            punkty = 100000;
            label5.Text = punkty.ToString();
            klikniecia = 0;
            label7.Text = klikniecia.ToString();
            ticks = 0;
            label2.Text = ticks.ToString();

            foreach (Button przycisk in panel1.Controls)
            {
                if (przycisk.Visible == false)
                {
                    przycisk.Visible = true;
                }
            }

            button3.Visible = false;
        }          

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Visible == false)
            {
                button2.Visible = true;
                button2.Text = button1.Text;
                button1.Visible = false;
                button1.Text = "";
            }
            else if (button4.Visible == false)
            {
                button4.Visible = true;
                button4.Text = button1.Text;
                button1.Visible = false;
                button1.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Visible == false)
            {
                button1.Visible = true;
                button1.Text = button2.Text;
                button2.Visible = false;
                button2.Text = "";
            }
            else if (button3.Visible == false)
            {
                button3.Visible = true;
                button3.Text = button2.Text;
                button2.Visible = false;
                button2.Text = "";
            }
            else if (button5.Visible == false)
            {
                button5.Visible = true;
                button5.Text = button2.Text;
                button2.Visible = false;
                button2.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button2.Visible == false)
            {
                button2.Visible = true;
                button2.Text = button3.Text;
                button3.Visible = false;
                button3.Text = "";
            }
            else if (button6.Visible == false)
            {
                button6.Visible = true;
                button6.Text = button3.Text;
                button3.Visible = false;
                button3.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.Visible == false)
            {
                button1.Visible = true;
                button1.Text = button4.Text;
                button4.Visible = false;
                button4.Text = "";
            }
            else if (button5.Visible == false)
            {
                button5.Visible = true;
                button5.Text = button4.Text;
                button4.Visible = false;
                button4.Text = "";
            }
            else if (button7.Visible == false)
            {
                button7.Visible = true;
                button7.Text = button4.Text;
                button4.Visible = false;
                button4.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button2.Visible == false)
            {
                button2.Visible = true;
                button2.Text = button5.Text;
                button5.Visible = false;
                button5.Text = "";
            }
            else if (button4.Visible == false)
            {
                button4.Visible = true;
                button4.Text = button5.Text;
                button5.Visible = false;
                button5.Text = "";
            }
            else if (button6.Visible == false)
            {
                button6.Visible = true;
                button6.Text = button5.Text;
                button5.Visible = false;
                button5.Text = "";
            }
            else if (button8.Visible == false)
            {
                button8.Visible = true;
                button8.Text = button5.Text;
                button5.Visible = false;
                button5.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button3.Visible == false)
            {
                button3.Visible = true;
                button3.Text = button6.Text;
                button6.Visible = false;
                button6.Text = "";
            }
            else if (button5.Visible == false)
            {
                button5.Visible = true;
                button5.Text = button6.Text;
                button6.Visible = false;
                button6.Text = "";
            }
            else if (button9.Visible == false)
            {
                button9.Visible = true;
                button9.Text = button6.Text;
                button6.Visible = false;
                button6.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button4.Visible == false)
            {
                button4.Visible = true;
                button4.Text = button7.Text;
                button7.Visible = false;
                button7.Text = "";
            }
            else if (button8.Visible == false)
            {
                button8.Visible = true;
                button8.Text = button7.Text;
                button7.Visible = false;
                button7.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(button7.Visible==false)
            {
                button7.Visible = true;
                button7.Text = button8.Text;
                button8.Visible = false;
                button8.Text = "";
            }
            else if(button5.Visible == false)
            {
                button5.Visible = true;
                button5.Text = button8.Text;
                button8.Visible = false;
                button8.Text = "";
            }
            else if(button9.Visible == false)
            {
                button9.Visible = true;
                button9.Text = button8.Text;
                button8.Visible = false;
                button8.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button8.Visible == false)
            {
                button8.Visible = true;
                button8.Text = button9.Text;
                button9.Visible = false;
                button9.Text = "";
            }
            else if (button6.Visible == false)
            {
                button6.Visible = true;
                button6.Text = button9.Text;
                button9.Visible = false;
                button9.Text = "";
            }
            klikniecia++;
            label7.Text = klikniecia.ToString();
            punkty = punkty - 150;
            label5.Text = punkty.ToString();
            SprawdzCzyWygrana();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;
            label2.Text = ticks.ToString();            
            punkty = punkty - 100;
            label5.Text = punkty.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            losujCyfryNaPrzyciskach();

            
        }

        private void SprawdzCzyWygrana()
        {
            int sumaDobrejKolejnosci = 0;
            int iteracja = 0;
            for(int i=0;i<8;i++)
            {
                iteracja = i+1;
                if(tablicaPrzyciskow[i].Text == iteracja.ToString())
                {
                    sumaDobrejKolejnosci++;
                }
                else
                {
                    sumaDobrejKolejnosci = 0;
                    break;
                }                
            }
            if (sumaDobrejKolejnosci == 8)
            {
                timer1.Stop();
                MessageBox.Show("Brawo, wygrałeś!\n Twoja liczba punktów to: " +punkty+"!");
                losujCyfryNaPrzyciskach();
            }
        }
    }
}
