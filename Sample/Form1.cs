using TNT.LiveData;

namespace Sample;

public partial class Form1 : Form
{
  LiveData<string> textLiveData1 = new LiveData<string>();
  LiveData<string> textLiveData2 = null;
  LiveData<string> textLiveData3 = null;
  LiveData<string> switchMapLive = new LiveData<string>();

  public Form1()
  {
    InitializeComponent();

    textBox1.Text = "initial text";
    textBox6.Text = "switchmap inti";
    textLiveData1.Value = textBox1.Text;

    textLiveData2 = textLiveData1.Map<string, string>((text) =>
    {
      return string.Concat("Mapped: ", text);
    });

    textLiveData3 = textLiveData1.SwitchMap<string, string>((text) =>
       {
         switchMapLive.Value += text;
         return switchMapLive;
       });

    textLiveData1.Observe(t => textBox2.Text = t);
    textLiveData1.Observe(t => textBox4.Text = t);
    textLiveData2.Observe(t => textBox3.Text = t);
    textLiveData3.Observe(t => textBox5.Text = t);
  }

  private void textBox1_TextChanged(object sender, EventArgs e)
  {
    var textBox = sender as TextBox;
    textLiveData1.Value = textBox.Text;
  }

  private void textBox6_TextChanged(object sender, EventArgs e)
  {
    switchMapLive.Value = (sender as TextBox)?.Text;
  }
}
