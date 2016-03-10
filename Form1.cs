//---------------------------------------
// Author: Caio Luiz Gomes
// E-mail: caio.luiz1@gmail.com
//---------------------------------------
// A simple serial program,that receive 
// the date from the a sensor connected 
// in a microcontroller.
//---------------------------------------


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private SerialPort porta;
        string entrada;
        string temp;

        // delegate is used to write to a UI control from a non-UI thread
        private delegate void SetTextDeleg(string text);
        
        public Form1()
        {
            InitializeComponent();


            porta = new SerialPort("COM4", 9600);
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {    
            porta.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            porta.ReadTimeout = 500;
            porta.WriteTimeout = 500;
            porta.Open();
        }
        
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            entrada = porta.ReadLine();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { entrada });
        }

        private void si_DataReceived(string entrada)
        {

            string umid;
            if (entrada.StartsWith("T"))
            {
                temp = entrada.Substring(entrada.IndexOf("T") + 1, entrada.IndexOf("U") - 1);
                umid = entrada.Substring(entrada.IndexOf("U") + 1);
                //tB_temperatura.Text = temp.Trim();
                //tB_umi.Text = umid.Trim();
                textBox1.Text = temp.Trim();
            }

            //wr.WriteLine(data + " " + x);


        }
    }
}
