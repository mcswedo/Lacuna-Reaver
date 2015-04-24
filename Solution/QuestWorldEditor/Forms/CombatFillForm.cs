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
    public partial class CombatFillForm : Form
    {
        public CombatFillForm()
        {
            InitializeComponent();

            sourceBox.Sorted = true;

            foreach (CombatZone zone in CombatZonePool.Singleton.Zones)
            {
                if (!sourceBox.Items.Contains(zone) && zone.ZoneName != "Empty")
                    sourceBox.Items.Add(zone);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (sourceBox.Items.Count < 0 || sourceBox.SelectedIndex < 0)
                return;

            destBox.Items.Add(sourceBox.SelectedItem);
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (destBox.Items.Count < 0 || destBox.SelectedIndex < 0)
                return;

            destBox.Items.Remove(destBox.SelectedItem);
        }
    }
}
