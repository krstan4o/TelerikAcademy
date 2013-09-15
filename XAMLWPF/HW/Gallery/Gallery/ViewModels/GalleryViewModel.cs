using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.ViewModels
{
    public class GalleryViewModel
    {
        public IEnumerable<AlbumViewModel> Albums { get; set; }

        public GalleryViewModel()
        {
            this.Albums = DataPersister.GetFromXML("..\\..\\ImageGallery.xml");
        }
    }
}
