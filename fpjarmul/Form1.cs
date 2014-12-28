using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace fpjarmul
{
    public partial class Form1 : Form
    {
        int stegoLength=0;
        int MAXFILESIZE = 0;
        TcpClient clientSocket;
        string msg;
        SecretData data;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            data = new SecretData();
            //pictureBox3.Image = null;
            //stegoLength = 18;
            
            //byte[] outStream = System.Text.Encoding.ASCII.GetBytes(msg);
            //serverStream.Write(outStream, 0, outStream.Length);
            //serverStream.Close();
            //serverStream.Flush();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "JPEG Image Files(*.jpg; *.jpeg;)|*.jpg; *.jpeg;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                pictureBox1.Image = new Bitmap(open.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                //pictureBox1.Location = new Point((pictureBox1.Parent.ClientSize.Width / 2) - (pictureBox1.Image.Width / 2), (pictureBox1.Parent.ClientSize.Height / 2) - (pictureBox1.Image.Height / 2));
                pictureBox1.Refresh();
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                MAXFILESIZE = (pictureBox1.Image.Width * pictureBox1.Image.Height * 3 / (8 * 1024));
                maxFileSize.Text = MAXFILESIZE.ToString() + " KB";
            } 
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                richTextBox1.Enabled = true;
                btnCreateStegoText.Enabled = true;
            }
            else
            {
                richTextBox1.Enabled = false;
                btnCreateStegoText.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                btnOpenSecretImage.Enabled = true;
            }
            else
            {
                btnOpenSecretImage.Enabled = false;
            }
        }

        private void btnOpenSecretImage_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "JPEG Image Files(*.jpg; *.jpeg;)|*.jpg; *.jpeg;";
            open.FileOk += delegate(object s, CancelEventArgs ev)
            {
                var size = new FileInfo(open.FileName).Length;
                if (size > MAXFILESIZE*1024)
                {
                    MessageBox.Show("Ukuran file melebihi batas maksimum!");
                    ev.Cancel = true;             // <== here
                }
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                pictureBox2.Image = new Bitmap(open.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                //pictureBox1.Location = new Point((pictureBox1.Parent.ClientSize.Width / 2) - (pictureBox1.Image.Width / 2), (pictureBox1.Parent.ClientSize.Height / 2) - (pictureBox1.Image.Height / 2));
                pictureBox2.Refresh();
                btnCreateStegoImage.Enabled = true;
            }
        }

        private void btnCreateStegoText_Click(object sender, EventArgs e)
        {
            //data = new SecretData();
            data.SecretText = richTextBox1.Text;

            byte[] secretByte = Converter.secretDataToByte(data);
            ElementRGB carrier = Converter.imageToElementRGB(pictureBox1.Image);
            Console.WriteLine(secretByte.Length);
            pictureBox3.Image = Steganography.CreateStegoImage(carrier, secretByte, (Bitmap)pictureBox1.Image);
            stegoLength = carrier.StegoLength;

            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Refresh();
            btnSendImage.Enabled = true;
            MessageBox.Show("Stego Image Created");
        }

        private void btnCreateStegoImage_Click(object sender, EventArgs e)
        {
            //data = new SecretData();
            data.SecretImage = new Bitmap(pictureBox2.Image);

            byte[] secretByte = Converter.secretDataToByte(data);
            ElementRGB carrier = Converter.imageToElementRGB(pictureBox1.Image);
            Console.WriteLine(secretByte.Length);
            pictureBox3.Image = Steganography.CreateStegoImage(carrier, secretByte, (Bitmap)pictureBox1.Image);
            stegoLength = carrier.StegoLength;

            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.Refresh();
            btnSendImage.Enabled = true;
            MessageBox.Show("Stego Image Created");
        }

        private void btnSendImage_Click(object sender, EventArgs e)
        {
            BinaryFormatter bformatter = new BinaryFormatter();
            PacketData packetData = new PacketData(pictureBox3.Image, stegoLength);
            packetData.setValue(pictureBox1.Image, pictureBox2.Image, data.SecretText);

            clientSocket = new TcpClient();
            Console.WriteLine("Client Started");
            clientSocket.Connect("127.0.0.1", 5118);
            NetworkStream serverStream = clientSocket.GetStream();
            bformatter.Serialize(serverStream, packetData);
            Console.WriteLine(pictureBox3.Image.Size);
            //SecretData data1 = new SecretData();

            //data1 = Steganography.ExtractSecretData(pictureBox3.Image, stegoLength);

            //if (data.SecretText != null)
            //    MessageBox.Show(data1.SecretText);



            //Tambahkan di sini socket programming untuk mengirim pesan ke receiver
            //data yang dikirim berupa objek Image dan integer stegoLength
            //Image diperoleh dari pictureBox3.Image
            //stegoLength diperoleh dari variabel global class ini stegoLength
            MessageBox.Show("Message Sent");
        }

       
    }
}
