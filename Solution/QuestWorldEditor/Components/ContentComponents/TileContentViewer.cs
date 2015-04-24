using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace QuestWorldEditor
{
    /// <summary>
    /// Derives the ContentViewer class. This viewer displays
    /// the tile content of a Map and handles the logic
    /// behind selecting tile selections for editting
    /// </summary>
    public class TileContentViewer : ContentViewer
    {
        protected override void Initialize()
        {
            base.Initialize();
            currentSelection = -1;
        }

        /// <summary>
        /// Perform any required rendering
        /// </summary>
        protected override void Render()
        {
            base.Render();

            if(mainForm.CurrentMap != null && mainForm.CurrentMap.TileSheet != null)
            {
                spriteBatch.Begin();

                for (int i = 0; i < frameDestination.Count; ++i)
                {
                    spriteBatch.Draw(
                        mainForm.CurrentMap.TileSheet,
                        frameDestination[i],
                        frameSources[i],
                        Color.White);

                    spriteBatch.Draw(
                        MainForm.ThinTileOutline,
                        frameDestination[i],
                        MainForm.TileOutlineColor);
                }

                spriteBatch.End();
            }
        }

        internal override void LostMouse()
        {
            currentSelection = -1;
        }
    }
}
