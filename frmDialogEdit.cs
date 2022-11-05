namespace DialogEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Dialog Editor Form
    /// </summary>
    public partial class FrmDialogEdit : Form
    {
        private class LSP_ENCODE_BUFF
        {
            public LSP_ENCODE_BUFF()
            {
                this.Pixels = new ushort[1024];
            }

            public LSP_ENCODE_BUFF Prev { get; set; }

            public LSP_ENCODE_BUFF Next { get; set; }

            public ushort Zero { get; set; }

            public ushort PixelCount { get; set; }

            public ushort[] Pixels { get; set; }
        }

        private class SPR_LSP
        {
            public Rectangle Area { get; set; }

            public int LineCount { get; set; }

            public int DataSize { get; set; }

            public ushort[] Data { get; set; }
        }

        private byte[] headerBytes = new byte[147];

        private Bitmap dialogImage;
        private string openFileName;

        private Point mouseDownPosition;
        private Point mouseMovePosition;

        private bool showAllArea = true;
        private bool showButtonArea = true;
        private bool showButtonFocus = true;
        private bool showButtonDown = true;
        private bool showButtonExtraArea = true;
        private bool showButtonExtraNormal = true;
        private bool showButtonExtraFocus = true;
        private bool showButtonExtraDown = true;

        private const int FillSize = 8;

        private enum SelectionType
        {
            None,
            Area,
            ButtonArea,
            ButtonFocus,
            ButtonDown,
            ButtonExtraArea,
            ButtonExtraNormal,
            ButtonExtraFocus,
            ButtonExtraDown
        }

        private enum SizeType
        {
            Left,
            Top,
            Right,
            Bottom,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private enum MouseMoveType
        {
            None,
            Drag,
            SizeLeft,
            SizeTop,
            SizeRight,
            SizeBottom,
            SizeTopLeft,
            SizeTopRight,
            SizeBottomLeft,
            SizeBottomRight
        }

        private struct SelectedRectangle
        {
            public Rectangle Rect;
            public int Index;          // Index within the array of selected rectangle type
            public SelectionType Type;
        }

        private struct DialogButton
        {
            public Rectangle Area;
            public Rectangle Focus;
            public Rectangle Down;
            public int Transparent;
            public int Style;
        }

        private struct DialogButtonExtra
        {
            public Rectangle Area;
            public Rectangle Normal;
            public Rectangle Focus;
            public Rectangle Down;
            public int Transparent;
            public int Style;
        }

        private List<DialogButton> dialogButtons = new List<DialogButton>();
        private List<DialogButtonExtra> dialogButtonsExtra = new List<DialogButtonExtra>();
        private List<Rectangle> areaRectangles = new List<Rectangle>();

        private SelectionType selectionType;
        private TreeNode selectedRectangleNode;
        private MouseMoveType mouseMoveType;

        private TreeNode mainRoot;
        private TreeNode areaRoot;
        private TreeNode buttonRoot;
        private TreeNode buttonExtraRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmDialogEdit"/> class.
        /// </summary>
        public FrmDialogEdit()
        {
            this.InitializeComponent();

            this.selectionType = SelectionType.None;

            this.imageBox1.MinimumSelectionSize = new Size(1, 1);
        }

        private ushort RGB16(int r, int g, int b)
        {
            return (ushort)(((r >> 3) << 11) | ((g >> 2) << 5) | (b >> 3));
        }

        private byte RGBBlue(ushort colour)
        {
            return (byte)((colour & 0x1F) << 3);
        }

        private byte RGBGreen(ushort colour)
        {
            return (byte)(((colour & 0x7E0) << 2) >> 5);
        }

        private byte RGBRed(ushort colour)
        {
            return (byte)(((colour & 0xF800) << 3) >> 11);
        }

        private void ImportDialogImage(string fileName)
        {
            Bitmap newImage;
            newImage = new Bitmap(fileName);
            if (this.dialogImage.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                MessageBox.Show("You have imported an invalid 256 colour bitmap file.");
            }
            else
            {
                this.dialogImage = newImage;
                this.imageBox1.Image = this.dialogImage;
            }
        }

        private void ExportDialogImage(string fileName)
        {
            this.dialogImage.Save(fileName, ImageFormat.Bmp);
        }

        private void ExportDialogInformation(string fileName)
        {
            //StreamWriter writer = new StreamWriter(fileName);
            //writer.WriteLine("Myth of Soma Dialog Information for {0} Exported by Dialog Editor.\r\n\r\n", this.Text);
            //writer.WriteLine("Area Rectangles (Left, Top, Right, Bottom)");
            //foreach (Rectangle area in this.dialogAreaSelector.Items)
            //{
            //    writer.WriteLine("  {0}, {1}, {2}, {3}", area.Left, area.Top, area.Right, area.Bottom);
            //}

            //writer.WriteLine("\r\nButton Rectangles(Left, Top, Right, Bottom) Transparent(Yes/No) Style(??)");
            //foreach (DialogButton button in this.dialogButtonSelector.Items)
            //{
            //    writer.WriteLine("  Area:= {0}, {1}, {2}, {3}", button.Area.Left, button.Area.Top, button.Area.Right, button.Area.Bottom);
            //    writer.WriteLine("  Focus:= {0} Top: {1}, {2}, {3}", button.Focus.Left, button.Focus.Top, button.Focus.Right, button.Focus.Bottom);
            //    writer.WriteLine("  Down:= {0}, {1}, {2}, {3}", button.Down.Left, button.Down.Top, button.Down.Right, button.Down.Bottom);
            //    writer.WriteLine("  Transparent:= {0} Style:= {0}\r\n", "no value yet", "no value yet");
            //}

            //writer.WriteLine("\r\nButtonExtra Rectangles(Left, Top, Right, Bottom) Transparent(Yes/No) Style(??)");
            //foreach (DialogButtonExtra buttonExtra in this.dialogButtonExtraSelector.Items)
            //{
            //    writer.WriteLine("  Area:= {0}, {1}, {2}, {3}", buttonExtra.Area.Left, buttonExtra.Area.Top, buttonExtra.Area.Right, buttonExtra.Area.Bottom);
            //    writer.WriteLine("  Focus:= {0} Top: {1}, {2}, {3}", buttonExtra.Normal.Left, buttonExtra.Normal.Top, buttonExtra.Normal.Right, buttonExtra.Normal.Bottom);
            //    writer.WriteLine("  Focus:= {0} Top: {1}, {2}, {3}", buttonExtra.Focus.Left, buttonExtra.Focus.Top, buttonExtra.Focus.Right, buttonExtra.Focus.Bottom);
            //    writer.WriteLine("  Down:= {0}, {1}, {2}, {3}", buttonExtra.Down.Left, buttonExtra.Down.Top, buttonExtra.Down.Right, buttonExtra.Down.Bottom);
            //    writer.WriteLine("  Transparent:= {0} Style:= {0}\r\n", "no value yet", "no value yet");
            //}

            //writer.Close();
        }

        private void SaveSomaLib(string fileName)
        {
            FileStream dialogStream = new FileStream(fileName, FileMode.OpenOrCreate);
            BinaryWriter dialogWriter = new BinaryWriter(dialogStream);
            dialogWriter.Write(this.headerBytes, 0, 147);

            this.SaveSpl256_(dialogWriter);

            foreach (TreeNode node in this.mainRoot.Nodes)
            {
                Rectangle area = (Rectangle)node.Tag;
                dialogWriter.Write(area.Left);
                dialogWriter.Write(area.Top);
                dialogWriter.Write(area.Right);
                dialogWriter.Write(area.Bottom);
                break;
            }

            dialogWriter.Write(this.areaRoot.Nodes.Count);
            foreach (TreeNode node in this.areaRoot.Nodes)
            {
                Rectangle area = (Rectangle)node.Tag;
                dialogWriter.Write(area.Left);
                dialogWriter.Write(area.Top);
                dialogWriter.Write(area.Right);
                dialogWriter.Write(area.Bottom);
            }

            dialogWriter.Write(this.buttonRoot.Nodes.Count);
            foreach (TreeNode node in this.buttonRoot.Nodes)
            {
                DialogButton button = (DialogButton)node.Tag;
                Rectangle area = button.Area;
                dialogWriter.Write(area.Left);
                dialogWriter.Write(area.Top);
                dialogWriter.Write(area.Right);
                dialogWriter.Write(area.Bottom);

                Rectangle focus = button.Focus;
                dialogWriter.Write(focus.Left);
                dialogWriter.Write(focus.Top);
                dialogWriter.Write(focus.Right);
                dialogWriter.Write(focus.Bottom);

                Rectangle down = button.Down;
                dialogWriter.Write(down.Left);
                dialogWriter.Write(down.Top);
                dialogWriter.Write(down.Right);
                dialogWriter.Write(down.Bottom);

                dialogWriter.Write(button.Transparent);
                dialogWriter.Write(button.Style);
            }

            dialogWriter.Write(this.buttonExtraRoot.Nodes.Count);
            foreach (TreeNode node in this.buttonExtraRoot.Nodes)
            {
                DialogButtonExtra buttonExtra = (DialogButtonExtra)node.Tag;
                Rectangle area = buttonExtra.Area;
                dialogWriter.Write(area.Left);
                dialogWriter.Write(area.Top);
                dialogWriter.Write(area.Right);
                dialogWriter.Write(area.Bottom);

                Rectangle normal = buttonExtra.Normal;
                dialogWriter.Write(normal.Left);
                dialogWriter.Write(normal.Top);
                dialogWriter.Write(normal.Right);
                dialogWriter.Write(normal.Bottom);

                Rectangle focus = buttonExtra.Focus;
                dialogWriter.Write(focus.Left);
                dialogWriter.Write(focus.Top);
                dialogWriter.Write(focus.Right);
                dialogWriter.Write(focus.Bottom);

                Rectangle down = buttonExtra.Down;
                dialogWriter.Write(down.Left);
                dialogWriter.Write(down.Top);
                dialogWriter.Write(down.Right);
                dialogWriter.Write(down.Bottom);

                dialogWriter.Write(buttonExtra.Transparent);
                dialogWriter.Write(buttonExtra.Style);
            }

            dialogStream.Close();
        }

        /// <summary>
        /// Loads a Myth of Soma Dialog
        /// </summary>
        /// <param name="fileName">Name of dialog file to load</param>
        public void LoadSomaLib(string fileName)
        {
            this.mainRoot = new TreeNode("Main");
            this.areaRoot = new TreeNode("Area");
            this.buttonRoot = new TreeNode("Button");
            this.buttonExtraRoot = new TreeNode("Button Extra");

            FileStream dialogStream = new FileStream(fileName, FileMode.Open);
            BinaryReader dialogReader = new BinaryReader(dialogStream);
            dialogReader.Read(this.headerBytes, 0, 147);

            Bitmap dialogBitmap = this.LoadSpl256(dialogReader);
            this.dialogImage = dialogBitmap;

            int x = dialogReader.ReadInt32();
            int y = dialogReader.ReadInt32();
            int width = dialogReader.ReadInt32() - x;
            int height = dialogReader.ReadInt32() - y;
            Rectangle rectMain = new Rectangle(x, y, width, height);
            this.mainRoot.Nodes.Add("0").Tag = rectMain;

            int areaCount = dialogReader.ReadInt32();
            for (int i = 0; i < areaCount; ++i)
            {
                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;

                Rectangle area = new Rectangle(x, y, width, height);

                this.areaRoot.Nodes.Add((i + 1).ToString()).Tag = area;
            }

            int buttonCount = dialogReader.ReadInt32();
            for (int i = 0; i < buttonCount; ++i)
            {
                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;
                Rectangle area = new Rectangle(x, y, width, height);

                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;
                Rectangle focus = new Rectangle(x, y, width, height);

                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;
                Rectangle down = new Rectangle(x, y, width, height);

                int transparent = dialogReader.ReadInt32();
                int style = dialogReader.ReadInt32();

                DialogButton button = default(DialogButton);
                button.Area = area;
                button.Focus = focus;
                button.Down = down;
                button.Transparent = transparent;
                button.Style = style;

                TreeNode buttonNode = new TreeNode((i + 1).ToString());
                buttonNode.Tag = button;
                buttonNode.Nodes.Add("Normal").Tag = button.Area;
                buttonNode.Nodes.Add("Focus").Tag = button.Focus;
                buttonNode.Nodes.Add("Down").Tag = button.Down;
                this.buttonRoot.Nodes.Add(buttonNode);
            }

            int buttonExtraCount = dialogReader.ReadInt32();
            for (int i = 0; i < buttonExtraCount; ++i)
            {
                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;
                Rectangle area = new Rectangle(x, y, width, height);

                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;
                Rectangle normal = new Rectangle(x, y, width, height);

                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;
                Rectangle focus = new Rectangle(x, y, width, height);

                x = dialogReader.ReadInt32();
                y = dialogReader.ReadInt32();
                width = dialogReader.ReadInt32() - x;
                height = dialogReader.ReadInt32() - y;
                Rectangle down = new Rectangle(x, y, width, height);

                int transparent = dialogReader.ReadInt32();
                int style = dialogReader.ReadInt32();

                DialogButtonExtra buttonExtra = default(DialogButtonExtra);
                buttonExtra.Area = area;
                buttonExtra.Normal = normal;
                buttonExtra.Focus = focus;
                buttonExtra.Down = down;
                buttonExtra.Transparent = transparent;
                buttonExtra.Style = style;

                TreeNode buttonExtraNode = new TreeNode((i + 1).ToString());
                buttonExtraNode.Tag = buttonExtra;
                buttonExtraNode.Nodes.Add("Area").Tag = buttonExtra.Area;
                buttonExtraNode.Nodes.Add("Normal").Tag = buttonExtra.Normal;
                buttonExtraNode.Nodes.Add("Focus").Tag = buttonExtra.Focus;
                buttonExtraNode.Nodes.Add("Down").Tag = buttonExtra.Down;
                this.buttonExtraRoot.Nodes.Add(buttonExtraNode);
            }

            dialogStream.Close();
            this.openFileName = fileName;

            this.imageBox1.Image = this.dialogImage;

            this.treeView1.Nodes.Add(this.mainRoot);
            this.treeView1.Nodes.Add(this.areaRoot);
            this.treeView1.Nodes.Add(this.buttonRoot);
            this.treeView1.Nodes.Add(this.buttonExtraRoot);
            this.treeView1.ExpandAll();
        }

        private Bitmap LoadSpl256(BinaryReader reader)
        {
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            int colour = reader.ReadInt32();
            int unknown = reader.ReadInt32();
            Rectangle position = new Rectangle(
                reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
            int lineCount = reader.ReadInt32();
            int dataSize = reader.ReadInt32();
            reader.BaseStream.Seek(4, SeekOrigin.Current);

            byte[] splData = new byte[dataSize];
            reader.Read(splData, 0, dataSize);

            short paletteSize = reader.ReadInt16();
            ushort[] paletteData = new ushort[paletteSize];

            for (int i = 0; i < paletteSize; ++i)
            {
                paletteData[i] = reader.ReadUInt16();
            }

            int skip = 0;
            int index = 0;

            byte[] temp = new byte[width * height];
            byte[] imageBuffer = new byte[width * height];
            byte[] source = splData;

            int sourceIndex = 0;
            for (int j = 0; j < lineCount; j++)
            {
                int nNode = source[sourceIndex] + (source[sourceIndex + 1] * 256);
                sourceIndex += 2;

                for (int k = 0; k < nNode; k++)
                {
                    int nZero = source[sourceIndex] + (source[sourceIndex + 1] * 256);
                    sourceIndex += 2;
                    index += nZero;

                    int nPixel = source[sourceIndex] + (source[sourceIndex + 1] * 256);
                    sourceIndex += 2;

                    for (int l = 0; l < nPixel; l++)
                    {
                        byte pixel = source[sourceIndex];
                        sourceIndex++;

                        temp[index] = pixel;
                        index++;
                    }

                    skip += nPixel + nZero;
                }

                index += width - skip;
                skip = 0;
            }

            for (int h = height - 1, h2 = 0; h >= 0; h--, h2++)
            {
                for (int w = 0; w < width; w++)
                {
                    imageBuffer[(h2 * width) + w] = temp[(h * width) + w];
                }
            }

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int copyLength = width * height;
            Marshal.Copy(imageBuffer, 0, ptr, copyLength);
            bmp.UnlockBits(bmpData);

            ColorPalette cp = bmp.Palette;
            for (int i = 0; i < paletteData.Length; ++i)
            {
                int r = this.RGBRed(paletteData[i]);
                int g = this.RGBGreen(paletteData[i]);
                int b = this.RGBBlue(paletteData[i]);
                cp.Entries[i] = Color.FromArgb(r, g, b);
            }

            bmp.Palette = cp;

            return bmp;
        }

        private void SaveSpl256(BinaryReader reader)
        {
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            int colour = reader.ReadInt32();
            int unknown = reader.ReadInt32();
            Rectangle position = new Rectangle(
                reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
            int lineCount = reader.ReadInt32();
            int dataSize = reader.ReadInt32();
            reader.BaseStream.Seek(4, SeekOrigin.Current);

            byte[] splData = new byte[dataSize];
            reader.Read(splData, 0, dataSize);

            short paletteSize = reader.ReadInt16();
            ushort[] paletteData = new ushort[paletteSize];

            for (int i = 0; i < paletteSize; ++i)
            {
                paletteData[i] = reader.ReadUInt16();
            }

            int skip = 0;
            int index = 0;

            byte[] temp = new byte[width * height];
            byte[] imageBuffer = new byte[width * height];
            byte[] source = splData;

            int sourceIndex = 0;
            for (int j = 0; j < lineCount; j++)
            {
                int nNode = source[sourceIndex] + (source[sourceIndex + 1] * 256);
                sourceIndex += 2;

                for (int k = 0; k < nNode; k++)
                {
                    int nZero = source[sourceIndex] + (source[sourceIndex + 1] * 256);
                    sourceIndex += 2;
                    index += nZero;

                    int nPixel = source[sourceIndex] + (source[sourceIndex + 1] * 256);
                    sourceIndex += 2;

                    for (int l = 0; l < nPixel; l++)
                    {
                        byte pixel = source[sourceIndex];
                        sourceIndex++;
                        temp[index] = pixel;
                        index++;
                    }

                    skip += nPixel + nZero;
                }

                index += width - skip;
                skip = 0;
            }

            for (int h = height - 1, h2 = 0; h >= 0; h--, h2++)
            {
                for (int w = 0; w < width; w++)
                {
                    imageBuffer[(h2 * width) + w] = temp[(h * width) + w];
                }
            }
        }

        private void SaveSpl256_(BinaryWriter writer)
        {
            BitmapData bmpData = this.dialogImage.LockBits(
                new Rectangle(0, 0, this.dialogImage.Width, this.dialogImage.Height), ImageLockMode.ReadWrite, this.dialogImage.PixelFormat);

            int padding = (4 - (bmpData.Width % 4)) % 4;
            int width = bmpData.Width + padding;
            int height = bmpData.Height;
            if (width < 1 || height < 1)
            {
                return;
            }

            // A lookup for the palette as RGB16 is needed because accessing
            // the palette colour for the pixel loop is too slow.
            ushort[] paletteRGB16 = new ushort[this.dialogImage.Palette.Entries.Length];
            for (int i = 0; i < this.dialogImage.Palette.Entries.Length; i++)
            {
                Color c = this.dialogImage.Palette.Entries[i];
                paletteRGB16[i] = this.RGB16(c.R, c.G, c.B);
            }

            ushort[] imageBuffer = new ushort[width * height];

            int srcOffset = (bmpData.Stride * height) - bmpData.Stride;
            int dstOffset = 0;

            for (int i = 0; i < height; ++i)
            {
                if (bmpData.PixelFormat == PixelFormat.Format16bppRgb565)
                {
                    //Marshal.Copy(bmpData.Scan0 + srcOffset, imageBuffer, 0, width * 2);
                }
                else if (bmpData.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    unsafe
                    {
                        byte* bmpBuffer = (byte*)bmpData.Scan0;
                        int offset = 0;
                        for (int w = 0; w < width; w++)
                        {
                            byte paletteIndex = bmpBuffer[srcOffset + offset++];
                            imageBuffer[dstOffset++] = this.RGB16(
                                this.dialogImage.Palette.Entries[paletteIndex].R,
                                this.dialogImage.Palette.Entries[paletteIndex].G,
                                this.dialogImage.Palette.Entries[paletteIndex].B);
                        }
                    }

                    srcOffset -= bmpData.Stride;
                }
                else
                {
                    return;
                }
            }

            ushort transparentPixel = this.RGB16(248, 0, 248);
            ushort[] buffer = new ushort[width * height * 4];
            int bufferOffset = 0;
            int bufferByteCount = 0;
            int lineCount = 0;
            int imageBufferOffset = 0;

            for (int y = 0; y < height; y++)
            {
                ushort node = 0;
                bool isTherePixel = false;

                LSP_ENCODE_BUFF encodeBuff = null;
                LSP_ENCODE_BUFF start = null;

                int ing = -1;
                for (int x = 0; x < width; x++)
                {
                    if (ing == -1)
                    {
                        encodeBuff = new LSP_ENCODE_BUFF();
                        start = encodeBuff;
                        node += 1;
                    }

                    ushort pixel = imageBuffer[imageBufferOffset];
                    imageBufferOffset += 1;

                    if (pixel == transparentPixel)
                    {
                        if (ing == 1)
                        {
                            LSP_ENCODE_BUFF tmp = new LSP_ENCODE_BUFF();
                            tmp.Prev = encodeBuff;
                            encodeBuff.Next = tmp;
                            tmp.Next = null;
                            encodeBuff = tmp;
                            isTherePixel = false;
                            node += 1;
                        }

                        encodeBuff.Zero += 1;
                        ing = 0;
                    }
                    else
                    {
                        encodeBuff.Pixels[encodeBuff.PixelCount] = pixel;
                        encodeBuff.PixelCount += 1;
                        ing = 1;
                        isTherePixel = true;
                    }
                }

                if (!isTherePixel)
                {
                    node -= 1;
                }

                buffer[bufferOffset] = node;
                bufferOffset += 1;
                bufferByteCount += 2;

                if (node == 0)
                {
                    encodeBuff = null;
                }
                else
                {
                    int n = 0;
                    while (start != null)
                    {
                        if (n < node)
                        {
                            buffer[bufferOffset] = start.Zero;
                            bufferOffset += 1;
                            bufferByteCount += 2;

                            buffer[bufferOffset] = start.PixelCount;
                            bufferOffset += 1;
                            bufferByteCount += 2;

                            Array.Copy(start.Pixels, 0, buffer, bufferOffset, start.PixelCount);
                            bufferOffset += start.PixelCount;
                            bufferByteCount += start.PixelCount * 2;

                            n += 1;
                        }

                        LSP_ENCODE_BUFF tmp = start.Next;
                        start = tmp;
                    }
                }

                lineCount += 1;
            }

            SPR_LSP spr = new SPR_LSP();
            spr.LineCount = lineCount;
            spr.DataSize = bufferByteCount;
            spr.Data = buffer;
            spr.Area = new Rectangle(0, 0, width, height);

            writer.BaseStream.Seek(191, SeekOrigin.Begin);

            int dataOffset = 0;
            int dataSize = 0;
            for (int i = 0; i < spr.LineCount; i++)
            {
                ushort node = spr.Data[dataOffset];
                dataOffset += 1;

                writer.Write(node);
                dataSize += 2;

                for (int j = 0; j < node; j++)
                {
                    ushort skip = spr.Data[dataOffset];
                    dataOffset += 1;

                    writer.Write(skip);
                    dataSize += 2;

                    ushort pixelCount = spr.Data[dataOffset];
                    dataOffset += 1;

                    writer.Write(pixelCount);
                    dataSize += 2;

                    byte[] indexedPixels = new byte[pixelCount];
                    int indexedPixelsOffset = 0;
                    for (int k = 0; k < pixelCount; k++)
                    {
                        ushort pixel = spr.Data[dataOffset];
                        dataOffset += 1;

                        byte paletteIndex = 0;
                        int length = this.dialogImage.Palette.Entries.Length;
                        for (int l = 0; l < length; l++)
                        {
                            if (paletteRGB16[l] == pixel)
                            {
                                paletteIndex = (byte)l;
                                break;
                            }

                            paletteIndex += 1;
                        }

                        indexedPixels[indexedPixelsOffset] = paletteIndex;
                        indexedPixelsOffset += 1;
                    }

                    writer.Write(indexedPixels);
                    dataSize += pixelCount;
                }
            }

            Color crCK = Color.FromArgb(0, 248, 0, 248);
            int unknown = 0;
            writer.BaseStream.Seek(147, SeekOrigin.Begin);
            writer.Write(width);
            writer.Write(height);
            writer.Write(crCK.ToArgb());
            writer.Write(unknown);
            writer.Write(spr.Area.Left);
            writer.Write(spr.Area.Top);
            writer.Write(spr.Area.Right);
            writer.Write(spr.Area.Bottom);
            writer.Write(spr.LineCount);
            writer.Write(dataSize);
            writer.BaseStream.Seek(4 + dataSize, SeekOrigin.Current);

            writer.Write((short)this.dialogImage.Palette.Entries.Length);
            for (int i = 0; i < this.dialogImage.Palette.Entries.Length; ++i)
            {
                ushort palettePixel = this.RGB16(this.dialogImage.Palette.Entries[i].R, this.dialogImage.Palette.Entries[i].G, this.dialogImage.Palette.Entries[i].B);
                writer.Write(palettePixel);
            }

            this.dialogImage.UnlockBits(bmpData);
        }

        private void ShowAreaRectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showAllArea = !this.showAllArea;
            this.areaRectsToolStripMenuItem.Checked = this.showAllArea;
            this.imageBox1.Invalidate();
        }

        private void FrmDialogEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.panel1.Bounds.Contains(this.PointToClient(Cursor.Position)))
            {
                if (e.KeyCode == Keys.ControlKey)
                {
                    // isSlowRectMove = true;
                }
            }
        }

        private void FrmDialogEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.panel1.Bounds.Contains(this.PointToClient(Cursor.Position)))
            {
                if (e.KeyCode == Keys.ControlKey)
                {
                    // isSlowRectMove = false;
                }
            }
        }

        private void DialogImageExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "256 Colour Bitmap (*.bmp)|*.bmp|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                this.ExportDialogImage(fileName);
            }
        }

        private void DialogInfoExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Plain Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                this.ExportDialogInformation(fileName);
            }
        }

        private void ButtonAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showButtonArea = !this.showButtonArea;
            this.buttonAreaToolStripMenuItem.Checked = this.showButtonArea;
            this.imageBox1.Invalidate();
        }

        private void ButtonFocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showButtonFocus = !this.showButtonFocus;
            this.buttonFocusToolStripMenuItem.Checked = this.showButtonFocus;
            this.imageBox1.Invalidate();
        }

        private void ButtonDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showButtonDown = !this.showButtonDown;
            this.buttonDownToolStripMenuItem.Checked = this.showButtonDown;
            this.imageBox1.Invalidate();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveSomaLib(this.openFileName);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Myth of Soma Dialog Files (*.lib)|*.lib|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                this.SaveSomaLib(fileName);
            }
        }

        private void ButtonExtraAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showButtonExtraArea = !this.showButtonExtraArea;
            this.buttonExtraAreaToolStripMenuItem.Checked = this.showButtonExtraArea;
            this.imageBox1.Invalidate();
        }

        private void ButtonExtraNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showButtonExtraNormal = !this.showButtonExtraNormal;
            this.buttonExtraNormalToolStripMenuItem.Checked = this.showButtonExtraNormal;
            this.imageBox1.Invalidate();
        }

        private void ButtonExtraFocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showButtonExtraFocus = !this.showButtonExtraFocus;
            this.buttonExtraFocusToolStripMenuItem.Checked = this.showButtonExtraFocus;
            this.imageBox1.Invalidate();
        }

        private void ButtonExtraDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showButtonExtraDown = !this.showButtonExtraDown;
            this.buttonExtraDownToolStripMenuItem.Checked = this.showButtonExtraDown;
            this.imageBox1.Invalidate();
        }

        private void DialogImageImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Replace the current dialog image";
            openFileDialog.Filter = "256 Colour Bitmap (*.bmp)|*.bmp|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                this.ImportDialogImage(fileName);
            }
        }

        private void DrawAreaRectangles(Graphics graphics)
        {
            if (this.showAllArea)
            {
                foreach (TreeNode node in this.areaRoot.Nodes)
                {
                    graphics.DrawRectangle(
                        new Pen(Color.Red, 1.0f),
                        this.imageBox1.GetOffsetRectangle((Rectangle)node.Tag));
                }
            }
        }

        private void DrawButtonRectangles(Graphics graphics)
        {
            foreach (TreeNode node in this.buttonRoot.Nodes)
            {
                DialogButton button = (DialogButton)node.Tag;
                if (this.showButtonArea)
                {
                    Rectangle rect = this.imageBox1.GetOffsetRectangle(button.Area);
                    if (node == this.treeView1.SelectedNode)
                    {
                        graphics.FillRectangle(
                            new SolidBrush(Color.FromArgb(128, SystemColors.Highlight)),
                            rect);
                    }

                    graphics.DrawRectangle(
                        new Pen(Color.Orange, 1.0f),
                        rect);
                }

                if (this.showButtonFocus)
                {
                    Rectangle rect = this.imageBox1.GetOffsetRectangle(button.Focus);
                    if (node == this.treeView1.SelectedNode)
                    {
                        graphics.FillRectangle(
                            new SolidBrush(Color.FromArgb(128, SystemColors.Highlight)),
                            rect);
                    }

                    graphics.DrawRectangle(
                        new Pen(Color.Yellow, 1.0f),
                        rect);
                }

                if (this.showButtonDown)
                {
                    Rectangle rect = this.imageBox1.GetOffsetRectangle(button.Down);
                    if (node == this.treeView1.SelectedNode)
                    {
                        graphics.FillRectangle(
                            new SolidBrush(Color.FromArgb(128, SystemColors.Highlight)),
                            rect);
                    }

                    graphics.DrawRectangle(
                        new Pen(Color.Green, 1.0f),
                        rect);
                }
            }
        }

        private void DrawButtonExtraRectangles(Graphics graphics)
        {
            foreach (TreeNode node in this.buttonExtraRoot.Nodes)
            {
                DialogButtonExtra buttonExtra = (DialogButtonExtra)node.Tag;
                if (this.showButtonExtraArea)
                {
                    graphics.DrawRectangle(
                        new Pen(Color.Orange, 1.0f),
                        this.imageBox1.GetOffsetRectangle(buttonExtra.Area));
                }

                if (this.showButtonExtraNormal)
                {
                    graphics.DrawRectangle(
                        new Pen(Color.Aqua, 1.0f),
                        this.imageBox1.GetOffsetRectangle(buttonExtra.Normal));
                }

                if (this.showButtonExtraFocus)
                {
                    graphics.DrawRectangle(
                        new Pen(Color.Yellow, 1.0f),
                        this.imageBox1.GetOffsetRectangle(buttonExtra.Focus));
                }

                if (this.showButtonExtraDown)
                {
                    Rectangle rect = this.imageBox1.GetOffsetRectangle(buttonExtra.Down);
                    graphics.DrawRectangle(
                        new Pen(Color.Green, 1.0f),
                        rect);
                }
            }
        }

        private void ImageBox1_Paint(object sender, PaintEventArgs e)
        {
            this.DrawAreaRectangles(e.Graphics);
            this.DrawButtonRectangles(e.Graphics);
            this.DrawButtonExtraRectangles(e.Graphics);
        }

        private TreeNode SelectNodeWithMouse(TreeNodeCollection nodes, TreeNode selectedNode, Point imagePoint, Point mousePosition)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is Rectangle)
                {
                    Rectangle imageRect = (Rectangle)node.Tag;
                    if (imageRect.Contains(imagePoint))
                    {
                        if (node != this.selectedRectangleNode && this.imageBox1.SelectionRegion.Contains(imageRect))
                        {
                            if (imageRect != this.imageBox1.SelectionRegion)
                            {
                                selectedNode = node;
                            }
                        }
                        else if (!this.imageBox1.SelectionRegion.Contains(imagePoint))
                        {
                            selectedNode = node;
                        }
                    }
                }

                selectedNode = this.SelectNodeWithMouse(node.Nodes, selectedNode, imagePoint, mousePosition);
            }

            return selectedNode;
        }

        private void ImageBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point imagePoint = this.imageBox1.PointToImage(e.Location);
                if (this.imageBox1.HitTest(e.Location) != Cyotek.Windows.Forms.DragHandleAnchor.None)
                {
                    return;
                }

                TreeNode selectedNode = this.SelectNodeWithMouse(this.treeView1.Nodes, null, imagePoint, e.Location);
                if (selectedNode != null)
                {
                    this.imageBox1.SelectionRegion = (Rectangle)selectedNode.Tag;
                    this.imageBox1.DragOrigin = e.Location;
                    this.imageBox1.DragOriginOffset = new Point(imagePoint.X - (int)this.imageBox1.SelectionRegion.X, imagePoint.Y - (int)this.imageBox1.SelectionRegion.Y);
                    this.treeView1.SelectedNode = selectedNode;
                }
            }
        }

        private void ImageBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.UpdateCursorPosition(e.Location);
        }

        private void ImageBox1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is Rectangle)
            {
                this.selectedRectangleNode = e.Node;
                this.selectionType = SelectionType.Area;
                this.imageBox1.SelectionRegion = (Rectangle)e.Node.Tag;
            }
            else
            {
                this.selectionType = SelectionType.None;
                this.imageBox1.SelectNone();
                this.imageBox1.Invalidate();
            }
        }

        private void UpdateSelectedRectangleNode()
        {
            if (this.selectionType != SelectionType.None)
            {
                Rectangle newRectangle = Rectangle.Truncate(this.imageBox1.SelectionRegion);

                this.selectedRectangleNode.Tag = newRectangle;

                if (this.selectedRectangleNode.Parent.Tag != null)
                {
                    if (this.selectedRectangleNode.Parent.Tag.GetType() == typeof(DialogButton))
                    {
                        DialogButton button = (DialogButton)this.selectedRectangleNode.Parent.Tag;
                        switch (this.selectedRectangleNode.Text)
                        {
                            case "Normal":
                                button.Area = newRectangle;
                                break;
                            case "Focus":
                                button.Focus = newRectangle;
                                break;
                            case "Down":
                                button.Down = newRectangle;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        this.selectedRectangleNode.Parent.Tag = button;
                    }
                    else if (this.selectedRectangleNode.Parent.Tag.GetType() == typeof(DialogButtonExtra))
                    {
                        DialogButtonExtra button = (DialogButtonExtra)this.selectedRectangleNode.Parent.Tag;
                        switch (this.selectedRectangleNode.Text)
                        {
                            case "Area":
                                button.Area = newRectangle;
                                break;
                            case "Normal":
                                button.Normal = newRectangle;
                                break;
                            case "Focus":
                                button.Focus = newRectangle;
                                break;
                            case "Down":
                                button.Down = newRectangle;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        this.selectedRectangleNode.Parent.Tag = button;
                    }
                }

                this.imageBox1.Invalidate();
            }
        }

        private void ImageBox1_SelectionMoved(object sender, EventArgs e)
        {
            this.SetStatus(string.Empty);

            this.UpdateSelectedRectangleNode();
        }

        private void ImageBox1_SelectionResized(object sender, EventArgs e)
        {
            this.SetStatus(string.Empty);

            this.UpdateSelectedRectangleNode();
        }

        private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                TreeNode dragNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode", false);
                Point dropPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode dropNode = ((TreeView)sender).GetNodeAt(dropPoint);

                if (dropNode != null &&
                    dragNode.TreeView == dropNode.TreeView &&
                    dragNode.Parent == dropNode.Parent)
                {
                    TreeNode root = null;
                    if (dragNode.Parent == this.areaRoot)
                    {
                        root = this.areaRoot;
                    }
                    else if (dragNode.Parent == this.buttonRoot)
                    {
                        root = this.buttonRoot;
                    }
                    else if (dragNode.Parent == this.buttonExtraRoot)
                    {
                        root = this.buttonExtraRoot;
                    }

                    if (root != null && dragNode != dropNode)
                    {
                        if (dragNode.Index < dropNode.Index)
                        {
                            dragNode.Remove();
                            root.Nodes.Insert(dropNode.Index + 1, dragNode);
                        }
                        else
                        {
                            dragNode.Remove();
                            root.Nodes.Insert(dropNode.Index, dragNode);
                        }

                        this.treeView1.SelectedNode = dragNode;

                        foreach (TreeNode node in root.Nodes)
                        {
                            node.Text = (node.Index + 1).ToString();
                        }
                    }
                }
            }
        }

        private void TreeView1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))
            {
                TreeNode dragNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode", false);
                Point dropPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode dropNode = ((TreeView)sender).GetNodeAt(dropPoint);
                if (dropNode != null &&
                    dragNode.TreeView == dropNode.TreeView &&
                    dragNode.Parent == dropNode.Parent)
                {
                    if (dragNode.Parent == this.areaRoot ||
                        dragNode.Parent == this.buttonRoot ||
                        dragNode.Parent == this.buttonExtraRoot)
                    {
                        this.treeView1.SelectedNode = dropNode;
                    }
                }
            }
        }

        private void AddArea()
        {
            Rectangle area = new Rectangle(0, 0, 50, 50);
            TreeNode node = this.areaRoot.Nodes.Add((this.areaRoot.Nodes.Count + 1).ToString());
            node.Tag = area;
            this.treeView1.SelectedNode = node;
        }

        private void AddButton()
        {
            Rectangle area = new Rectangle(0, 0, 50, 25);
            Rectangle focus = new Rectangle(0, 35, 50, 25);
            Rectangle down = new Rectangle(0, 70, 50, 25);

            DialogButton button = default(DialogButton);
            button.Area = area;
            button.Focus = focus;
            button.Down = down;
            button.Transparent = 0;
            button.Style = 0;

            TreeNode buttonNode = this.buttonRoot.Nodes.Add((this.buttonRoot.Nodes.Count + 1).ToString());
            buttonNode.Tag = button;
            buttonNode.Nodes.Add("Normal").Tag = button.Area;
            buttonNode.Nodes.Add("Focus").Tag = button.Focus;
            buttonNode.Nodes.Add("Down").Tag = button.Down;
            buttonNode.Expand();
            this.treeView1.SelectedNode = buttonNode;
        }

        private void AddButtonExtra()
        {
            Rectangle area = new Rectangle(0, 0, 50, 25);
            Rectangle normal = new Rectangle(0, 35, 50, 25);
            Rectangle focus = new Rectangle(0, 70, 50, 25);
            Rectangle down = new Rectangle(0, 105, 50, 25);

            DialogButtonExtra buttonExtra = default(DialogButtonExtra);
            buttonExtra.Area = area;
            buttonExtra.Normal = normal;
            buttonExtra.Focus = focus;
            buttonExtra.Down = down;
            buttonExtra.Transparent = 0;
            buttonExtra.Style = 0;

            TreeNode buttonExtraNode = this.buttonExtraRoot.Nodes.Add((this.buttonExtraRoot.Nodes.Count + 1).ToString());
            buttonExtraNode.Tag = buttonExtra;
            buttonExtraNode.Nodes.Add("Area").Tag = buttonExtra.Area;
            buttonExtraNode.Nodes.Add("Normal").Tag = buttonExtra.Normal;
            buttonExtraNode.Nodes.Add("Focus").Tag = buttonExtra.Focus;
            buttonExtraNode.Nodes.Add("Down").Tag = buttonExtra.Down;
            buttonExtraNode.Expand();
            this.treeView1.SelectedNode = buttonExtraNode;
        }

        private void RenumberElements(TreeNode root)
        {
            foreach (TreeNode node in root.Nodes)
            {
                node.Text = (node.Index + 1).ToString();
            }
        }

        private void AddElement()
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && node.Level == 0)
            {
                if (node.Text == "Area")
                {
                    this.AddArea();
                }
                else if (node.Text == "Button")
                {
                    this.AddButton();
                }
                else if (node.Text == "Button Extra")
                {
                    this.AddButtonExtra();
                }

                this.RenumberElements(node);
            }
        }

        private void RemoveElement()
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null && node.Level == 1)
            {
                TreeNode parent = node.Parent;
                node.Remove();
                this.RenumberElements(parent);
            }
        }

        private void TreeView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                this.AddElement();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.RemoveElement();
            }
        }

        private void SetStatus(string message)
        {
            this.statusToolStripStatusLabel.Text = message;
        }

        private void UpdateCursorPosition(Point location)
        {
            if (this.imageBox1.IsPointInImage(location))
            {
                Point point;
                point = this.imageBox1.PointToImage(location);
                this.cursorToolStripStatusLabel.Text = $"X:{point.X}, Y:{point.Y}";
            }
            else
            {
                this.cursorToolStripStatusLabel.Text = string.Empty;
            }
        }

        private void UpdateStatusBar()
        {
            this.zoomToolStripStatusLabel.Text = $"{this.imageBox1.Zoom}%";
        }

        private void FrmDialogEdit_Load(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void ImageBox1_Resize(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void ImageBox1_Scroll(object sender, ScrollEventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void ImageBox1_Selected(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void ImageBox1_SelectionMoving(object sender, CancelEventArgs e)
        {
            this.SetStatus("Press escape to cancel move.");
        }

        private void ImageBox1_SelectionRegionChanged(object sender, EventArgs e)
        {
            Rectangle rectangle = Rectangle.Truncate(this.imageBox1.SelectionRegion);
            this.selectionToolStripStatusLabel.Text = $"X:{rectangle.X}, Y:{rectangle.Y}, W:{rectangle.Width}, H:{rectangle.Height}";
        }

        private void ImageBox1_SelectionResizing(object sender, CancelEventArgs e)
        {
            this.SetStatus("Press escape to cancel resize.");
        }

        private void ImageBox1_Zoomed(object sender, Cyotek.Windows.Forms.ImageBoxZoomEventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void FrmDialogEdit_SizeChanged(object sender, EventArgs e)
        {
            this.selectionType = SelectionType.None;
            this.treeView1.SelectedNode = null;
            this.imageBox1.SelectNone();
            this.imageBox1.Invalidate();
        }
    }
}
