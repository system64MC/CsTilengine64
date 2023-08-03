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
    public struct Sequence
    {
        private IntPtr _seqAddr;
        public IntPtr Seq
        {
            get { return _seqAddr; }
        }
        public Sequence(IntPtr seqAddr)
        {
            _seqAddr = seqAddr;
        }
        // Sequence
        public Sequence(string name, int target, int numFrames, SequenceFrame[] frames)
        {
            _seqAddr = TLN_CreateSequence(name, target, numFrames, frames);
        }
        // Cycle
        public Sequence(string name, int numStrips, ColorStrip[] strips)
        {
            _seqAddr = TLN_CreateCycle(name, numStrips, strips);
        }
        // Sprite
        public Sequence(string name, SpriteSet spriteset, string baseName, int delay)
        {
            _seqAddr = TLN_CreateSpriteSequence(name, spriteset.SprSet, baseName, delay);
        }
        public Sequence Clone()
        {
            return new Sequence(TLN_CloneSequence(_seqAddr));
        }
        public SequenceInfo Infos
        {
            get
            {
                SequenceInfo seqInfos = new SequenceInfo();
                TLN_GetSequenceInfo(_seqAddr, out seqInfos);
                return seqInfos;
            }
        }
        public void Delete()
        {
            TLN_DeleteSequence(_seqAddr);
            _seqAddr = IntPtr.Zero;
        }
    }
}