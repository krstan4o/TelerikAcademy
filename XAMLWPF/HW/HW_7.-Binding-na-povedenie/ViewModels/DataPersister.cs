using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ViewModels
{
    public class DataPersister
    {
        public static IEnumerable<AlbumViewModel> GetAll(string galleryPath)
        {
            var galleryDocumentRoot = XDocument.Load(galleryPath).Root;
            var albums =
                            from album in galleryDocumentRoot.Elements("album")
                            select new AlbumViewModel()
                            {
                                Name = album.Element("name").Value,
                                ImagesEnum = from image in album.Element("images").Elements("image")
                                         select new ImageViewModel()
                                         {
                                             Title = image.Element("title").Value,
                                             Source = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), image.Element("source").Value)
                                         }
                            };
            return albums;
        }

        public static void AddNewImage(string filename, string title, string albumName, string galleryPath)
        {
            var galleryDocumentRoot = XDocument.Load(galleryPath).Root;

            var albumFound =
                (from album in galleryDocumentRoot.Elements("album")
                 where album.Element("name").Value == albumName
                 select album.Element("images")).FirstOrDefault();

            if (albumFound != null)
            {
                var newImage = new XElement("image",
                    new XElement("title", title),
                    new XElement("source", filename));

                albumFound.Add(newImage);
            }

            galleryDocumentRoot.Save(galleryPath);
        }
    }
}
