using System;

namespace Countries.ViewModels
{
    public interface INotifySelectedCountry
    {
        event EventHandler<string> SelectedCountry;
    }
}
