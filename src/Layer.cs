#region License

/* CsTilenginePure - 1:1 Api C# Wrapper for Tilengine
 * Copyright (C) 2022 Simon Vonhoff <mailto:simon.vonhoff@outlook.com>
 *
 * Tilengine - The 2D retro graphics engine with raster effects
 * Copyright (c) 2018 Marc Palacios Domènech <mailto:megamarc@hotmail.com>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 */

/*
 *****************************************************************************
 * C# Tilengine binding - Up to date to library version 2.11.0 and above.
 * http://www.tilengine.org
 *****************************************************************************
 */

#endregion

#region Using Statements

using System.Numerics;
using static Tilengine.TLN;

#endregion

#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Tilengine
{
    public struct Layer
    {
        private int _index;
        public Layer(int index)
        {
            _index = index;
        }
        public int Index
        { get { return _index; } }

        public Tileset Tileset
        {
            get
            {
                return new Tileset(TLN_GetLayerTileset(_index));
            }
        }

        public Tilemap Tilemap
        {
            get
            {
                return new Tilemap(TLN_GetLayerTilemap(_index));
            }
            set
            {
                TLN_SetLayerTilemap(_index, value.Tmap);
            }
        }
        public Bitmap Bitmap
        {
            get
            {
                return new Bitmap(TLN_GetLayerBitmap(_index));
            }
            set
            {
                TLN_SetLayerBitmap(_index, value.Bmap);
            }
        }
        public Palette Palette
        {
            get
            {
                return new Palette(TLN_GetLayerPalette(_index));
            }
            set
            {
                TLN_SetLayerPalette(_index, value.Pal);
            }
        }
        public int X
        {
            get { return TLN_GetLayerX(_index); }
            set
            {
                var y = TLN_GetLayerY(_index);
                TLN_SetLayerPosition(_index, value, y);
            }
        }
        public int Y { get { return TLN_GetLayerY(_index); } set { TLN_SetLayerPosition(_index, TLN_GetLayerX(_index), value); } }
        public bool SetPosition(int x, int y)
        {
            return TLN_SetLayerPosition(_index, x, y);
        }
        public Vector2 Position
        {
            get { return new Vector2(TLN_GetLayerX(_index), TLN_GetLayerY(_index)); }
            set { TLN_SetLayerPosition(_index, (int)value.X, (int)value.Y); }
        }
        public bool SetScaling(int scaleX, int scaleY)
        {
            return TLN_SetLayerScaling(_index, scaleX, scaleY);
        }
        public Affine Affine
        {
            set { TLN_SetLayerAffineTransform(_index, value); }
        }
        public bool SetTransform(float angle, float dx, float dy, float sx, float sy)
        {
            return TLN_SetLayerTransform(_index, angle, dx, dy, sx, sy);
        }
        public PixelMap[] PixelMapping
        {
            set { TLN_SetLayerPixelMapping(_index, value); }
        }
        public bool SetBlendMode(Blend mode, byte factor)
        {
            return TLN_SetLayerBlendMode(_index, mode, factor);
        }
        public int[] ColumnOffset
        {
            set { TLN_SetLayerColumnOffset(_index, value); }
        }
        public bool SetClip(int x1, int y1, int x2, int y2)
        {
            return TLN_SetLayerClip(_index, x1, y1, x2, y2);
        }
        public bool DisableClip()
        {
            return TLN_DisableLayerClip(_index);
        }
        public bool SetWindow(int x1, int y1, int x2, int y2, bool invert)
        {
            return TLN_SetLayerWindow(_index, x1, y1, x2, y2, invert);
        }
        public bool SetWindowColor(byte r, byte g, byte b, Blend blend)
        {
            return TLN_SetLayerWindowColor(_index, r, g, b, blend);
        }
        public bool SetWindowColor(Color color, Blend blend)
        {
            return TLN_SetLayerWindowColor(_index, color.r, color.g, color.b, blend);
        }
        public bool DisableWindow()
        {
            return TLN_DisableLayerWindow(_index);
        }
        public bool DisableWindowColor()
        {
            return TLN_DisableLayerWindowColor(_index);
        }
        public bool SetMosaic(int width, int height)
        {
            return TLN_SetLayerMosaic(_index, width, height);
        }
        public bool DisableMosaic()
        {
            return TLN_DisableLayerMosaic(_index);
        }
        public bool ResetMode()
        {
            return TLN_ResetLayerMode(_index);
        }
        public ObjectList Objects
        {
            get { return new ObjectList(TLN_GetLayerObjects(_index)); }
        }
        public bool SetObjects(ObjectList list, Tileset tileset)
        {
            return TLN_SetLayerObjects(_index, list.ObjList, tileset.Tset);
        }
        public bool Priority
        {
            set { TLN_SetLayerPriority(_index, value); }
        }
        public Layer Parent
        {
            set { TLN_SetLayerParent(_index, value.Index); }
        }
        public bool DisableParent()
        {
            return TLN_DisableLayerParent(_index);
        }
        public bool Enabled
        {
            set
            {
                if (value)
                    TLN_EnableLayer(_index);
                else
                    TLN_DisableLayer(_index);
            }
        }
        public LayerType Type
        {
            get { return TLN_GetLayerType(_index); }
        }
        public TileInfo GetTile(int x, int y)
        {
            var infos = new TileInfo();
            TLN_GetLayerTile(_index, x, y, out infos);
            return infos;
        }
        public int Width
        {
            get { return TLN_GetLayerWidth(_index); }
        }
        public int Height
        {
            get { return TLN_GetLayerHeight(_index); }
        }
    }
}