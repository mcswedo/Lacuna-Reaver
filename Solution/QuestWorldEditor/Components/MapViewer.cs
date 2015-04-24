using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyQuest;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using System.Windows.Forms;
using System.IO;

namespace QuestWorldEditor
{    
    /// <summary>
    /// Layers that can be edited 
    /// </summary>
    public enum Layers
    {
        Ground,
        ForeGround,
        Fringe,
        Collision,
        Damage,
        Portal,
        CombatZone,
        NPC,
        CutScene
    }

    /// <summary>
    /// A GraphicsControl which handles the viewing and editting
    /// of QuestClassLibrary Maps. This control is designed to be
    /// contained within a MapController.
    /// </summary>
    public class MapViewer : GraphicsDeviceControl
    {
        #region Fields


        /// <summary>
        /// Scroll speed modifier
        /// </summary>
        const float ScrollSpeed = 0.025f;

        MainForm mainForm;

        SpriteBatch spriteBatch;
        Map map;

        Vector2 cameraPosition;

        /// <summary>
        /// Determines if the camera should scroll when the mouse moves
        /// </summary>
        bool autoScrolling = false;

        /// <summary>
        /// The position where the user pressed the middle-mouse button
        /// </summary>
        Vector2 scrollCenter = Vector2.Zero;

        float zoomFactor = 1.0f;


        #endregion

        #region Properties


        public float ZoomFactor
        {
            set
            {
                zoomFactor = MathHelper.Clamp(value, 0.25f, 1.0f);
            }
        }
      
        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public MainForm MainForm
        {
            set { mainForm = value; }
        }


        #endregion

        protected override void Initialize()
        {         
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }

        protected override void Draw()
        {
            Logic();
            Render();
        }       

        #region Render


        void Render()
        {
            GraphicsDevice.Clear(Color.Black);

            if (map == null)
                return;

            Matrix transform =
                Matrix.CreateScale(zoomFactor, zoomFactor, 1f) * Matrix.CreateTranslation(new Vector3(-cameraPosition, 0f));
            if (mainForm.DrawGround)
                map.DrawGroundLayer(spriteBatch, transform, Matrix.Identity);

            if (mainForm.DrawForeGround)
                map.DrawForeGroundLayer(spriteBatch, transform, Matrix.Identity);
            
            if (mainForm.DrawFringe)
                map.DrawFringeLayer(spriteBatch, transform, Matrix.Identity);

            if (mainForm.DrawCollision)
                DrawNonGraphicLayer(map.CollisionLayer, MainForm.ThickTileOutline, Color.Red, transform, 1f, true, true);

            if (mainForm.DrawDamage)
                DrawNonGraphicLayer(map.DamageLayer, MainForm.ThickTileOutline, Color.Black, transform, 0f, true, false);

            if (mainForm.DrawPortal)
                DrawPortals(MainForm.ThinTileOutline, transform);

            if (mainForm.DrawCombat)
                DrawCombatZones(transform);

            if (mainForm.DrawNPC)
                DrawNPCs(MainForm.ThinTileOutline, transform);

            if (mainForm.DrawCutscene)
                DrawCutscenes(MainForm.ThinTileOutline, transform);

            /// Placing a grid over the map is a new feature I added and instead of writing special code to handle it,
            /// I just used a function I already wrote. Since none of the collision layer values will ever be 99,
            /// the function will draw a grid over every tile
            if(MainForm.DrawMapGrid)
                DrawNonGraphicLayer(map.CollisionLayer, MainForm.ThinTileOutline, MainForm.MapGridColor, transform, 99f, false, false);


            /// If we're erasing, we don't draw the overlay graphic
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                return;

            RenderGraphicOverlay();
        }

        void DrawCombatZones(Matrix transform)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, null, null, null, null, transform);

            Rectangle destination = new Rectangle(0, 0, map.TileDimensions.X, map.TileDimensions.Y);
            Rectangle source = new Rectangle(0, 0, MainForm.ThickTile.Width, MainForm.ThickTile.Height);

