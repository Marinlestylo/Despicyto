using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Client
{
    public partial class ClientApp : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientApp()
        {
            InitializeComponent();
            btnSend.Enabled = false;
        }

        public string Nickname
        {
            get { return txtNickName.Text; }
        }

        SimpleTcpClient client; // Client

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {                
                client.Connect(txtHost.Text, Convert.ToInt32(txtPort.Text));
                btnSend.Enabled = true; // active le bouton
                btnConnect.Enabled = false; // desactive le bouton
            }
            catch (Exception ex)
            {
               txtStatus.Text = ex.Message;
               txtStatus.Text = "Il n'y a aucun serveur de disponible";
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient(); 
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                string nickName = txtNickName.Text;
                string myMessage = e.MessageString.Substring(0, e.MessageString.Length);
                txtStatus.Text += myMessage + Environment.NewLine;
                if(myMessage == "Server has been shutdown")
                {
                    btnConnect.Enabled = true;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Trim() == "")
            {

            }
            else
            {
                string name = (sender as Control).Name;
                name = txtNickName.Text;
                client.WriteLineAndGetReply(name + " a écrit: " + txtMessage.Text, TimeSpan.FromSeconds(0));
                txtMessage.Clear();
            }           
        }
    }
}
