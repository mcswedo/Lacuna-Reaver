using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyQuest;
using Point = Microsoft.Xna.Framework.Point;

namespace QuestWorldEditor
{
    public partial class CreateMapForm : Form
    {
        public CreateMapForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.PNG;*JPEG)|*.BMP;*.JPG;*.PNG;*JPEG|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = MainForm.RootContentFolder;
        }

        public Map ShowDialog(Map existingInstance)
        {
            Map map = existingInstance;

            if (map == null)
            {
                map = new Map();
            }
            else
            {
                map = Utility.Clone<Map>(existingInstance);
            }

            SetControlValues(map);

            if (base.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetControlValues(ref map);
                return map;
            }
            else
                return existingInstance;
        }

        private void textureButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                texturePath.Text = openFileDialog1.FileName;
            }
        }

        void SetControlValues(Map map)
        {
            mapName.Text = map.Name;
            texturePath.Text = map.TextureSheetName;

            tileWidth.Text = map.TileDimensions.X.ToString();
            tileHeight.Text = map.TileDimensions.Y.ToString();

            widthInTiles.Text = map.DimensionsInTiles.X.ToString();
            heightInTiles.Text = map.DimensionsInTiles.Y.ToString();
        }

        void GetControlValues(ref Map map)
        {
            map.Name = mapName.Text;
            map.TextureSheetName = texturePath.Text;

            map.TileDimensions = new Point(
                int.Parse(tileWidth.Text),
                int.Parse(tileHeight.Text));

            int newCols = int.Parse(widthInTiles.Text);
            int newRows = int.Parse(heightInTiles.Text);

            map.GroundLayer = CopyLayer<short>(map.GroundLayer, map.DimensionsInTiles.Y, map.DimensionsInTiles.X, newRows, newCols, -1);
            map.ForeGroundLayer = CopyLayer<short>(map.ForeGroundLayer, map.DimensionsInTiles.Y, map.DimensionsInTiles.X, newRows, newCols, -1);
            map.FringeLayer = CopyLayer<short>(map.FringeLayer, map.DimensionsInTiles.Y, map.DimensionsInTiles.X, newRows, newCols, -1);
            map.CollisionLayer = CopyLayer<float>(map.CollisionLayer, map.DimensionsInTiles.Y, map.DimensionsInTiles.X, newRows, newCols, 1);
            map.DamageLayer = CopyLayer<float>(map.DamageLayer, map.DimensionsInTiles.Y, map.DimensionsInTiles.X, newRows, newCols, 0);
            map.CombatLayer = CopyLayer<short>(map.CombatLayer, map.DimensionsInTiles.Y, map.DimensionsInTiles.X, newRows, newCols, 0);

            map.DimensionsInTiles = new Point(newCols, newRows);
        }
        
        /// <summary>
        /// Untested !!
        /// </summary>       
        T[] CopyLayer<T>(T[] source, int srcRows, int srcCols, int newRows, int newCols, T defaultValue)
        {
            T[] newLayer = new T[newRows * newCols];

            Utility.InitializeArray<T>(newLayer, defaultValue);

            if (source == null)
                return newLayer;

            int copyRows = (source.Length < newLayer.Length ? srcRows : newRows);
            int copyCols = (source.Length < newLayer.Length ? srcCols : newCols);

            for (int col = 0; col < copyCols; ++col)
            {
                for (int row = 0; row < copyRows; ++row)
                {
                    newLayer[row * srcCols + col] = source[row * srcCols + col];
                }
            }

            //if (source.Length < newLayer.Length)
            //{
            //    for (int col = 0; col < srcCols; ++col)
            //    {
            //        for (int row = 0; row < srcRows; ++row)
            //        {
            //            newLayer[row * srcCols + col] = source[row * srcCols + col];
            //        }
            //    }
            //}
            //else
            //{
            //    for (int col = 0; col < newCols; ++col)
            //    {
            //        for (int row = 0; row < newRows; ++row)
            //        {
            //            newLayer[row * srcCols + col] = source[row * srcCols + col];
            //        }
            //    }
            //}

            return newLayer;
        }
    }
}
