using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace QuestWorldEditor
{
    /// <summary>
    /// A custom control for displaying map content in a TabPage
    /// </summary>
    public class ContentViewer : GraphicsDeviceControl
    {
        /// <summary>
        /// Anything we display in the viewer will be 64x64
        /// </summary>
        internal const int DisplayFrameSize = 64;
        
        #region Fields


        TabPage container;

        protected MainForm mainForm;

        protected SpriteBatch spriteBatch;
        protected Texture2D tileOutline;

        /// <summary>
        /// Pre-calculated destination rectangles for the WorldDisplay
        /// </summary>
        protected List<Rectangle> frameDestination;

        /// <summary>
        /// Pre-calculated source rectanges from the map's tilesheet used when drawing to the WorldDisplay
        /// </summary>
        protected List<Rectangle> frameSources;

        protected short currentSelection;


        #endregion

        #region Properties


        /// <summary>
        /// The form this viewer is contained in
        /// </summary>
        public MainForm MainForm
        {
            set { mainForm = value; }
        }
        
        
        /// <summary>
        /// The index of the currently selected item
        /// </summary>
        public short CurrentSelection
        {
            get { return currentSelection; }
        }


        /// <summary>
        /// The dimensions of the Viewer in tiles
        /// </summary>
        public Point TileDisplayInTiles
        {
            get
            {
                return new Microsoft.Xna.Framework.Point(
                    Size.Width / DisplayFrameSize,
                    Size.Height / DisplayFrameSize);
            }
        }


        public int TileCount
        {
            get
            {
                return (frameDestination == null ? 0 : frameDestination.Count);
            }
        }

        internal TabPage PageContainer
        {
            get { return container; }
            set { container = value; }
        }


        #endregion

        #region Methods


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Draw()
        {
            Logic();
            Render();
        }

        /// <summary>
        /// Perform any required rendering
        /// </summary>
        protected virtual void Render()
        {
            GraphicsDevice.Clear(Color.Black);
        }

        /// <summary>
        /// Perform and required Logic
        /// </summary>
        protected virtual void Logic()
        {
            //If no map is loaded or we aren't currently editting a graphics layer
            if (mainForm.CurrentMap == null ||
                MainForm.CurrentLayer == Layers.Collision ||
                MainForm.CurrentLayer == Layers.Damage)
            {
                return;
            }

            /// The following madness has to do with making sure that the mouse is inside the content viewer.
            /// The tab pages auto scroll so that I don't have to mess with using stupid scroll bars. The downside
            /// is that I lose all control over how the scroll bars work and how the viewer and its container are
            /// sized. Container sizes and locations are completely unintuitive and this dirty combination of 
            /// calculations was the only way to get this viewer to work correctly.

            /// Get the mouse relative to my Container
            System.Drawing.Point relativeMouse = PageContainer.PointToClient(MousePosition);

            /// Also get the mouse relative to the current MapViewer
            System.Drawing.Point mapRelativeMouse = mainForm.CurrentMapViewer.PointToClient(MousePosition);

            /// The top left of my Container Bounds are slightly off but we can adjust for that by moving the mouse
            relativeMouse.X += 3; // I believe this is the margin? padding of the tab page
            relativeMouse.Y += 18; // I believe this is the height of the the actual tab (the part with text)

            /// The width of my Container Bounds extends outside the Application Window ??!! so I check against mine as well
            if (PageContainer.Bounds.Contains(relativeMouse) && relativeMouse.X < Size.Width)
            {
                //int cellX = relativeMouse.X / DisplayFrameSize;
                int cellX = (relativeMouse.X + Math.Abs(PageContainer.AutoScrollPosition.X)) / DisplayFrameSize;

                /// Now that we've made it in the content viewer, move the mouse back to where it should be
                relativeMouse.Y -= 18;

                /// The scroll bar position is negative in the Y direction. I believe that's because it is
                /// used for translations which makes sense so I take the ABS of it
                int cellY = (relativeMouse.Y + Math.Abs(PageContainer.AutoScrollPosition.Y)) / DisplayFrameSize;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    short index = (short)(cellY * TileDisplayInTiles.X + cellX);
                    currentSelection = (index < frameDestination.Count ? index : (short)-1);
                }

                return;
            }
            Debug.WriteLine("Mouse out");
            if (!mainForm.CurrentMapViewer.Bounds.Contains(mapRelativeMouse) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                currentSelection = -1;
            }
        }

        public void BuildDestinationFrames()
        {
            frameDestination = new List<Rectangle>();

            for (int i = 0; i < frameSources.Count; ++i)
            {
                frameDestination.Add(new Rectangle(
                    (i % TileDisplayInTiles.X) * DisplayFrameSize,
                    (i / TileDisplayInTiles.X) * DisplayFrameSize,
                    DisplayFrameSize,
                    DisplayFrameSize));
            }
        }

        public void BuildSourceFrames()
        {
            frameSources = new List<Rectangle>();

            int cols = mainForm.CurrentMap.TileSheet.Width / mainForm.CurrentMap.TileDimensions.X;
            int rows = mainForm.CurrentMap.TileSheet.Height / mainForm.CurrentMap.TileDimensions.Y;

            for (int y = 0; y < rows; ++y)
            {
                for (int x = 0; x < cols; ++x)
                {
                    frameSources.Add(new Rectangle(
                        x * mainForm.CurrentMap.TileDimensions.X,
                        y * mainForm.CurrentMap.TileDimensions.Y,
                        mainForm.CurrentMap.TileDimensions.X,
                        mainForm.CurrentMap.TileDimensions.Y));
                }
            }
        }

        public Rectangle SourceFrame(int index)
        {
            if (index < 0 || index >= frameSources.Count)
                return Rectangle.Empty;

            return frameSources[index];
        }

        internal virtual void LostMouse()
        {
        }

        internal void UpdateSize(Size containerSize)
        {
            if (mainForm.CurrentMap == null)
                return;

            /// Calculate the number of tiles in the tilesheet
            int sheetCols = mainForm.CurrentMap.TileSheet.Width / mainForm.CurrentMap.TileDimensions.X;
            int sheetRows = mainForm.CurrentMap.TileSheet.Height / mainForm.CurrentMap.TileDimensions.Y;

            /// Let's assume that the tilesheet is very large. If we allow the number of columns
            /// to become very small then the height of the viewer will become very large. The
            /// graphics device limits the dimensions of the backbuffer and if we exceed that
            /// limit, the device will fail. For that reason, 4 columns is a safe minimum.
            //int cols = Math.Max(4, (containerSize.Width - 5) / DisplayFrameSize);

            /// The number of rows we will display is the total
            /// number of tiles / the number of columns we display
            //int rows = (int)Math.Ceiling(sheetCols * sheetRows / (float)cols);


            /// Make sure our width and height are large enough for at least on tile
            int width = Math.Max(DisplayFrameSize, sheetCols * DisplayFrameSize);
            int height = Math.Max(DisplayFrameSize, sheetRows * DisplayFrameSize);

            Size = new Size(width, height);
        }

        /*internal void UpdateSize(Size containerSize)
        {
            if (mainForm.CurrentMap == null)
                return;

            /// Calculate the number of tiles in the tilesheet
            int sheetCols = mainForm.CurrentMap.tileSheet.Width / mainForm.CurrentMap.TileDimensions.X;
            int sheetRows = mainForm.CurrentMap.tileSheet.Height / mainForm.CurrentMap.TileDimensions.Y;

            /// Let's assume that the tilesheet is very large. If we allow the number of columns
            /// to become very small then the height of the viewer will become very large. The
            /// graphics device limits the dimensions of the backbuffer and if we exceed that
            /// limit, the device will fail. For that reason, 4 columns is a safe minimum.
            int cols = Math.Max(4, (containerSize.Width - 5) / DisplayFrameSize);

            /// The number of rows we will display is the total
            /// number of tiles / the number of columns we display
            int rows = (int)Math.Ceiling(sheetCols * sheetRows / (float)cols);


            /// Make sure our width and height are large enough for at least on tile
            int width = Math.Max(DisplayFrameSize, cols * DisplayFrameSize);
            int height = Math.Max(DisplayFrameSize, rows * DisplayFrameSize);

            Size = new Size(width, height);
        }*/


        #endregion
    }
}
