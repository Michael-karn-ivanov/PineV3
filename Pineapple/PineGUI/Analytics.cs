using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;

namespace PineGUI
{
    public partial class Analytics : Form
    {
        Dictionary<String, Dictionary<string, float>> _analytic = new Dictionary<string, Dictionary<string, float>>();

        public Analytics(List<Tuple<byte, byte, byte, byte, decimal, Dictionary<string, float>>> data)
        {
            InitializeComponent();
            data.Sort(delegate(Tuple<byte, byte, byte, byte, decimal, Dictionary<string, float>> t1,
                Tuple<byte, byte, byte, byte, decimal, Dictionary<string, float>> t2) 
                {
                    return t2.Item5.CompareTo(t1.Item5);
                });
            foreach (var item in data)
            {
                var element = String.Format("{0}: {1} -> {2}, {3} -> {4}",
                    item.Item5.ToString(), InputReader.WriteSingle(item.Item3),
                    InputReader.WriteLine(item.Item1), InputReader.WriteSingle(item.Item4),
                    InputReader.WriteLine(item.Item2));
                _analytic.Add(element, item.Item6);
                lbxData.Items.Add(element);
            }
        }

        private void lbxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbxStat.Items.Clear();
            var map = _analytic[lbxData.SelectedItem.ToString()];
            foreach (var key in map.Keys)
            {
                lbxStat.Items.Add(String.Format("{0} = {1}", key, map[key]));
            }
        }
    }
}
