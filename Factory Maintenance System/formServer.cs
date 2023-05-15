using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Maintenance_system
{
    public partial class formServer : Form
    {
        public formServer()
        {
            InitializeComponent();
        }
        TcpListener server;
        int Port_Number = 1337;

        public string GetIP()
        {
            string name = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(name);
            IPAddress[] addr = entry.AddressList;
            if (addr[1].ToString().Split('.').Length == 4)
            {
                return addr[1].ToString();
            }
            return addr[2].ToString();
        }

        private void formServer_Load(object sender, EventArgs e)
        {
            server = new TcpListener(IPAddress.Parse(GetIP()), Port_Number);
            ServerStatus.Text = "Server inactive...";
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            SaveFileDialog saveFile = new SaveFileDialog();

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (serverText.Text == "")
                    return;

                addressBox.Text = saveFile.FileName;
                File.WriteAllText(saveFile.FileName, serverText.Text);
                serverText.Text = "";
            }

            addressBox.Text = "";
        }

        private void btnStop_Click_1(object sender, EventArgs e)
        {
            if (serverText.Text != null)
            {
                server.Stop();
                // btnStart.Enabled = true;
                ServerStatus.Text = "Server inactive...";
                addressBox.Text = "";
                serverText.Text = "";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                ServerStatus.Text = "Server starting...";

                server.Start();
                btnStart.Enabled = false;


                byte[] receive_data = new byte[1000];
                byte[] send_data = new byte[100];

                string data = null;

                TcpClient client = server.AcceptTcpClient();
                Connection.Text += "\r\nConnection from " + client.Client.RemoteEndPoint;

                NetworkStream stream = client.GetStream();
                int databyte = 1;

                if (!File.Exists(@".\FormlueKB.py"))
                {
                    using (StreamWriter sw = File.CreateText(@".\FormlueKB.py")) ;
                }

                StreamWriter writer = new StreamWriter(@".\FormlueKB.py", false);

                while (databyte != 0)
                {
                    databyte = stream.Read(receive_data, 0, receive_data.Length);

                    if (databyte == 0)
                        break;


                    data = System.Text.Encoding.ASCII.GetString(receive_data, 0, databyte);
                    writer.BaseStream.Write(receive_data, 0, receive_data.Length);
                    serverText.Text += data;


                    send_data = System.Text.Encoding.ASCII.GetBytes(data);
                    client.Client.Send(send_data); //this works on the phone but not on the client server I programmed
                }

                client.Close();

                server.Stop();
                ServerStatus.Text = "Server inactive...";
                Connection.Text = "";
                btnStart.Enabled = true;

            }
            catch (Exception h)
            {
                MessageBox.Show("Please, check your connection \n" + h.Message);
            }
        }
    }
    
}
