using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fpjarmul
{
    public partial class Form2 : Form
    {
        int stegoLength = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Tambahi di sini masalah socketing
            //pictureBox1 image diperoleh dari buffer socket kemudian ditampilkan ke pictureBox
            //data yang diterima dari socket berupa objek Image dan integer stegoLength
            //update nilai pictureBox1.Image dengan buffer image dari socket
            //update nilai stegoLength pada form ini (awalnya 0) dengan stegoLength dari socket

            SecretData data = new SecretData();
            data = Steganography.ExtractSecretData(pictureBox1.Image, stegoLength);

            if (data.SecretText != null)
                MessageBox.Show(data.SecretText);
            else
                pictureBox2.Image = data.SecretImage;
        }

        




    }
}
