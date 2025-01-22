using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImageReader
{
    public static class MnistReader
    {
        public static IEnumerable<Image> ReadData(string labelsPath, string imagesPath)
        {
            using (var labelsReader = new BinaryReader(new FileStream(labelsPath, FileMode.Open)))
            {
                using (var imagesReader = new BinaryReader(new FileStream(imagesPath, FileMode.Open)))
                {
                    foreach (var item in Read(imagesReader, labelsReader))
                    {
                        yield return item;
                    }
                }
            }
        }

        private static IEnumerable<Image> Read(BinaryReader imagesReader, BinaryReader labelsReader)
        {
            int magicNumber = imagesReader.ReadBigInt32();
            int numberOfImages = imagesReader.ReadBigInt32();
            int width = imagesReader.ReadBigInt32();
            int height = imagesReader.ReadBigInt32();

            int magicLabel = labelsReader.ReadBigInt32();
            int numberOfLabels = labelsReader.ReadBigInt32();

            for (int i = 0; i < numberOfImages; i++)
            {
                var bytes = imagesReader.ReadBytes(width * height);

                yield return new Image()
                {
                    Width = width,
                    Height = height,
                    Bytes = bytes,
                    Label = labelsReader.ReadByte()
                };
            }
        }
    }
}
