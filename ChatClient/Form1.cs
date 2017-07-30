using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient gonder = new TcpClient(txtIP.Text, Convert.ToInt32(txtPort.Text));
            NetworkStream okuryazar = gonder.GetStream();
            string mesaj = txtGiden.Text;
            byte[] mesaj_dizi = Encoding.ASCII.GetBytes(mesaj);
            byte[] gelen = new byte[gonder.ReceiveBufferSize];

            if (okuryazar.CanWrite)
            {
                okuryazar.Write(mesaj_dizi, 0, mesaj_dizi.Length);
            }
            else
            {
                gonder.Close();
                return;
            }
            if (okuryazar.CanRead)
            {
                okuryazar.Read(gelen, 0, (int)gonder.ReceiveBufferSize);
                string gelenveri = Encoding.ASCII.GetString(gelen);
                txtGelen.Text = Environment.NewLine + gelenveri;
            }
            else
            {
                gonder.Close();
                return;
            }
            
            txtGiden.Text = "";
            mesaj = "";
        }
    }
}
