using System;
using System.Threading.Tasks;
using ObserverPatternDemo.Models;

namespace ObserverPatternDemo.Services
{
    public delegate Task Notify(Order order);
    
    public class PaymentService
    {
        public event Notify OnPaymentComplete;
        
        public void ProcessPayment()
        {
            Console.WriteLine("Processing Payment");


            var order = new Order
            {
                PaymentNo = "ABC123",
                TotalPayment = 123.34m,
            };
            
            OnPaymentComplete?.Invoke(order);
        }
        
    }
}