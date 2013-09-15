using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gallery.ViewModels
{
    public class DataPersister
    {
        public static IEnumerable<AlbumViewModel> GetFromXML(string xmlPath)
        {
            XDocument doc = XDocument.Load(xmlPath);
            var root = doc.Root;

            var gallery =
                from album in root.Elements("album")
                select new AlbumViewModel()
                {
                    Name = album.Element("name").Value,
                    Images =
                        from image in album.Element("images").Elements("image")
                        select new ImageViewModel()
                        {
                            Title = image.Element("title").Value,
                            ImageSource = image.Element("imageSource").Value
                        }
                };

            return gallery;
        }
    }
}
