using Aspose.Drawing;

namespace ImageReader
{
    public class Image
    {
        public int Label { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Bytes { get; set; }

        public void SaveToBitmap(int guess, string path)
        {
            var bmp = new Bitmap(Width, Height);

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var color = Bytes[i * Height + j];
                    bmp.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }

            bmp.Save($"{path}{Label}_{guess}_{DateTime.UtcNow.Ticks}.bmp");
        }

        public double[] GenerateNetworkInput()
        {
            return Bytes.Select(b => (double)b / byte.MaxValue).ToArray();
        }

        public double[] GenerateBlackAndWhiteNetworkInput()
        {
            return Bytes.Select(b => b > byte.MaxValue / 2 ? 1.0 : 0.0).ToArray();
        }
    }

    public static class Extensions
    {
        public static int ReadBigInt32(this BinaryReader br)
        {
            var bytes = br.ReadBytes(sizeof(Int32));
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static void ForEach<T>(this T[,] source, Action<int, int> action)
        {
            for (int w = 0; w < source.GetLength(0); w++)
            {
                for (int h = 0; h < source.GetLength(1); h++)
                {
                    action(w, h);
                }
            }
        }
    }
}
