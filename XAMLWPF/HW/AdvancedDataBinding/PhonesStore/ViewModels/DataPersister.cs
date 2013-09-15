using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace PhonesStore.ViewModels
{
    public class DataPersister
    {
        public static IEnumerable<StoreViewModel> GetStores(string phonesStoreDocumentPath)
        {
            var phonesDocumentRoot = XDocument.Load(phonesStoreDocumentPath).Root;
            

            var phonesVMs = from storeElement in phonesDocumentRoot.Elements("store")
                            select new StoreViewModel()
                            {
                               Name = storeElement.Element("name").Value,
                               Phones =
                               from phoneElement in storeElement.Element("phones").Elements("phone")
                               select new PhoneViewModel()
                               {
                                   Vendor = phoneElement.Element("vendor").Value,
                                   Model = phoneElement.Element("model").Value,
                                   Year = int.Parse(phoneElement.Element("year").Value),
                                   ImagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), phoneElement.Element("image").Value),
                                   OS = new OperatingSystemViewModel()
                                   {
                                       Name = phoneElement.Element("os").Element("name").Value,
                                       Manufacturer = phoneElement.Element("os").Element("manufacturer").Value,
                                       Version = phoneElement.Element("os").Element("version").Value,
                                   }
                               }
                        };

            return phonesVMs;
        }

        internal static void AddStore(string documenPath, StoreViewModel store)
        {
            var root = XDocument.Load(documenPath).Root;
            root.Add(new XElement("store",
                new XElement("name", store.Name),
                new XElement("phones", null)));
            root.Document.Save(documenPath);
        }

        internal static void RemoveStore(string documenPath, StoreViewModel store)
        {
            var root = XDocument.Load(documenPath).Root;
            foreach (var storeChild in root.Elements("store"))
            {
                if (storeChild.Element("name").Value.ToLower()==store.Name.ToLower())
                {
                    storeChild.Remove();
                    root.Document.Save(documenPath);
                    break;
                }
            }
        }

        internal static void SaveStores(string documenPath, IEnumerable<StoreViewModel> stores)
        {
            var root = XDocument.Load(documenPath).Root;
            root.RemoveNodes();
            foreach (var store in stores)
            {
                var phonesRootElement = new XElement("phones", null);
                foreach (var phone in store.Phones)
                {
                    phonesRootElement.Add(new XElement("phone",
                new XElement("vendor", phone.Vendor),
                new XElement("model", phone.Model),
                new XElement("year", phone.Year),
                new XElement("image", phone.ImagePath),
                new XElement("os",
                        new XElement("name", phone.OS.Name),
                        new XElement("version", phone.OS.Name),
                        new XElement("manufacturer", phone.OS.Manufacturer))));
                }


                root.Add(new XElement("store",
                new XElement("name", store.Name),
                phonesRootElement
                ));
            }
            root.Document.Save(documenPath);
        }

        internal static IEnumerable<OperatingSystemViewModel> GetAllOperationalSystems(string phonesStoreDocumentPath)
        {
            var phonesDocumentRoot = XDocument.Load(phonesStoreDocumentPath).Root;

            var osVms =
                from storeElement in phonesDocumentRoot.Elements("store")
                from phoneElement in storeElement.Element("phones").Elements("phone")
                       select new OperatingSystemViewModel()
                       {
                           Name = phoneElement.Element("os").Element("name").Value,
                           Manufacturer = phoneElement.Element("os").Element("manufacturer").Value,
                           Version = phoneElement.Element("os").Element("version").Value,
                       };
            return osVms.Union(new List<OperatingSystemViewModel>());
        }

        internal static void AddPhone(string documenPath, PhoneViewModel phone, string storeName)
        {
            var root = XDocument.Load(documenPath).Root;
            XElement storeElement = null;
            foreach (var store in root.Elements("store"))
            {
                if (store.Element("name").Value.ToLower()==storeName.ToLower())
                {
                    storeElement = store.Element("phones");
                    break;
                }
            }
            if (storeElement==null)
            {
                throw new ArgumentException(String.Format("Store with name {0} does not exist", storeName));
            }
            storeElement.Add(new XElement("phone",
                new XElement("vendor", phone.Vendor),
                new XElement("model", phone.Model),
                new XElement("year", phone.Year),
                new XElement("image", phone.ImagePath),
                new XElement("os",
                        new XElement("name", phone.OS.Name),
                        new XElement("version", phone.OS.Name),
                        new XElement("manufacturer", phone.OS.Manufacturer))));
            root.Document.Save(documenPath);
        }

        public static IEnumerable<PhoneViewModel> GetPhones(string phonesStoreDocumentPath, string storeName)
        {
            var root = XDocument.Load(phonesStoreDocumentPath).Root;
            XElement storeElement = null;
            foreach (var store in root.Elements("store"))
            {
                if (store.Element("name").Value.ToLower() == storeName.ToLower())
                {
                    storeElement = store.Element("phones");
                    break;
                }
            }
            if (storeElement == null)
            {
                throw new ArgumentException(String.Format("Store with name {0} does not exist", storeName));
            }

            //var phonesDocumentRoot = XDocument.Load(phonesStoreDocumentPath).Root;

            var phonesVMs =
                           from phoneElement in storeElement.Elements("phone")
                           select new PhoneViewModel()
                           {
                               Vendor = phoneElement.Element("vendor").Value,
                               Model = phoneElement.Element("model").Value,
                               Year = int.Parse(phoneElement.Element("year").Value),
                               ImagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), phoneElement.Element("image").Value),
                               OS = new OperatingSystemViewModel()
                               {
                                   Name = phoneElement.Element("os").Element("name").Value,
                                   Manufacturer = phoneElement.Element("os").Element("manufacturer").Value,
                                   Version = phoneElement.Element("os").Element("version").Value,
                               }
                           };
            return phonesVMs;
        }

        internal static void SavePhones(string documenPath, IEnumerable<PhoneViewModel> phones, string storeName)
        {
            var root = XDocument.Load(documenPath).Root;
            XElement storeElement = null;
            foreach (var store in root.Elements("store"))
            {
                if (store.Element("name").Value.ToLower() == storeName.ToLower())
                {
                    storeElement = store.Element("phones");
                    break;
                }
            }
            if (storeElement == null)
            {
                throw new ArgumentException(String.Format("Store with name {0} does not exist", storeName));
            }

            //var phonesRoot = storeElement.Element("phones");
            storeElement.RemoveNodes();
            foreach (var phone in phones)
            {
                storeElement.Add(new XElement("phone",
                new XElement("vendor", phone.Vendor),
                new XElement("model", phone.Model),
                new XElement("year", phone.Year),
                new XElement("image", phone.ImagePath),
                new XElement("os",
                        new XElement("name", phone.OS.Name),
                        new XElement("version", phone.OS.Name),
                        new XElement("manufacturer", phone.OS.Manufacturer))));
            }
            root.Document.Save(documenPath);
        }


        internal static void RemovePhone(string documenPath, PhoneViewModel phone, string storeName)
        {
            var root = XDocument.Load(documenPath).Root;
            XElement storeElement = null;
            foreach (var store in root.Elements("store"))
            {
                if (store.Element("name").Value.ToLower() == storeName.ToLower())
                {
                    storeElement = store.Element("phones");
                    break;
                }
            }
            if (storeElement == null)
            {
                throw new ArgumentException(String.Format("Store with name {0} does not exist", storeName));
            }


            foreach (var phoneElement in storeElement.Elements("phone"))
            {
                if (phoneElement.Element("vendor").Value.ToLower() == phone.Vendor.ToLower()&&
                    phoneElement.Element("model").Value.ToLower() == phone.Model.ToLower()&&
                    phoneElement.Element("year").Value.ToLower() == phone.Year.ToString().ToLower())
                {
                    phoneElement.Remove();
                    root.Document.Save(documenPath);
                    break;
                }
            }
        }
    }
}