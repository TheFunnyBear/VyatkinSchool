using System.Collections.Generic;
using System.Linq;
using VyatkinSchool.Infrastructure;

namespace VyatkinSchool.Models
{
    public sealed class GalleryPage
    {
        public List<GalleryData> GalleryItems { get; set; }
        public List<GalleryGroup> GalleryGroup { get; set; }
        public int SelectedPaginationIndex { get; set; }
        public List<GalleryGroup> GroupsWithPicturesOnly
        {
            get
            {
                return GalleryGroup.Where(group => GalleryItems
                                .Any(galleryItem => galleryItem.GalleryGroupID == group.ID &&
                                galleryItem.IsVisible == true &&
                                galleryItem.IsVideo == false)).ToList();
            }
        }
        public GalleryGroup SelectedPaginationGroup
        {
            get
            {
                return GroupsWithPicturesOnly.ElementAt(SelectedPaginationIndex);
            }
        }

        public List<GalleryData> GetGalleryItems(int groupId)
        {
            return GalleryItems
                            .Where(galleryItem => galleryItem.GalleryGroupID == groupId)
                            .Where(galleryItem => galleryItem.IsVisible == true)
                            .Where(galleryItem => galleryItem.IsVideo == false).ToList();
        }

        public int GetGalleryItemsCount(int groupId)
        {
            return GalleryItems
                            .Where(galleryItem => galleryItem.GalleryGroupID == groupId)
                            .Where(galleryItem => galleryItem.IsVisible == true)
                            .Where(galleryItem => galleryItem.IsVideo == false).Count();
        }

        public GalleryData GetGalleryItem(int groupId, int index)
        {
            var galleryItems = GetGalleryItems(groupId);
            return galleryItems.ElementAt(index);
        }

        public List<GalleryData> SelectedPaginationGalleryItems
        {
            get
            {
                return GalleryItems
                            .Where(galleryItem => galleryItem.GalleryGroupID == SelectedPaginationGroup.ID)
                            .Where(galleryItem => galleryItem.IsVisible == true)
                            .Where(galleryItem => galleryItem.IsVideo == false).ToList(); 
            }
        }

        public GalleryPage()
        {
            SelectedPaginationIndex = 0;
            GalleryItems = new List<GalleryData>();
            GalleryGroup = new List<GalleryGroup>();

            using (var dataBase = new VyatkinSchoolDbContext())
            {
                GalleryGroup.AddRange(dataBase.GalleryGroup);
                GalleryItems.AddRange(dataBase.Gallery);
            }
        }



    }
}