using System;
using System.Windows.Forms;
using TNT.LiveData;

namespace Test
{
	public partial class Form1 : Form
	{
		LiveData<string> textLiveData1 = new LiveData<string>();
		LiveData<string> textLiveData2 = null;

		public Form1()
		{
			InitializeComponent();
			textLiveData2 = textLiveData1.Map<string, string>((text) =>
			{
				return string.Concat("Transformed: ", text);
			});

			textLiveData1.Observe(t => textBox2.Text = t);
			textLiveData1.Observe(t => textBox4.Text = t);
			textLiveData2.Observe(t => textBox3.Text = t);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			var textBox = sender as TextBox;
			textLiveData1.Value = textBox.Text;
		}
	}
}
