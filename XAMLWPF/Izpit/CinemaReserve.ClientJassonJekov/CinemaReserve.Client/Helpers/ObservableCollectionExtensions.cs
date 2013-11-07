using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReserve.Client.Helpers
{
    using System.Collections.ObjectModel;

    internal static class ObservableCollectionExtensions
    {
        public static void UpdateWith<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
