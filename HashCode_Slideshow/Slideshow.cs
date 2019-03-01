using System;
using System.Collections.Generic;
using System.Linq;

namespace HashCode_Slideshow
{
    public class Slideshow
    {
        public Slideshow(List<Photo> photos)
        {
            this.photos = photos;
        }

        public List<Photo> GetPath()
        {
            List<Photo> path = new List<Photo>();            
            Photo p1 = photos[0];

            while (photos.Count != 0)
            {
                Console.WriteLine("Photos remaining: " + photos.Count);

                Photo p2 = null;
                int bestCost = -1;
                
                for (int j = 0; j < photos.Count; j++)
                {
                    Photo current = photos[j];
                    if (p1.Index == current.Index)
                    {
                        continue;
                    }

                    int cost = GetCost(p1, current);
                    if (cost > bestCost)
                    {
                        p2 = current;
                        bestCost = cost;
                    }
                }

                path.Add(p1);
                photos.Remove(p1);

                p1 = p2;
            }

            return path;
        }

        private int GetCost(Photo p1, Photo p2)
        {
            int tagsInP1ButNotInP2 = p1.Tags.Where(t => !p2.Tags.Any(t1 => t == t1)).Count();
            int tagsInP2ButNotInP1 = p2.Tags.Where(t => !p1.Tags.Any(t1 => t == t1)).Count();
            int tagsOnBoth = p1.Tags.Intersect(p2.Tags).Count();

            return Math.Min(tagsInP1ButNotInP2, Math.Min(tagsInP2ButNotInP1, tagsOnBoth));
        }

        private List<Photo> photos;
    }
}
