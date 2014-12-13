using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fpjarmul
{
    public partial class Form2 : Form
    {
        PacketData incomingData;
        int stegoLength = 0;
        TcpListener serverSocket;
        TcpClient clientSocket;
        IPAddress ipaddress;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            ipaddress = IPAddress.Parse("10.151.43.68");
            serverSocket = new TcpListener(ipaddress, 5118);
            clientSocket = default(TcpClient);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Tambahi di sini masalah socketing
            //pictureBox1 image diperoleh dari buffer socket kemudian ditampilkan ke pictureBox
            //data yang diterima dari socket berupa objek Image dan integer stegoLength
            //update nilai pictureBox1.Image dengan buffer image dari socket
            //update nilai stegoLength pada form ini (awalnya 0) dengan stegoLength dari socket
            
            pictureBox1.Image = incomingData.stegoImage;
            stegoLength = incomingData.stegoLength;
            SecretData data = new SecretData();
            data = Steganography.ExtractSecretData(incomingData.stegoImage, incomingData.stegoLength);

            if (data.SecretText != null)
                MessageBox.Show(data.SecretText);
            else
                pictureBox2.Image = data.SecretImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            incomingData = null;
            serverSocket.Start();
            Console.WriteLine("Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Accept connection from client");
            BinaryFormatter bformatter = new BinaryFormatter();
            NetworkStream networkStream = clientSocket.GetStream();
            incomingData = (PacketData)bformatter.Deserialize(networkStream);
            Console.WriteLine(incomingData.stegoLength);
            Console.WriteLine(incomingData.stegoImage.Size);

            serverSocket.Stop();
            
        }

    }
}
