using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using ViewModels.Behavior;

namespace ViewModels
{
    public class AlbumViewModel : INotifyPropertyChanged
    {
        private ICommand prevImage;

        private ICommand nextImage;

        private ICommand addImage;

        private ObservableCollection<ImageViewModel> images;

        private ImageViewModel currentImage;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }

        public ImageViewModel CurrentImage
        {
            get
            {
                if (this.currentImage == null)
                {
                    var imagesCollectionView = CollectionViewSource.GetDefaultView(this.Images);
                    this.currentImage = imagesCollectionView.CurrentItem as ImageViewModel;
                }
                return this.currentImage;
            }

            set
            {
                this.currentImage = value;
                this.OnPropertyChanged("CurrentImage");
            }
        }

        public IEnumerable<ImageViewModel> ImagesEnum { get; set; }

        public ObservableCollection<ImageViewModel> Images
        {
            get
            {
                if (this.images == null)
                {
                    this.images = new ObservableCollection<ImageViewModel>();
                    foreach (var image in ImagesEnum)
                    {
                        this.images.Add(image);
                    }
                }

                return images;
            }
        }

        public ICommand PrevImage
        {
            get
            {
                if (this.prevImage == null)
                {
                    this.prevImage = new RelayCommand(this.HandlePreviousCommand);
                }
                return this.prevImage;
            }
        }

        public ICommand NextImage
        {
            get
            {
                if (this.nextImage == null)
                {
                    this.nextImage = new RelayCommand(this.HandleNextCommand);
                }
                return this.nextImage;
            }
        }

        public ICommand AddImage
        {
            get
            {
                if (this.addImage == null)
                {
                    this.addImage = new RelayCommand(HandleAddCommand);
                }

                return addImage;
            }
        }

        protected void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(p));
            }
        }

        private void HandlePreviousCommand(object obj)
        {
            var imagesCollectionView = CollectionViewSource.GetDefaultView(this.Images);
            imagesCollectionView.MoveCurrentToPrevious();
            if (imagesCollectionView.IsCurrentBeforeFirst)
            {
                imagesCollectionView.MoveCurrentToLast();
            }

            var current = imagesCollectionView.CurrentItem as ImageViewModel;
            this.CurrentImage = current;
        }

        private void HandleNextCommand(object obj)
        {
            var imagesCollectionView = CollectionViewSource.GetDefaultView(this.Images);
            imagesCollectionView.MoveCurrentToNext();
            if (imagesCollectionView.IsCurrentAfterLast)
            {
                imagesCollectionView.MoveCurrentToFirst();
            }

            var current = imagesCollectionView.CurrentItem as ImageViewModel;
            this.CurrentImage = current;
        }

        private void HandleAddCommand(object obj)
        {
            var image = obj as ImageViewModel;
            if (image == null)
            {
                return;
            }

            DataPersister.AddNewImage(image.Source, image.Title, this.Name, "..\\..\\..\\ViewModels\\gallery.xml");
            this.images.Add(image);
        }
    }
}
