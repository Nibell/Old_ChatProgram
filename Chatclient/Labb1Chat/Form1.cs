using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Labb1Chat
{
    public partial class Form1 : Form
    {
        private int c_iLastMessageId = 0;
        private int c_iLastMessageId1 = 0;

        wsChatService.ServiceSoapClient wsService = new wsChatService.ServiceSoapClient("ServiceSoap12");
        wsChatService1.ServiceSoapClient wsService1 = new wsChatService1.ServiceSoapClient("ServiceSoap121");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            /*sMessage = wsService.Test();
            MessageBox.Show(sMessage);

            Random rnd = new Random();
            Array arrNames = new Array[10];
            txtName.Text = arrNames[rnd.Next(10)];*/
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] arrMessages;
            string[] arrMessages1;
            int i;

            arrMessages = wsService.GetMessages(c_iLastMessageId);
            arrMessages1 = wsService1.GetMessages(c_iLastMessageId1);

            int difMessages = int.Parse(arrMessages[10]) - c_iLastMessageId;
            int difMessages1 = int.Parse(arrMessages1[10]) - c_iLastMessageId1;

            if (difMessages > 10)
            {
                difMessages = 10;
            }

            if (difMessages1 > 10)
            {
                difMessages1 = 10;
            }

            for (i = difMessages - 1; i >= 0; i--)
            {
                txtMessages.Text = arrMessages[i] + "\r\n" + txtMessages.Text;
            }

            for (i = difMessages1 - 1; i >= 0; i--)
            {
                txtMessages1.Text = arrMessages1[i] + "\r\n" + txtMessages1.Text;
            }

            c_iLastMessageId = int.Parse(arrMessages[10]);
            c_iLastMessageId1 = int.Parse(arrMessages1[10]);
        }

        private void ExamineKeypress(object sender, KeyPressEventArgs e)
        {
            int iKeyChar = e.KeyChar;
            if (iKeyChar == 13)
            {
                button1_Click(sender, e);
                button2_Click(sender, e);
                e.Handled = true;
                txtMessage.Text = "";
            }
            //MessageBox.Show(iKeyChar.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wsService1.SendMessage("babbel", txtName.Text, txtMessage.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wsService.SendMessage("babbel", txtName.Text, txtMessage.Text);
        }
    }
}
