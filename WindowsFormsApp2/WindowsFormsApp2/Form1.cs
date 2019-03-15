using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Thu vien nguoi dung
using System.IO;
using System.IO.Ports;
using System.Xml;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        SerialPort P = new SerialPort(); //Khai bao mot Object SerialPort moi
        string InputData = String.Empty; //Khai bao String buf dung cho luu du lieu sau nay
        delegate void SetTextCallback(string text); //Khai bao delegate SetTextCallback voi tham so String

        public Form1()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();  //Chua tat ca cac Port COM co tren may tinh

            //Them toan bo COM vao combobox
            comboBox1.Items.AddRange(ports); //Su dung addrange thay cho foreach
            P.ReadTimeout = 1000;

            //SerialDataReceivedEventHandler DataReceive = null;
            P.DataReceived += new SerialDataReceivedEventHandler(DataReceive);

            //Cai dat cho Baud Rate
            string[] BaudRate = {"1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200"};
            comboBox2.Items.AddRange(BaudRate);

            //Cai dat cho Databit
            string[] DataBits = { "6", "7", "8" };
            comboBox3.Items.AddRange(DataBits);

            //Cai dat cho Parity
            string[] Parity = { "None", "Odd", "Even" };
            comboBox4.Items.AddRange(Parity);

            //Cai dat cho Stop Bit
            string[] StopBit = { "1", "1.5", "2" };
            comboBox5.Items.AddRange(StopBit);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close(); //Neu dang mo Port thi phai dong lai
            }
            P.PortName = comboBox1.SelectedItem.ToString(); //gan PortName bang COM da chon
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            P.BaudRate = Convert.ToInt32(comboBox2.Text);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            P.DataBits = Convert.ToInt32(comboBox3.Text);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (P.IsOpen)
            {
                P.Close();
            }
            switch (comboBox4.SelectedItem.ToString())
            {
                case "Odd":
                    P.Parity = Parity.Odd;
                    break;
                case "None":
                    P.Parity = Parity.None;
                    break;
                case "Even":
                    P.Parity = Parity.Even;
                    break;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(P.IsOpen)
            {
                P.Close();
            }
            switch (comboBox5.SelectedItem.ToString())
            {
                case "1":
                    P.StopBits = StopBits.One;
                    break;
                case "1.5":
                    P.StopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    P.StopBits = StopBits.Two;
                    break;
            }
        }
        /*
         * Xây dựng các hàm thủ tục cho viec truyen va nhan du lieu
        */
        //Ham nay duoc su kien nhan du lieu goi toi, muc dich: hien thi du lieu
        private void DataReceive(object obj, SerialDataReceivedEventArgs e)
        {
            InputData = P.ReadExisting();
            if (InputData != String.Empty)
            {
                //Phai uy quyen tai day, goi delegate da khai bao truoc do
                SetText(InputData);
            }
        }
        private void SetText(string text)
        {
            if (this.textShow.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText); // khởi tạo1 delegate mới gọi đến SetText
                this.Invoke(d, new object[] { text });
            }
            else this.textShow.Text += text;
        }
    }
}
