using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameServer.Forms.Controls
{
	public partial class ColorPicker : UserControl
	{
        public ColorPicker()
		{
			InitializeComponent();
		}

		public Color Color => BackColor;

		private void ColorPicker_BackColorChanged(object sender, EventArgs e)
		{
			if (BackColor != Color.Transparent &&
				BackColor.A == 0)
			{
				BackColor = Color.FromArgb(255, BackColor);
				return;
			}

			lblDescription.Text = BackColor.Name;

			Hide();

			this.Invalidate();

			Show();
		}

		private void lblDescription_Click(object sender, EventArgs e)
		{
			try
			{
				var cd = new ColorDialog()
				{
					AnyColor = true,
					Color = BackColor,
					FullOpen = true,
				};

				if (cd.ShowDialog() == DialogResult.OK)
				{
					BackColor = cd.Color;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
