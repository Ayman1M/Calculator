using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private string basicOrder;

        private string Ans;
        public Form1()
        {
            InitializeComponent();
            basicOrder = string.Empty;
            BrowserName.Text = "\tHISTORY";
            BrowserName.BackColor = Color.Red;
            this.BackColor = Color.DarkSeaGreen;
        }
        private void output_TextChanged(object sender, EventArgs e)
        {

        }
      
        private void num_1_Click(object sender, EventArgs e)
        {
            output.Text += "1"; 
        }

        private void num_2_Click(object sender, EventArgs e)
        {
            output.Text += "2"; 
        }

        private void num_3_Click(object sender, EventArgs e)
        {
            output.Text += "3"; 
        }

        private void num_4_Click(object sender, EventArgs e)
        {
            output.Text += "4";
        }

        private void num_5_Click(object sender, EventArgs e)
        {
            output.Text += "5"; 
        }

        private void num_6_Click(object sender, EventArgs e)
        {
            output.Text += "6";
        }

        private void num_7_Click(object sender, EventArgs e)
        {
            output.Text += "7"; 
        }

        private void num_8_Click(object sender, EventArgs e)
        {
            output.Text += "8"; 
        }

        private void num_9_Click(object sender, EventArgs e)
        {
            output.Text += "9";      
        }

        private void num_0_Click(object sender, EventArgs e)
        {
             output.Text += "0";
        }

        private void point_Click(object sender, EventArgs e)
        {
              output.Text += ".";
        }

        private void Scape_last_Click(object sender, EventArgs e)
        {
            if (output.Text.Length == 0 || output.Text.Length == 1)
            {
                output.Text = "";
            }
            else if (output.Text.EndsWith("cos(") || output.Text.EndsWith("sin(") || output.Text.EndsWith("tan("))
            {
                output.Text = output.Text.Substring(0, output.TextLength - 4);
            }
            else if (output.Text.EndsWith("cos^-1(") || output.Text.EndsWith("sin^-1(") || output.Text.EndsWith("tan^-1("))
            {
                output.Text = output.Text.Substring(0, output.TextLength - 7);
            }
            else if (output.Text.EndsWith("log[") || output.Text.EndsWith("*10^"))
            {
                output.Text = output.Text.Substring(0, output.TextLength - 4);
            }
            else if (output.Text.EndsWith("ln["))
            {
                output.Text = output.Text.Substring(0, output.TextLength - 3);
            }
            else if(output.Text.EndsWith("∛(") || output.Text.EndsWith("√("))
            {
                output.Text = output.Text.Substring(0, output.TextLength - 2);
            }
            else if(output.Text.EndsWith(" + ") || output.Text.EndsWith(" - ") || output.Text.EndsWith(" % " )|| output.Text.EndsWith(" * ") || output.Text.EndsWith(" / "))
            {
                output.Text = output.Text.Substring(0, output.TextLength - 3);
            }
            else
            {
                output.Text = output.Text.Substring(0, output.TextLength - 1);
            }
        }
        private void delete_input_Click(object sender, EventArgs e)
        {
            output.Text = "";
        }

        private void plus_Click(object sender, EventArgs e)
        {
            output.Text += " + ";
        }

        private void negative_Click(object sender, EventArgs e)
        {
            output.Text += " - ";
        }

        private void Multipicate_Click(object sender, EventArgs e)
        {
            output.Text += " * ";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            output.Text += " / ";
        }

        private void module_Click(object sender, EventArgs e)
        {
            output.Text += " % ";
        }

        private void power_Click(object sender, EventArgs e)
        {
            output.Text += "^";
        }

        private void sine_Click(object sender, EventArgs e)
        {
            output.Text += "sin(";
        }

        private void cosine_Click(object sender, EventArgs e)
        {
            output.Text += "cos(";
        }

        private void tan__Click(object sender, EventArgs e)
        {
            output.Text += "tan(";
        }

        private void open_bracet_Click(object sender, EventArgs e)
        {
            output.Text += "(";
        }

        private void close_bracet_Click(object sender, EventArgs e)
        {
            output.Text += ")";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            output.Text += ",";
        }
        private void logarithm_Click(object sender, EventArgs e)
        {
            output.Text += "log[";
        }

        private void sqr_Click(object sender, EventArgs e)
        {

            output.Text += "√(";
        }
        private void root_3_Click(object sender, EventArgs e)
        {
            output.Text += "∛(";
        }
        private void natrual_logarithm_Click(object sender, EventArgs e)
        {

            output.Text += "ln[";
        }

        private void power_2_Click(object sender, EventArgs e)
        {

            output.Text += "^2";
        }

        private void power_3_Click(object sender, EventArgs e)
        {
            output.Text += "^3";
        }

        private void close_normal_bracet_Click(object sender, EventArgs e)
        {
            output.Text += "]";
        }

        private void inverse_tan_Click(object sender, EventArgs e)
        {
            output.Text += "tan^-1(";
        }

        private void inverse_cosine_Click(object sender, EventArgs e)
        {
            output.Text += "cos^-1(";
        }

        private void enverse_sine_Click(object sender, EventArgs e)
        {
            output.Text += "sin^-1(";
        }
        private void Pi__Click(object sender, EventArgs e)
        {
            output.Text += "π";
        }

        private void oilar_const_Click(object sender, EventArgs e)
        {
            output.Text += "e";
        }

        private void _10_power_x_Click(object sender, EventArgs e)
        {
            output.Text += "*10^";
        }
        private void Show_output_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(output.Text)) { return; }
            ExpressionValidator Testing = new ExpressionValidator(output.Text);

            if (!Testing.IsValid)
            {
                MessageBox.Show("bad math expression");
                return;
            }

            improveString helper = new improveString(output.Text);
            basicOrder = helper.Endimprove;
         //   MessageBox.Show(basicOrder);
            rewriteString endPoint = new rewriteString(basicOrder, Begree_Mode_.Checked);

            basicOrder = endPoint.pyCode;
           // MessageBox.Show(basicOrder);
            pythonMaker result = new pythonMaker(basicOrder);

            if(!result.goodresult_()) {MessageBox.Show(result.result_) ; return; }

             Ans = result.result_;
             webBrowser1.DocumentText += output.Text + " = " + result.result_ + "\n\n";
              
            return;
        }
        private void answer__Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Ans))
            {
                return;
            }
            output.Text += Ans;

        }
        private void Delete_history_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "";
            return;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void DarkMode_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
        }

        private void WhiteMode_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkSeaGreen;
        }
    }
}
