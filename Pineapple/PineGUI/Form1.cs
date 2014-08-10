using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core;
using System.IO;

namespace PineGUI
{
    public partial class Form1 : Form
    {
        byte firstIndex;
        byte secondIndex;
        byte firstValue;
        byte secondValue;
        byte outValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            tbxVillain.Text = String.Empty;
            tbxVillain2.Text = String.Empty;
            tbxDeadCards.Text = String.Empty;
            tbxHeroShort.Text = String.Empty;
            tbxHeroMiddle.Text = String.Empty;
            tbxHeroTop.Text = String.Empty;
            tbxCurrentTriple.Text = String.Empty;
            lblBestPlace.Text = String.Empty;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                byte[] heroHand = InputReader.HeroHand().Short(tbxHeroShort.Text).Middle(tbxHeroMiddle.Text)
                    .Top(tbxHeroTop.Text).Hand;
                byte[] triple = InputReader.ReadInput(tbxCurrentTriple.Text);
                byte[] deck = InputReader.Deck().Remove(tbxHeroShort.Text)
                    .Remove(tbxHeroMiddle.Text)
                    .Remove(tbxHeroTop.Text)
                    .Remove(tbxVillain.Text.Replace(Environment.NewLine," "))
                    .Remove(tbxVillain2.Text.Replace(Environment.NewLine, " "))
                    .Remove(tbxCurrentTriple.Text)
                    .Remove(tbxDeadCards.Text.Replace(Environment.NewLine, " ")).Deck;

                int nonEmptyCount = 0;
                foreach (var b in heroHand)
                {
                    if (b != 0) nonEmptyCount++;
                }
                int sampleSize = nonEmptyCount == 7 ? Int32.Parse(tbx2ndSampleSize.Text)
                    : (nonEmptyCount == 9 ? Int32.Parse(tbx3rdSampleSize.Text) : 0);
                if (sampleSize == 0)
                {
                    MessageBox.Show("Не могу определить улицу (количество карт у Hero не 7 и не 9)");
                    return;
                }

                Predictor predictor = new Predictor();
                decimal score = predictor.FindBestPlaceForTriple(heroHand, deck, triple,
                    out firstIndex, out secondIndex, out firstValue, out secondValue,
                    sampleSize, false, new List<Tuple<byte, byte, byte, byte, decimal, Dictionary<string, float>>>());

                for (int i = 0; i < triple.Length; i++)
                {
                    if (firstValue != triple[i] && secondValue != triple[i])
                        outValue = triple[i];
                }

                btnApply.Enabled = true;

                lblBestPlace.Text = String.Format("{0}: {1} -> {2}, {3} -> {4}",
                    score.ToString(), InputReader.WriteSingle(firstValue),
                    InputReader.WriteLine(firstIndex), InputReader.WriteSingle(secondValue),
                    InputReader.WriteLine(secondIndex));
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            apply(firstIndex, firstValue);
            apply(secondIndex, secondValue);
            tbxDeadCards.Text += (" " + InputReader.WriteSingle(outValue));
            tbxCurrentTriple.Text = "";
        }

        private void apply(byte index, byte value)
        {
            if (index < 3) tbxHeroShort.Text += (" " + InputReader.WriteSingle(value));
            else if (index < 8) tbxHeroMiddle.Text += (" " + InputReader.WriteSingle(value));
            else if (index < 13) tbxHeroTop.Text += (" " + InputReader.WriteSingle(value));
        }

        private void tbxVillain_DoubleClick(object sender, EventArgs e)
        {
            ShowPicker(tbxVillain);
        }

