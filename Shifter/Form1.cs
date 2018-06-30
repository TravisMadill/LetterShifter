using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shifter
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			foreach (Control c in Controls)
			{
				if (c.Font.Equals(SystemFonts.DefaultFont))
					c.Font = SystemFonts.MessageBoxFont;
			}
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			richTextBox1_TextChanged(null, null);
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			int spot = richTextBox1.SelectionStart;
			richTextBox2.Text = letterShift(richTextBox1.Text, Convert.ToInt32(numericUpDown1.Value));
			richTextBox1.SelectionStart = spot;
			richTextBox1.SelectionLength = 0;
		}

		private void richTextBox2_TextChanged(object sender, EventArgs e)
		{
			int spot = richTextBox2.SelectionStart;
			richTextBox1.Text = letterShift(richTextBox2.Text, -Convert.ToInt32(numericUpDown1.Value));
			richTextBox2.SelectionStart = spot;
			richTextBox2.SelectionLength = 0;
		}

		private string letterShift(string input, int shift)
		{
			StringBuilder msg = new StringBuilder();
			if (input.Length > 0)
			{
				foreach (char c in input)
				{
					if (!(((c >= 'A') && (c <= 'Z')) || ((c >= 'a') && (c <= 'z')) || ((c >= '0') && (c <= '9')) || ((c >= 'À') && (c <= 'ß')) || ((c >= 'à') && (c <= 'ÿ'))))
						msg.Append(c);
					else
					{
						int ltr = c;
						if (c >= 'A' && c <= 'Z')
						{
							ltr -= 'A' - 'a';
							ltr -= 'a';
							ltr += shift + 26;
							ltr %= 26;
							ltr += 'a';
							ltr += 'A' - 'a';
						}
						else if (c >= 'a' && c <= 'z')
						{
							ltr -= 'a';
							ltr += shift + 26;
							ltr %= 26;
							ltr += 'a';
						}
						else if ((c >= 'À' && c <= 'ß') || (c >= 'à' && c <= 'ÿ'))
						{
							ltr -= 'À';
							ltr += shift + 64;
							ltr %= 64;
							ltr += 'À';
						}
						else if (c >= '0' && c <= '9')
						{
							ltr -= '0';
							ltr += shift + 10;
							ltr %= 10;
							ltr += '0';
						}

						msg.Append((char)ltr);
					}
				}
			}
			return msg.ToString();
		}
	}
}
