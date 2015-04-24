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
    public partial class CreateNPCForm : Form
    {
        NPCMapCharacter npc;

        public CreateNPCForm()
        {
            InitializeComponent();

            idleAnimationButton.Click += new EventHandler(idleAnimationButton_Click);
            walkingAnimationButton.Click += new EventHandler(walkingAnimationButton_Click);

            previewIdleAnimation.Click += new EventHandler(previewIdleAnimation_Click);
            previewWalkingAnimation.Click += new EventHandler(previewWalkingAnimation_Click);
        }

        public NPCMapCharacter ShowDialog(NPCMapCharacter existingInstance, out DialogResult result)
        {
            if (existingInstance == null)
                existingInstance = new NPCMapCharacter();

            npc = Utility.Clone<NPCMapCharacter>(existingInstance);

            SeControlValues(npc);

            result = base.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                GetControlValues(ref npc);
                return npc;
            }

            return existingInstance;
        }

        void SeControlValues(NPCMapCharacter npc)
        {
            this.npcName.Text = npc.Name;
            this.idleOnly.Checked = npc.IdleOnly;
        }

        void GetControlValues(ref NPCMapCharacter npc)
        {
            npc.Name = npcName.Text;
            npc.IdleOnly = idleOnly.Checked;
        }

        void previewWalkingAnimation_Click(object sender, EventArgs e)
        {            
        }

        void previewIdleAnimation_Click(object sender, EventArgs e)
        {
        }

        void walkingAnimationButton_Click(object sender, EventArgs e)
        {
            SpriteAnimationForm form = new SpriteAnimationForm();

            DialogResult result;
            npc.WalkingAnimation = form.ShowDialog(npc.WalkingAnimation, out result);
        }

        void idleAnimationButton_Click(object sender, EventArgs e)
        {
            SpriteAnimationForm form = new SpriteAnimationForm();

            DialogResult result;
            npc.IdleAnimation = form.ShowDialog(npc.IdleAnimation, out result);
        }
    }
}
