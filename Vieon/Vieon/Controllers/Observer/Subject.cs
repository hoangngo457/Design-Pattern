using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vieon.Models;

namespace Vieon.Controllers.Observer
{
    public class Subject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(PhimYeuThich phimYeuThich, TapPhim tapPhim)
        {
            foreach (var observer in observers)
            {
                observer.Update(phimYeuThich, tapPhim);
            }
        }
    }
}