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
        private SerialPort porta;  //Instantiate a Serial port
        string entrada;  //Input Variable
        string temp;

        // delegate is used to write to a UI control from a non-UI thread
        private delegate void SetTextDeleg(string text);
        
        public Form1()
        {
            InitializeComponent();


            porta = new SerialPort("COM4", 9600);   //Initialized the serial port "COM4"
        }

        // It is necessary introduce this Events in the Load Form
        private void Form1_Load(object sender, EventArgs e)
        {    
            porta.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            porta.ReadTimeout = 500;
            porta.WriteTimeout = 500;
            porta.Open();
        }
        
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);  //It should be the same is used in the microcontroller (delay)
            entrada = porta.ReadLine();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { entrada });
        }

        private void si_DataReceived(string entrada)
        {

            string umid;
            // It is being used string and the frame begin with letter "T"
            if (entrada.StartsWith("T"))    
            {
                temp = entrada.Substring(entrada.IndexOf("T") + 1, entrada.IndexOf("U") - 1);
                umid = entrada.Substring(entrada.IndexOf("U") + 1);
                textBox1.Text = temp.Trim();
            }

            //wr.WriteLine(data + " " + x);


        }
    }
}
