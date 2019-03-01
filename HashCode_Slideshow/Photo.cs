using System.Collections.Generic;
using System.Linq;

namespace HashCode_Slideshow
{
    public enum PhotoType
    {
        Horizontal,
        Vertical
    }

    public class Photo
    {
        public List<string> Tags { get; set; }
        public PhotoType Type { get; set; }
        public int Index { get; set; }

        public Photo(int index, List<string> tags, PhotoType type)
        {
            this.Index = index;
            this.Type = type;
            this.Tags = tags;
        }
    }

    public class HorizontalPhoto : Photo
    {
        public HorizontalPhoto(int index, List<string> tags) : base(index, tags, PhotoType.Horizontal)
        {

        }
    }

    public class VerticalPhoto : Photo
    {
        public VerticalPhoto(int index, List<string> tags) : base(index, tags, PhotoType.Vertical)
        {

        }
    }

    public class GroupedPhoto : Photo
    {
        public VerticalPhoto LeftPhoto { get; set; }
        public VerticalPhoto RightPhoto { get; set; }

        public GroupedPhoto(VerticalPhoto leftPhoto, VerticalPhoto rightPhoto) : base(leftPhoto.Index, leftPhoto.Tags.Concat(rightPhoto.Tags).Distinct().ToList(), PhotoType.Horizontal)
        {
            LeftPhoto = leftPhoto;
            RightPhoto = rightPhoto;
        }
    }
}
