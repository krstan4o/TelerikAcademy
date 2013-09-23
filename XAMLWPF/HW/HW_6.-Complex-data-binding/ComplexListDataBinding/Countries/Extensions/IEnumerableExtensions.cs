using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Countries.Extensions
{
    public static class IEnumerableExtensions
    {
        public static ICollectionView GetDefaultView<T>(this IEnumerable<T> collection)
        {
            return CollectionViewSource.GetDefaultView(collection);
        }
    }
}
