using Gallery.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Gallery.ViewModels
{
    public class AlbumViewModel
    {
        public IEnumerable<ImageViewModel> Images { get; set; }
        public string Name { get; set; }

        private ICommand prevCommand;
        private ICommand nextCommand;

        private void HandleExecutePrevCommand(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(this.Images);
            view.MoveCurrentToPrevious();
            if (view.IsCurrentBeforeFirst)
            {
                view.MoveCurrentToLast();
            }
        }
        private void HandleExecuteNextCommand(object obj)
        {
            var view = CollectionViewSource.GetDefaultView(this.Images);
            view.MoveCurrentToNext();
            if (view.IsCurrentAfterLast)
            {
                view.MoveCurrentToFirst();
            }
        }

        public ICommand Prev
        {
            get
            {
                if (this.prevCommand == null)
                {
                    this.prevCommand = new RelayCommand(
                        this.HandleExecutePrevCommand);
                }
                return this.prevCommand;
            }
        }

        public ICommand Next
        {
            get
            {
                if (this.nextCommand == null)
                {
                    this.nextCommand = new RelayCommand(
                        this.HandleExecuteNextCommand);
                }
                return this.nextCommand;
            }
        }
    }
}
