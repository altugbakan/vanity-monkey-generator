using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace VanityMonKeyGenerator
{
    public partial class MonKeyForm : Form
    {
        public MonKeyForm()
        {
            InitializeComponent();
            MonKey m = new MonKey();
        }

        private async void GetMonKeyButton_Click(object sender, EventArgs e)
        {
            MonKey monKey = new MonKey();
            var result = await monKey.RequestMonKey();
            var doc = new XmlDocument();
            doc.LoadXml(result);
            var accessories = Accessories.MatchingAccessories(doc.OuterXml);
            LoadMonKeyImage(monKey.Address);
            addressTextBox.Text = monKey.Address;
            seedTextBox.Text = monKey.Seed;
            richTextBox1.Clear();
            foreach (string accessory in accessories)
            {
                richTextBox1.AppendText(accessory + "\n");
            }
        }

        private void LoadMonKeyImage(string address)
        {
            monKeyPictureBox.ImageLocation = "https://monkey.banano.cc/api/v1/monkey/" +
                address + "?format=png&size=125&background=true";
        }
    }
}
