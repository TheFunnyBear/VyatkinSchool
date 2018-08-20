﻿using System;

namespace VyatkinSchool.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class GalleryManager
    {
        public List<GalleryGroup> GalleryGroups { get; set; }
        public GalleryData GalleryData { get; set; }
        public int SelectetGalleryGroup { get; set; }

        public GalleryManager()
        {
            GalleryGroups = new List<GalleryGroup>();
            GalleryData = new GalleryData();
            SelectetGalleryGroup = -1;
            using (var dataBase = new GalleryManagerEntities())
            {
                GalleryGroups.AddRange(dataBase.GalleryGroup.ToList());
            }
        }

        public SelectList GetSelectList()
        {
            return new SelectList(GalleryGroups, "ID", "GroupName");

            /*
            foreach(var galleryGroup in GalleryGroups)
            {
                result.Add(new SelectListItem() { Text = galleryGroup.GroupName, Value = galleryGroup.ID.ToString() });
            }
            
            return result;*/
        }
    }
}