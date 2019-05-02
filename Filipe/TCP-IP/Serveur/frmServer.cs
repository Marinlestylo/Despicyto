using Client;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serveur
{
    public partial class Server : Form
    {

        ClientApp _client = new ClientApp();

        public Server()
        {
            InitializeComponent();          
        }

        
        public SimpleTcpServer server; // Tcp Serveur

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer(); // instanciation du serveur
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;
            server.GetListeningIPs();
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                string myMessage = e.MessageString.Substring(0, e.MessageString.Length - 1); // récupère la string sauf le dernier caractère
                //txtStatus.Text += myMessage + Environment.NewLine; // display du message               
                txtStatus.Text +=  Environment.NewLine;
                server.Broadcast(myMessage);
            });
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server starting... " + Environment.NewLine;
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text); // récupère l'ip
            Debug.WriteLine(ip);
            server.Start(ip, Convert.ToInt32(txtPort.Text)); // le serveur se lance avec l'ip et le port
            txtStatus.Text += "Connected to the server." + Environment.NewLine;
            btnStart.Enabled = false;
        }

        public void BtnStop_Click(object sender, EventArgs e)
        {
            
            if (server.IsStarted)
            {
                server.Broadcast("Server has been shutdown");
                server.Stop(); // arrete le serveur si il est lancé
                txtStatus.Text += "Server Stopped" + Environment.NewLine;               
                btnStart.Enabled = true;
            }

        }
    }
}