        public void ShowPicker(TextBox tbx)
        {
            var picker = new CardPicker(tbxVillain.Text, tbxHeroShort.Text,
                tbxHeroMiddle.Text, tbxHeroTop.Text, tbxDeadCards.Text, tbxCurrentTriple.Text);
            DialogResult res = picker.ShowDialog();
            if (res == DialogResult.OK)
                tbx.Text += (" " + picker.Result.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxCurrentTriple.Text = "";
        }

        private void tbxCurrentTriple_DoubleClick(object sender, EventArgs e)
        {
            ShowPicker(tbxCurrentTriple);
        }

        private void tbxHeroShort_DoubleClick(object sender, EventArgs e)
        {
            ShowPicker(tbxHeroShort);
        }

        private void tbxHeroMiddle_DoubleClick(object sender, EventArgs e)
        {
            ShowPicker(tbxHeroMiddle);
        }

        private void tbxHeroTop_DoubleClick(object sender, EventArgs e)
        {
            ShowPicker(tbxHeroTop);
        }

        private void tbxDeadCards_DoubleClick(object sender, EventArgs e)
        {
            ShowPicker(tbxDeadCards);
        }

        private void tbxVillain2_DoubleClick(object sender, EventArgs e)
        {
            ShowPicker(tbxVillain2);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "hand files (*.hand)|*.hand";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = saveFileDialog.FileName;
                if (!fileName.EndsWith(".hand")) fileName += ".hand";
                using (TextWriter w = new System.IO.StreamWriter(fileName))
                {
                    w.WriteLine(tbxVillain.Text);
                    w.WriteLine(tbxVillain2.Text);
                    w.WriteLine(tbxCurrentTriple.Text);
                    w.WriteLine(tbxDeadCards.Text);
                    w.WriteLine(tbxHeroShort.Text);
                    w.WriteLine(tbxHeroMiddle.Text);
                    w.WriteLine(tbxHeroTop.Text);
                    w.Flush();
                    w.Close();
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "hand files (*.hand)|*.hand";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (TextReader r = new StreamReader(openFileDialog.FileName))
                {
                    tbxVillain.Text = r.ReadLine();
                    tbxVillain2.Text = r.ReadLine();
                    tbxCurrentTriple.Text = r.ReadLine();
                    tbxDeadCards.Text = r.ReadLine();
                    tbxHeroShort.Text = r.ReadLine();
                    tbxHeroMiddle.Text = r.ReadLine();
                    tbxHeroTop.Text = r.ReadLine();
                }
            }
        }

        private void btnGoWithAnalyze_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                byte[] heroHand = InputReader.HeroHand().Short(tbxHeroShort.Text).Middle(tbxHeroMiddle.Text)
                    .Top(tbxHeroTop.Text).Hand;
                byte[] triple = InputReader.ReadInput(tbxCurrentTriple.Text);
                byte[] deck = InputReader.Deck().Remove(tbxHeroShort.Text)
                    .Remove(tbxHeroMiddle.Text)
                    .Remove(tbxHeroTop.Text)
                    .Remove(tbxVillain.Text.Replace(Environment.NewLine, " "))
                    .Remove(tbxVillain2.Text.Replace(Environment.NewLine, " "))
                    .Remove(tbxCurrentTriple.Text)
                    .Remove(tbxDeadCards.Text.Replace(Environment.NewLine, " ")).Deck;

                int nonEmptyCount = 0;
                foreach (var b in heroHand)
                {
                    if (b != 0) nonEmptyCount++;
                }
                int sampleSize = nonEmptyCount == 7 ? Int32.Parse(tbx2ndSampleSize.Text)
                    : (nonEmptyCount == 9 ? Int32.Parse(tbx3rdSampleSize.Text) : 0);
                if (sampleSize == 0)
                {
                    MessageBox.Show("Не могу определить улицу (количество карт у Hero не 7 и не 9)");
                    return;
                }

                Predictor predictor = new Predictor();
                var data = new List<Tuple<byte, byte, byte, byte, decimal, Dictionary<string, float>>>();
                decimal score = predictor.FindBestPlaceForTriple(heroHand, deck, triple,
                    out firstIndex, out secondIndex, out firstValue, out secondValue,
                    sampleSize, true, data);

                for (int i = 0; i < triple.Length; i++)
                {
                    if (firstValue != triple[i] && secondValue != triple[i])
                        outValue = triple[i];
                }

                btnApply.Enabled = true;

                lblBestPlace.Text = String.Format("{0}: {1} -> {2}, {3} -> {4}",
                    score.ToString(), InputReader.WriteSingle(firstValue),
                    InputReader.WriteLine(firstIndex), InputReader.WriteSingle(secondValue),
                    InputReader.WriteLine(secondIndex));

                Analytics f = new Analytics(data);
                f.Show();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
