using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class FlowerShop
    {
        private List<IObserver> observers = new List<IObserver>();

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in observers)
            {
                observer.Update(message);
            }
        }

        public string getMessages()
        {
            foreach (var observer in observers)
            {
                return observer.getMess();
            }
            return null;
        }

        // Các phương thức khác của FlowerShop
    }
}
