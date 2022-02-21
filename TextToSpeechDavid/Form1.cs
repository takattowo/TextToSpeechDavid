using System.Speech.AudioFormat;
using System.Speech.Synthesis;

namespace TextToSpeechDavid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private void speech_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
                return;

            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SpeakAsync(textBox.Text);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                speech.Enabled = false;
                save.Enabled = false;
            }
            else
            {
                speech.Enabled = true;
                save.Enabled = true;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
                return;

            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    panel1.BringToFront();
                    panel1.Visible = true;
                    synthesizer.SetOutputToWaveFile(@openFolderDialog.FileName, new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));
                    synthesizer.Speak(textBox.Text);

                    panel1.SendToBack();
                    panel1.Visible = false;
                }
                catch
                {
                    panel1.SendToBack();
                    panel1.Visible = false;
                }
                
            }
        }
    }
}