using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode_Slideshow
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = args[0];
            string outputPath = args[1];

            List<Photo> photos = ReadFromFile(inputPath);
            List<Photo> allHorizontalPhotos = new List<Photo>();

            // Group vertical photos two by two
            for (int i = 0; i < photos.Count; i++)
            {
                if (photos[i].Type == PhotoType.Horizontal)
                {
                    allHorizontalPhotos.Add(photos[i]);
                }
                else
                {
                    // Find the next vertical photo
                    for (int j = i + 1; j < photos.Count; j++)
                    {
                        if (photos[j].Type == PhotoType.Vertical)
                        {
                            GroupedPhoto groupedPhoto = new GroupedPhoto(photos[i] as VerticalPhoto, photos[j] as VerticalPhoto);
                            allHorizontalPhotos.Add(groupedPhoto);

                            photos.RemoveAt(j);
                            j = photos.Count;
                        }
                    }
                }
            }

            Slideshow slideshow = new Slideshow(allHorizontalPhotos);
            List<Photo> output = slideshow.GetPath();

            WriteToFile(output, outputPath);

            Console.WriteLine("Ok");
            Console.ReadLine();
        }

        static List<Photo> ReadFromFile(string path)
        {
            List<Photo> photos = new List<Photo>();
            StreamReader reader = new StreamReader(path);

            // First line is header
            string[] firstLine = reader.ReadLine().Split(' ');

            int index = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                char type = line[0];

                List<string> tags = line.Split(' ').ToList();
                tags.RemoveRange(0, 2);

                if (type == 'H')
                {
                    photos.Add(new HorizontalPhoto(index++, tags));
                }
                else
                {
                    photos.Add(new VerticalPhoto(index++, tags));
                }
            }

            return photos;
        }

        static void WriteToFile(List<Photo> photos, string outputPath)
        {
            StreamWriter writer = new StreamWriter(outputPath);

            writer.WriteLine(photos.Count);
            foreach (Photo photo in photos)
            {
                string s = "";
                if (photo.GetType() == typeof(HorizontalPhoto))
                {
                    s += photo.Index;
                }
                else if (photo.GetType() == typeof(GroupedPhoto))
                {
                    GroupedPhoto p = (GroupedPhoto)photo;
                    s += $"{p.LeftPhoto.Index} {p.RightPhoto.Index}";
                }

                writer.WriteLine(s);
            }

            writer.Close();
        }
    }
}
