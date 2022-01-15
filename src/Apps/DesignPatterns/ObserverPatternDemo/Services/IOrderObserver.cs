using ObserverPatternDemo.Models;

namespace ObserverPatternDemo.Services
{
    public interface IOrderObserver
    {
        // Attach an order observer to the subject.
        void Attach(IOrderObserver observer);
 
        // Detach an order observer from the subject.
        void Detach(IOrderObserver observer);
 
        // Notify all order observers about an event.
        void Notify(Order order);
    }
}