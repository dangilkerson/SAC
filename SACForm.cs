using System;
using System.Windows.Forms;
using SKYPE4COMLib;

namespace SAC
{
	public partial class SACForm : Form
	{
		private Skype _skype;

		public SACForm()
		{
			InitializeComponent();
		}

		private void SACForm_Load(object sender, EventArgs e)
		{
			_skype = new Skype();
			_skype.Attach();
			_skype.CallStatus += skype_CallStatus;
		}

		private void skype_CallStatus(Call call, TCallStatus Status)
		{
			switch (Status)
			{
				case TCallStatus.clsRinging:
					if (_skype.ActiveCalls.Count > 1)
					{
						foreach (Call activeCall in _skype.ActiveCalls)
						{
							call.Join(activeCall.Id);
							break;
						}
					}
					else
						call.Answer();
					break;

				default:
					break;
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SACForm_Shown(object sender, EventArgs e)
		{
			Hide();
		}
	}
}