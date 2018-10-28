using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NRemoting;

using CalculatorCore;

namespace CalculatorServer
{
    public partial class Form1 : Form
    {
        private NSingletonWellKnownService<Calculator> service;

        private delegate void AppendTextDelegate(string text);

        public Form1()
        {
            InitializeComponent();

            Font f = textBox1.Font;
            textBox1.Font = new Font(FontFamily.GenericMonospace, f.Size);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                service = new NSingletonWellKnownService<Calculator>(12345);
                service.Start();

                NWellKnownClient<Calculator> client = new NWellKnownClient<Calculator>("127.0.0.1", 12345);
                Calculator calculator = client.GetClient();
                calculator.OnAdded      += OnAdded;
                calculator.OnSubtracted += OnSubtracted;

                textBox1.AppendText("started." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                service.Stop();
                service = null;

                textBox1.AppendText("stopped." + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnAdded(int x, int y, int z)
        {
            AppendText(x + " + " + y + " = " + z + Environment.NewLine);
        }

        private void OnSubtracted(int x, int y, int z)
        {
            AppendText(x + " - " + y + " = " + z + Environment.NewLine);
        }

        private void AppendText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new AppendTextDelegate(AppendText), text);
            }
            else
            {
                textBox1.AppendText(text);
            }
        }
    }
}
