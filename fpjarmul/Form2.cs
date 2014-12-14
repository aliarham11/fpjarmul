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
        Image temp;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button3.Enabled = false;
            ipaddress = IPAddress.Parse("127.0.0.1");
            serverSocket = new TcpListener(ipaddress, 5118);
            clientSocket = default(TcpClient);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = incomingData.stegoImage;
            stegoLength = incomingData.stegoLength;
            SecretData data = new SecretData();
            data = Steganography.ExtractSecretData(incomingData.stegoImage, incomingData.stegoLength);

            if (data.SecretText != null)
                MessageBox.Show(data.SecretText);
            else
                pictureBox2.Image = data.SecretImage;

            serverSocket.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            incomingData = null;
            serverSocket.Start();
            Console.WriteLine("Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Accept connection from client");
            BinaryFormatter bformatter = new BinaryFormatter();
            NetworkStream networkStream = clientSocket.GetStream();
            incomingData = (PacketData)bformatter.Deserialize(networkStream);
            Console.WriteLine(incomingData.stegoLength);
            SecretData tempSecretDt = new SecretData();
            tempSecretDt.SecretImage = incomingData.secretImage;
            tempSecretDt.SecretText = incomingData.secretText;

            byte[] secretByte = Converter.secretDataToByte(tempSecretDt);
            Console.WriteLine(secretByte.Length);
            ElementRGB carrier = Converter.imageToElementRGB(incomingData.realImage);
            temp = Steganography.CreateStegoImage(carrier, secretByte, (Bitmap)incomingData.realImage);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = false;
            serverSocket.Stop();
        }

    }
}
