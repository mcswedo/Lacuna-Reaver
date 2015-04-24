using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyQuest;

namespace QuestWorldEditor
{
    public partial class CreatePortalForm : Form
    {
        Portal portal;

        public CreatePortalForm()
        {
            InitializeComponent();
        }

        public Portal ShowDialog(Portal existingInstance, out DialogResult result)
        {
            if (existingInstance == null)
                portal = new Portal();
            else
                portal = Utility.Clone<Portal>(existingInstance);

            SeControlValues(portal);

            result = base.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                GetControlValues(ref portal);
                return portal;
            }

            return existingInstance;
        }

        void SeControlValues(Portal portal)
        {
            this.destinationName.Text = portal.DestinationMap;
            this.destinationX.Text = portal.DestinationPosition.X.ToString();
            this.destinationY.Text = portal.DestinationPosition.Y.ToString();
            this.soundEffect.Text = portal.SoundCueName ?? string.Empty;
            this.destinationDirection.SelectedIndex = portal.DestinationDirection.HasValue ? (int)portal.DestinationDirection / 2 + 1 : 0;
        }

        void GetControlValues(ref Portal portal)
        {
            portal.DestinationMap = this.destinationName.Text;
            portal.DestinationPosition = new Microsoft.Xna.Framework.Point(
                int.Parse(destinationX.Text),
                int.Parse(destinationY.Text));

            portal.SoundCueName = this.soundEffect.Text;

            if (destinationDirection.SelectedIndex == 0)
                portal.DestinationDirection = null;
            else
                portal.DestinationDirection = (Direction)((destinationDirection.SelectedIndex - 1) * 2);
        }
    }
}
