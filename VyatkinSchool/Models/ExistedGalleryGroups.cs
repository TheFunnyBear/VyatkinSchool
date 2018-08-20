using System.Collections.Generic;

namespace VyatkinSchool.Models
{

    public class ExistedGalleryGroups
    {
        public ExistedGalleryGroups()
        {
            Groups = new List<GalleryGroup>();
            Succsessful = true;
            AlreadyCreated = false;
        }

        public List<GalleryGroup> Groups { get; set; }
        public bool AlreadyCreated { get; set; }
        public bool Succsessful { get; set; }
        public GalleryGroup CreatedGrop { get; set; }
    }
}