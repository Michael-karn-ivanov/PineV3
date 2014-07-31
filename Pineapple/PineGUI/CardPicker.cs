using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PineGUI
{
    public partial class CardPicker : Form
    {
        public StringBuilder Result = new StringBuilder();
        HashSet<string> outs = new HashSet<String>();

        public CardPicker(params string[] lines)
        {
            InitializeComponent();

            foreach (var line in lines)
            {
                foreach (var c in line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!outs.Contains(c.ToLower()))
                        outs.Add(c.ToLower());
                }
            }

            foreach (var control in this.Controls)
            {
                var chb = control as CheckBox;
                if (chb != null && outs.Contains(chb.Text.ToLower()))
                {
                    chb.Checked = true;
                    chb.Enabled = false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void _2h_CheckedChanged(object sender, EventArgs e)
        {
            Result.Clear();
            foreach (var control in this.Controls)
            {
                var chb = control as CheckBox;
                if (chb != null && chb.Checked && !outs.Contains(chb.Text.ToLower()))
                {
                    Result.Append(chb.Text);
                    Result.Append(" ");
                }
            }
            lblResult.Text = Result.ToString();
        }
    }
}