            for (int x = 0; x < map.DimensionsInTiles.X; ++x)
            {
                for (int y = 0; y < map.DimensionsInTiles.Y; ++y)
                {
                    int index = map.GetIndex(x, y);

                    if (map.CombatLayer[index] == 0)
                        continue;

                    destination.X = x * map.TileDimensions.X;
                    destination.Y = y * map.TileDimensions.Y;

                    string name = CombatZonePool.Singleton.GetZone(map.CombatLayer[index]).ZoneName;

                    System.Drawing.Color c = mainForm.combatListView.Items[name].BackColor;

                    spriteBatch.Draw(MainForm.ThickTile, destination, source, new Color(c.R, c.G, c.B, 192));
                }
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Helper function for displaying non-graphical layers like the Collision and Damage layers
        /// </summary>
        /// <param name="layer">the layer of values to display</param>
        /// <param name="outline">the outline texture to be used</param>
        /// <param name="outlineColor">the color of that texture</param>
        /// <param name="valueToSkip">any indices with this value will be skipped</param>
        void DrawNonGraphicLayer(float[] layer, Texture2D outline, Color outlineColor, Matrix transform, float valueToSkip, bool applyValueToColor, bool invertAlpha)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, transform);

            Rectangle destination = new Rectangle(0, 0, map.TileDimensions.X, map.TileDimensions.Y);
            Rectangle source = new Rectangle(0, 0, outline.Width, outline.Height);

            for (int x = 0; x < map.DimensionsInTiles.X; ++x)
            {
                for (int y = 0; y < map.DimensionsInTiles.Y; ++y)
                {
                    int index = map.GetIndex(x, y);

                    if (layer[index] == valueToSkip)
                        continue;

                    destination.X = x * map.TileDimensions.X;
                    destination.Y = y * map.TileDimensions.Y;

                    if (applyValueToColor)
                    {
                        /// clamp just in case the values are weird for some reason
                        float alpha = MathHelper.Clamp(layer[index], 0f, 1f);
                        
                        if(invertAlpha)
                            alpha = 1 - alpha;
                        
                        spriteBatch.Draw(outline, destination, source, outlineColor * alpha);
                    }
                    else
                    {
                        spriteBatch.Draw(outline, destination, source, outlineColor);
                    }
                }
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Helper that renders portal information over the map
        /// </summary>
        void DrawPortals(Texture2D texture, Matrix transform)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, transform);

            Rectangle location = new Rectangle(0, 0, ContentViewer.DisplayFrameSize, ContentViewer.DisplayFrameSize);

            foreach (Portal portal in map.Portals)
            {
                location.X = portal.Position.X * map.TileDimensions.X;
                location.Y = portal.Position.Y * map.TileDimensions.Y;

                spriteBatch.Draw(texture, location, Color.White);
                spriteBatch.DrawString(mainForm.SpriteFont, portal.DestinationMap, new Vector2(location.X + 10, location.Y + 15), Color.White);
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Helper that renders portal information over the map
        /// </summary>
        void DrawCutscenes(Texture2D texture, Matrix transform)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, transform);

            Rectangle location = new Rectangle(0, 0, ContentViewer.DisplayFrameSize, ContentViewer.DisplayFrameSize);

            foreach (CutSceneEntry entry in map.CutSceneEntries)
            {
                location.X = entry.TriggerPosition.X * map.TileDimensions.X;
                location.Y = entry.TriggerPosition.Y * map.TileDimensions.Y;

                spriteBatch.Draw(texture, location, Color.White);
                spriteBatch.DrawString(mainForm.SpriteFont, entry.AssetName, new Vector2(location.X + 10, location.Y + 15), Color.White);
            }

