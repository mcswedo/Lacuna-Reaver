using System.Windows.Forms;

namespace QuestWorldEditor
{
    public partial class FrameAnimationForm : Form
    {
        public FrameAnimationForm()
        {
            InitializeComponent();
        }

        internal FrameData ShowDialog(FrameData existingInstance, out DialogResult result)
        {
            if (existingInstance == null)
            {
                existingInstance = new FrameData();
            }

            FrameData clone = Utility.Clone<FrameData>(existingInstance);

            SetControlValues(clone);

            result = base.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                GetControlValues(ref clone);
                return clone;
            }

            return existingInstance;    
        }

        void SetControlValues(FrameData data)
        {
            animationName.Text = data.Name;

            xoffset.Text = data.XOffset.ToString();
            yoffset.Text = data.YOffset.ToString();

            frameCount.Text = data.FrameCount.ToString();

            frameWidth.Text = data.FrameWidth.ToString();
            frameHeight.Text = data.FrameHeight.ToString();
            frameDelay.Text = data.FrameDelay.ToString();
        }

        void GetControlValues(ref FrameData data)
        {
            data.Name = animationName.Text;

            data.XOffset = int.Parse(xoffset.Text);
            data.YOffset = int.Parse(yoffset.Text);

            data.FrameCount = int.Parse(frameCount.Text);

            data.FrameWidth = int.Parse(frameWidth.Text);
            data.FrameHeight = int.Parse(frameHeight.Text);
            data.FrameDelay = double.Parse(frameDelay.Text);  
        }
    }
}
