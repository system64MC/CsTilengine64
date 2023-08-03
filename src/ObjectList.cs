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
    public struct ObjectList
    {
        private IntPtr _objListAddr;
        public IntPtr ObjList { get { return _objListAddr; } }
        public ObjectList(IntPtr objListAddr)
        {
            _objListAddr = objListAddr;
        }
        public bool AddTileObject(UInt16 id, UInt16 gid, TileFlags flags, int x, int y)
        {
            return TLN_AddTileObjectToList(_objListAddr, id, gid, flags, x, y);
        }
        public ObjectList(string filename, string layername)
        {
            _objListAddr = TLN_LoadObjectList(filename, layername);
        }
        public ObjectList LoadObjectList(string filename, string layername)
        {
            return new ObjectList(TLN_LoadObjectList(filename, layername));
        }
        public ObjectList Clone()
        {
            return new ObjectList(TLN_CloneObjectList(_objListAddr));
        }
        public int NumObjects { get { return TLN_GetListNumObjects(_objListAddr); } }
        public ObjectInfo ObjectInfo
        {
            get
            {
                ObjectInfo objInfo = new ObjectInfo();
                unsafe
                {
                    TLN_GetListObject(_objListAddr, (IntPtr)(&objInfo));
                }
                return objInfo;
            }
        }
        public void Delete()
        {
            TLN_DeleteObjectList(_objListAddr);
            _objListAddr = IntPtr.Zero;
        }
    }
}