            spriteBatch.End();
        }

        void DrawNPCs(Texture2D texture, Matrix transform)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, transform);

            Rectangle location = new Rectangle(0, 0, ContentViewer.DisplayFrameSize, ContentViewer.DisplayFrameSize);

            foreach (NPCEntry entry in map.NpcEntries)
            {
                location.X = entry.SpawnPosition.X * map.TileDimensions.X;
                location.Y = entry.SpawnPosition.Y * map.TileDimensions.Y;

                spriteBatch.Draw(texture, location, Color.White);
                spriteBatch.DrawString(mainForm.SpriteFont, entry.AssetName, new Vector2(location.X + 10, location.Y + 15), Color.White);
            }

            spriteBatch.End();
        }

        void RenderGraphicOverlay()
        {
            float width = map.TileDimensions.X * zoomFactor;
            float height = map.TileDimensions.Y * zoomFactor;

            System.Drawing.Point mouse = PointToClient(MousePosition);

            Rectangle destination = new Rectangle(
                mouse.X - (int)width / 2,
                mouse.Y - (int)height / 2,
                (int)width,
                (int)height);
            
            spriteBatch.Begin();

            switch (MainForm.CurrentLayer)
            {
                case Layers.Collision:
                    spriteBatch.Draw(MainForm.ThickTileOutline, destination, new Rectangle(0, 0, MainForm.ThickTileOutline.Width, MainForm.ThickTileOutline.Height), Color.Red * 128f);
                    break;

                case Layers.Damage:
                    spriteBatch.Draw(MainForm.ThickTileOutline, destination, new Rectangle(0, 0, MainForm.ThickTileOutline.Width, MainForm.ThickTileOutline.Height), Color.Lime * 128f);
                    break;

                case Layers.Portal:
                    spriteBatch.Draw(MainForm.ThinTileOutline, destination, Color.White);
                    break;
                case Layers.CombatZone:
                    break;

                case Layers.ForeGround:
                case Layers.Fringe:
                case Layers.Ground:
                    if (mainForm.CurrentTabPage.Name != MainForm.TileTabPage)
                        break;

                    ContentViewer view = (ContentViewer)mainForm.CurrentTabPage.Controls[0];
                    int selection = view.CurrentSelection;
                    if (selection == -1)
                        break;

                    spriteBatch.Draw(map.TileSheet, destination, view.SourceFrame(selection), Color.White);
                    break;
            }

            spriteBatch.End();
        }


        #endregion

        #region Logic


        void Logic()
        {
            if (map == null || mainForm.MenuActive)
                return;

            System.Drawing.Point p = PointToClient(MousePosition);

            if (!Bounds.Contains(p) || mainForm.MenuActive)
                return;

            Vector2 relativeMouse = new Vector2(p.X, p.Y);

            if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                if (!autoScrolling)
                {
                    scrollCenter = relativeMouse;
                    autoScrolling = true;
                }

                Cursor = Cursors.NoMove2D;

                cameraPosition += ((relativeMouse - scrollCenter) * ScrollSpeed * zoomFactor);

                cameraPosition.X =
                    (int)MathHelper.Clamp(cameraPosition.X, 0, map.DimensionsInPixels.X * zoomFactor - Width);

                cameraPosition.Y =
                    (int)MathHelper.Clamp(cameraPosition.Y, 0, map.DimensionsInPixels.Y * zoomFactor - Height);
            }
            else
            {
                autoScrolling = false;

                Cursor = Cursors.Default;

                int cellX = (int)((relativeMouse.X + cameraPosition.X) / (map.TileDimensions.X * zoomFactor));
                int cellY = (int)((relativeMouse.Y + cameraPosition.Y) / (map.TileDimensions.Y * zoomFactor));

                /// just in case the map is smaller than the display.
                if (cellX >= map.DimensionsInTiles.X || cellY >= map.DimensionsInTiles.Y)
                    return;

                mainForm.currentMapTile.Text = cellX.ToString() + "," + cellY.ToString();

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    switch (mainForm.CurrentTabPage.Name)
                    {
                        case MainForm.TileTabPage:
                            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                                FloodFill(cellX, cellY);
                            else
                                SetLayerValueAt(cellX, cellY);
                            break;

                        case MainForm.PortalTabPage:
                            {
                                if (MainForm.CurrentLayer != Layers.Portal)
                                    break;

                                PortalListBox box = (PortalListBox)mainForm.CurrentTabPage.Controls[0];
                                if (box.Items.Count == 0 || box.SelectedIndex < 0)
                                    break;

                                foreach (Portal portal in map.Portals)
                                {
                                    if (portal.Position.X == cellX && portal.Position.Y == cellY)
                                    {
                                        map.Portals.Remove(portal);
                                        break;
                                    }
                                }

                                Portal selection = box.SelectedItem as Portal;
                                Portal copy = selection.Clone() as Portal;
                                copy.Position = new Point(cellX, cellY);
                                map.Portals.Add(copy);
                                break;
                            }
                        case MainForm.CombatZoneTabPage:
                            {
                                if (MainForm.CurrentLayer != Layers.CombatZone)
                                    break;

                                ListView view = (ListView)mainForm.CurrentTabPage.Controls[0];
                                if (view.Items.Count == 0 || view.SelectedItems.Count == 0)
                                    break;

                                ListViewItem item = view.SelectedItems[0];
                                map.CombatLayer[cellY * map.DimensionsInTiles.X + cellX] = CombatZonePool.Singleton.ToIndex(item.Name);
                                break;
                            }
                        case MainForm.NPCTabPage:
                            {
                                if (MainForm.CurrentLayer != Layers.NPC)
                                    break;

                                ListBox box = (ListBox)mainForm.CurrentTabPage.Controls[0];
                                if (box.Items.Count == 0 || box.SelectedIndex < 0)
                                    break;

                                for (int i = 0; i < map.NpcEntries.Count; ++i)
                                {
                                    if (map.NpcEntries[i].SpawnPosition.X == cellX && map.NpcEntries[i].SpawnPosition.Y == cellY)
                                    {
                                        map.NpcEntries.RemoveAt(i);
                                        break;
                                    }
                                }

                                SelectDirectionForm form = new SelectDirectionForm();
                                form.ShowDialog();
                                
                                Direction npcDirection;

                                switch (form.directionBox.SelectedIndex)
                                {
                                    case 0:
                                        npcDirection = Direction.North;
                                        break;
                                    case 1:
                                        npcDirection = Direction.East;
                                        break;
                                    case 2:
                                        npcDirection = Direction.South;
                                        break;
                                    default:
                                        npcDirection = Direction.West;
                                        break;
                                }

                                string assetName = (string)box.SelectedItems[0];
                                map.NpcEntries.Add(new NPCEntry 
                                {
                                    AssetName = assetName,
                                    SpawnPosition = new Point(cellX, cellY),
                                    SpawnDirection = npcDirection
                                });
                            }
                            break;

                        case MainForm.CutSceneTabPage:
                            {
                                if (MainForm.CurrentLayer != Layers.CutScene)
                                    break;

                                ListBox box = (ListBox)mainForm.CurrentTabPage.Controls[0];
                                if(box.Items.Count == 0 || box.SelectedIndex < 0)
                                    break;

                                for (int i = 0; i < map.CutSceneEntries.Count; ++i)
                                {
                                    if (map.CutSceneEntries[i].TriggerPosition.X == cellX && map.CutSceneEntries[i].TriggerPosition.Y == cellY)
                                    {
                                        map.CutSceneEntries.RemoveAt(i);
                                        break;
                                    }
                                }
                                
                                string assetName = Path.GetFileNameWithoutExtension((string)box.SelectedItems[0]);
                                map.CutSceneEntries.Add(new CutSceneEntry() { AssetName = assetName, TriggerPosition = new Point(cellX, cellY) });
                            }
                            break;
                    }
                }
                else if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    switch (mainForm.CurrentTabPage.Name)
                    {
                        case MainForm.TileTabPage:

                            ClearLayerValueAt(cellX, cellY);
                            break;

                        case MainForm.PortalTabPage:
                            if (MainForm.CurrentLayer != Layers.Portal)
                                break;

                            foreach (Portal portal in map.Portals)
                            {
                                if (portal.Position.X == cellX && portal.Position.Y == cellY)
                                {
                                    map.Portals.Remove(portal);
                                    break;
                                }
                            }
                            break;

                        case MainForm.CombatZoneTabPage:
                            if (MainForm.CurrentLayer != Layers.CombatZone)
                                break;

                            map.CombatLayer[cellY * map.DimensionsInTiles.X + cellX] = 0;
                            break;

                        case MainForm.NPCTabPage:
                            if (MainForm.CurrentLayer != Layers.NPC)
                                break;

                            for (int i = 0; i < map.NpcEntries.Count; ++i)
                            {
                                if (map.NpcEntries[i].SpawnPosition.X == cellX && map.NpcEntries[i].SpawnPosition.Y == cellY)
                                {
                                    map.NpcEntries.RemoveAt(i);
                                    break;
                                }
                            }
                            break;

                        case MainForm.CutSceneTabPage:
                            {
                                if (MainForm.CurrentLayer != Layers.CutScene)
                                    break;

                                for (int i = 0; i < map.CutSceneEntries.Count; ++i)
                                {
                                    if (map.CutSceneEntries[i].TriggerPosition.X == cellX && map.CutSceneEntries[i].TriggerPosition.Y == cellY)
                                    {
                                        map.CutSceneEntries.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Sets the value at the specified location based on MainForm input
        /// </summary>
        void SetLayerValueAt(int cellX, int cellY)
        {
            ContentViewer view = (ContentViewer)mainForm.CurrentTabPage.Controls[0];

            switch (MainForm.CurrentLayer)
            {
                case Layers.Ground:
                    if (view.CurrentSelection != -1)
                        map.GroundLayer[cellY * map.DimensionsInTiles.X + cellX] = view.CurrentSelection;
                    break;
            
                case Layers.ForeGround:
                    if (view.CurrentSelection != -1)
                        map.ForeGroundLayer[cellY * map.DimensionsInTiles.X + cellX] = view.CurrentSelection;
                    break;

                case Layers.Fringe:
                    if (view.CurrentSelection != -1)
                        map.FringeLayer[cellY * map.DimensionsInTiles.X + cellX] = view.CurrentSelection;
                    break;

                case Layers.Collision:
                    map.CollisionLayer[cellY * map.DimensionsInTiles.X + cellX] = mainForm.CollisionModifier / 100f;
                    break;

                case Layers.Damage:
                    map.DamageLayer[cellY * map.DimensionsInTiles.X + cellX] = mainForm.DamageModifier / 100f;
                    break;
            }
        }

        /// <summary>
        /// Sets the value at the specified location to a default value
        /// </summary>
        void ClearLayerValueAt(int cellX, int cellY)
        {
            switch (MainForm.CurrentLayer)
            {
                case Layers.Ground:
                    map.GroundLayer[cellY * map.DimensionsInTiles.X + cellX] = -1;
                    break;

                case Layers.ForeGround:
                    map.ForeGroundLayer[cellY * map.DimensionsInTiles.X + cellX] = -1;
                    break;

                case Layers.Fringe:
                    map.FringeLayer[cellY * map.DimensionsInTiles.X + cellX] = -1;
                    break;

                case Layers.Collision:
                    map.CollisionLayer[cellY * map.DimensionsInTiles.X + cellX] = 1;
                    break;

                case Layers.Damage:
                    map.DamageLayer[cellY * map.DimensionsInTiles.X + cellX] = 0;
                    break;
            }
        }

        void FloodFill(int cellX, int cellY)
        {
            short[] layer = null;

            ContentViewer view = (ContentViewer)mainForm.CurrentTabPage.Controls[0];

            switch (MainForm.CurrentLayer)
            {
                case Layers.Ground:
                    if (view.CurrentSelection != -1)
                        layer = map.GroundLayer;
                    break;

                case Layers.ForeGround:
                    if (view.CurrentSelection != -1)
                        layer = map.ForeGroundLayer;
                    break;

                case Layers.Fringe:
                    if (view.CurrentSelection != -1)
                        layer = map.FringeLayer;
                    break;
            }

            if (layer != null && layer.Length < 8101)
            {
                Flood(
                    layer,
                    layer[cellY * map.DimensionsInTiles.X + cellX],
                    view.CurrentSelection,
                    cellX,
                    cellY);
            }
        }

        void Flood(short[] layer, short replace, short newValue, int cellX, int cellY)
        {
            int index = cellY * map.DimensionsInTiles.X + cellX;
            if (index < 0 || index >= layer.Length)
                return;

            /// If we shouldn't be replacing it or it 
            /// has already been replaced, jump out
            if (layer[index] != replace || layer[index] == newValue)
                return;

            layer[index] = newValue;

            Flood(layer, replace, newValue, cellX - 1, cellY);
            Flood(layer, replace, newValue, cellX - 1, cellY - 1);
            Flood(layer, replace, newValue, cellX - 1, cellY + 1);
            Flood(layer, replace, newValue, cellX + 1, cellY);
            Flood(layer, replace, newValue, cellX + 1, cellY - 1);
            Flood(layer, replace, newValue, cellX + 1, cellY + 1);
            Flood(layer, replace, newValue, cellX, cellY - 1);
            Flood(layer, replace, newValue, cellX, cellY + 1);
        }


        #endregion
    }
}
