using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MyQuest;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace QuestWorldEditor
{
    /// <summary>
    /// Form for editting a SpriteAnimation object
    /// </summary>
    public partial class SpriteAnimationForm : Form
    {
        SpriteAnimation clone;
        List<FrameData> frameAnimations;

        public SpriteAnimationForm()
        {
            InitializeComponent();

            /// Setup the callbacks
            addAnimation.Click += new EventHandler(addAnimation_Click);
            removeAnimation.Click += new EventHandler(removeAnimation_Click);
            copyButton.Click += new EventHandler(copyButton_Click);
            modifyButton.Click += new EventHandler(modifyButton_Click);
        }

        /// <summary>
        /// A wrapper for the ShowDialog function which Shows the form as a dialog model box.
        /// </summary>
        /// <param name="existingInstance">The existing instance of the object to modify. The object 
        /// is cloned so that the original is not modified unless the dialog box is closed "Ok"</param>
        /// <param name="result">The result of the dialog is stored here</param>
        /// <returns>If the dialog result is "Ok", returns the modified clone of the existing instance.
        /// If the dialog result is "Cancel", returns the original un-modified existing instance</returns>
        public SpriteAnimation ShowDialog(SpriteAnimation existingInstance, out DialogResult result)
        {
            if (existingInstance == null)
                existingInstance = new SpriteAnimation();

            clone = Utility.Clone<SpriteAnimation>(existingInstance);

            SetControlValues(clone);

            result = base.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                GetControlValues(ref clone);
                return clone;
            }

            return existingInstance;
        }

        /// <summary>
        /// Set the values for each of the form's controls
        /// </summary>
        /// <param name="source">The source of the values</param>
        void SetControlValues(SpriteAnimation source)
        {
            animationName.Text = source.Name;

            string path = MainForm.RootContentFolder + source.SpriteSheetName;
            Utility.AppendImageFileExtension(ref path);
            textureName.Text = path;

            /// Reset the list and fill it with the animation data
            frameAnimations = new List<FrameData>();

            foreach (KeyValuePair<string, FrameAnimation> kvp in source.Animations)
            {
                /// Crash here caused by no validation on frameanimation form...
                /// Creating a frame animation with no frames-> Frames[0] crash
                animationList.Items.Add(kvp.Key);

                FrameData data = new FrameData();

                data.XOffset = kvp.Value.Frames[0].X - 0;
                data.YOffset = kvp.Value.Frames[0].Y - 0;

                data.FrameCount = kvp.Value.Frames.Count;
                data.FrameWidth = kvp.Value.Frames[0].Width;
                data.FrameHeight = kvp.Value.Frames[0].Height;

                data.FrameDelay = kvp.Value.FrameDelay;

                frameAnimations.Add(data);
            }
        }

        /// <summary>
        /// Copy the form's control values into the destination object
        /// </summary>
        /// <param name="destination">The object to copy values to</param>
        void GetControlValues(ref SpriteAnimation destination)
        {
            destination.Name = animationName.Text;
            destination.SpriteSheetName = Path.GetFileNameWithoutExtension(textureName.Text);

            destination.Animations.Clear();

            for (int i = 0; i < frameAnimations.Count; ++i)
            {
                FrameAnimation animation = new FrameAnimation();

                for (int j = 0; j < frameAnimations[i].FrameCount; ++j)
                {
                    Rectangle rect = new Rectangle(
                        (j * frameAnimations[i].FrameWidth) + frameAnimations[i].XOffset,
                        frameAnimations[i].YOffset,
                        frameAnimations[i].FrameWidth,
                        frameAnimations[i].FrameHeight);

                    animation.Frames.Add(rect);
                }

                animation.FrameDelay = frameAnimations[i].FrameDelay;

                destination.Animations.Add(frameAnimations[i].Name, animation);
            }
        }

        #region Buttons


        void addAnimation_Click(object sender, EventArgs e)
        {           
            FrameAnimationForm form = new FrameAnimationForm();
            
            DialogResult result;
            FrameData frameData = form.ShowDialog(null, out result);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                frameAnimations.Add(frameData);
                animationList.Items.Add(frameData.Name);
            }
        }
        
        void removeAnimation_Click(object sender, EventArgs e)
        {
            if (animationList.SelectedIndex != -1)
            {
                frameAnimations.RemoveAt(animationList.SelectedIndex);
                animationList.Items.RemoveAt(animationList.SelectedIndex);

                Invalidate();
            }
        }

        void copyButton_Click(object sender, EventArgs e)
        {
            if (animationList.SelectedIndex != -1)
            {
                FrameData clone = Utility.Clone<FrameData>(frameAnimations[animationList.SelectedIndex]);

                frameAnimations.Insert(animationList.SelectedIndex + 1, clone);

                animationList.Items.Insert(animationList.SelectedIndex + 1, clone.Name);

                Invalidate();
            }
        }

        void modifyButton_Click(object sender, EventArgs e)
        {
            if (animationList.SelectedIndex != -1)
            {
                FrameAnimationForm form = new FrameAnimationForm();

                FrameData clone = Utility.Clone<FrameData>(frameAnimations[animationList.SelectedIndex]);

                DialogResult result;
                clone = form.ShowDialog(clone, out result);

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    frameAnimations[animationList.SelectedIndex] = clone;
                    animationList.Items[animationList.SelectedIndex] = clone.Name;
                    Invalidate();
                }
            }
        }

        void openTextureFileButton_Click(object sender, EventArgs e)
        {
            openTextureFile.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*JPEG)|*.BMP;*.JPG;*.PNG;*JPEG|All files (*.*)|*.*";

            openTextureFile.InitialDirectory = MainForm.RootContentFolder;

            if (openTextureFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textureName.Text = openTextureFile.FileName;
            }
        }


        #endregion
    }

    #region Frame Data

    /// <summary>
    /// Used internally as an intermediate container for FrameAnimation Data
    /// </summary>
    internal class FrameData
    {
        public string Name
        {
            get;
            set;
        }

        public int XOffset
        {
            get;
            set;
        }

        public int YOffset
        {
            get;
            set;
        }

        public int FrameCount
        {
            get;
            set;
        }

        public int FrameWidth
        {
            get;
            set;
        }

        public int FrameHeight
        {
            get;
            set;
        }

        public double FrameDelay
        {
            get;
            set;
        }
    }

    #endregion
}
