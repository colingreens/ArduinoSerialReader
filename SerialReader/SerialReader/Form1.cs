using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialReader
{
    public partial class Form1 : Form
    {
        SerialPort sp = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        
        
        public Form1()
        {
            InitializeComponent();
            //Open the serial port           
            sp.Open();            
        }

        private void ProgramStart()
        {
            
            //Set the datareceived event handler
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            
        }

        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Write the serial port data to the console.
            var consoleRead = sp.ReadExisting();
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//TentData.csv", consoleRead );
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//TentData.csv", DateTime.Now.ToString() + ",");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ProgramStart();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 300000;
            timer1.Enabled = true;
            timer1.Start();
            

        }

    }
}
