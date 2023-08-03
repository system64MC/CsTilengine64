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

using static Tilengine.TLN;

#endregion

#pragma warning disable CA1401 // P/Invokes should not be visible

namespace Tilengine
{
    public struct Tilemap
    {
        private IntPtr _tilemapAddr;

        // For loading a tilemap from file
        public static Tilemap LoadTilemap(string fileName, string? layerName = null)
        {
            Tilemap tmap;
            tmap._tilemapAddr = TLN_LoadTilemap(fileName, layerName);
            return tmap;
        }

        public Tilemap(string fileName, string? layerName = null)
        {
            _tilemapAddr = TLN_LoadTilemap(fileName, layerName);
        }

        public Tilemap(IntPtr tilemapAddr)
        {
            _tilemapAddr = tilemapAddr;
        }

        public Tilemap(int rows, int columns, Tile[] tiles, UInt32 bgColor, Tileset tileset)
        {
            _tilemapAddr = TLN_CreateTilemap(rows, columns, tiles, bgColor, tileset.Tset);
        }

        public bool SetLayer(int layer)
        {
            return TLN_SetLayerTilemap(layer, _tilemapAddr);
        }

        public Tilemap Clone()
        {
            Tilemap tmap;
            tmap._tilemapAddr = TLN_CloneTilemap(this._tilemapAddr);
            return tmap;
        }

        public void DeleteTilemap()
        {
            TLN_DeleteTilemap(_tilemapAddr);
            this._tilemapAddr = IntPtr.Zero;
        }

        public IntPtr Tmap { get { return _tilemapAddr; } }

        public int Rows { get { return TLN_GetTilemapRows(_tilemapAddr); } }
        public int Columns { get { return TLN_GetTilemapCols(_tilemapAddr); } }

        public Tileset Tileset
        {
            get
            {
                return new Tileset(TLN_GetTilemapTileset(_tilemapAddr));
                //Tileset tset = new Tileset();
                //tset.Tset = TLN_GetTilemapTileset(_tilemapAddr);
                //return tset;
            }

            set
            {
                TLN_SetTilemapTileset(_tilemapAddr, value.Tset);
            }
        }

        public bool SetTilesetByIndex(Tileset tileset, int index)
        {
            return TLN_SetTilemapTileset2(_tilemapAddr, tileset.Tset, index);
        }

        public Tileset GetTilesetByIndex(int index)
        {
            return new Tileset(TLN_GetTilemapTileset2(_tilemapAddr, index));
            //tileset.Tset = TLN.TLN_GetTilemapTileset2(_tilemapAddr, index);
            //return tileset;
        }

        public Tile GetTile(int row, int column)
        {
            Tile tile;
            bool success = TLN_GetTilemapTile(_tilemapAddr, row, column, out tile);
            if (!success)
                tile.index = 0xFFFF;
            return tile;
        }

        public bool SetTile(int row, int column, Tile tile)
        {
            return TLN_SetTilemapTile(_tilemapAddr, row, column, tile);
        }

        public bool SetTileAttribute(int row, int column, byte? unused = null, byte? palette = null, byte? tileset = null, bool? masked = null, bool? priority = null, bool? rotated = null, bool? flipy = null, bool? flipx = null)
        {
            Tile tile = new();
            bool success = TLN_GetTilemapTile(_tilemapAddr, row, column, out tile);
            if (!success)
                return success;
            tile.unused = unused ?? tile.unused;
            tile.palette = palette ?? tile.palette;
            tile.tileset = tileset ?? tile.tileset;
            tile.masked = masked ?? tile.masked;
            tile.priority = priority ?? tile.priority;
            tile.rotated = rotated ?? tile.rotated;
            tile.flipy = flipy ?? tile.flipy;
            tile.flipx = flipx ?? tile.flipx;
            success = TLN_SetTilemapTile(_tilemapAddr, row, column, in tile);
            return success;
        }

        public bool SetBgColor()
        {
            return TLN_SetBGColorFromTilemap(_tilemapAddr);
        }

        public bool CopyTiles(int srcRow, int srcColumn, int rows, int columns, Tilemap destination, int destRow, int destColumn)
        {
            return TLN_CopyTiles(_tilemapAddr, srcRow, srcColumn, rows, columns, destination.Tmap, destRow, destColumn);
        }

        public bool Delete()
        {
            bool res = TLN_DeleteBitmap(_tilemapAddr);
            _tilemapAddr = 0;
            return res;
        }

        public static bool operator ==(Tilemap tilemap, object obj)
        {
            if (obj == null)
                return tilemap._tilemapAddr == IntPtr.Zero;

            if (obj is Tilemap otherTilemap)
                return tilemap._tilemapAddr == otherTilemap._tilemapAddr;

            return false;
        }

        public static bool operator !=(Tilemap tilemap, object obj)
        {
            return !(tilemap == obj);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Tilemap otherTilemap)
                return this._tilemapAddr == otherTilemap._tilemapAddr;

            return false;
        }

        public override int GetHashCode()
        {
            return _tilemapAddr.GetHashCode();
        }
    }
}