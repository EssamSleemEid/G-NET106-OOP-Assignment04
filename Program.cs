namespace G_NET106_OOP_Assignment04
{
    internal class Program
    {
        public struct DeliveryAddress
        {
            public string City;
            public string Street;
            public int BuldingNumber;
            public DeliveryAddress(string city, string street, int buldingNumber)
            {
                City = city;
                Street = street;
                BuldingNumber = buldingNumber;
            }
            public string GetFullAddress()
            {
                return $"bulding number : {BuldingNumber}, street : {Street}, city : {City}";
            }
        }
        public interface ITrackable
        {
            string GetTrackingStatus();
        }

        public abstract class Shipment : ITrackable
        {
            private string description;
            private decimal weight;
            private decimal deliveryFee;
            private string trackingCode;
            public DeliveryAddress Destination { get; set; }
            public string TrackingCode
            {
                get { return trackingCode; }
            }
            public string Description
            {
                get { return description; }

                set
                {
                    if (value != null)
                    {
                        description = value;
                    }
                }
            }
            public decimal Weight
            {
                get { return weight; }

                set
                {
                    if (value > 0)
                    {
                        weight = value;
                    }
                }
            }
            public decimal DeliveryFee
            {
                get { return deliveryFee; }

                set
                {

                    if (value > 0)
                    {
                        deliveryFee = value;
                    }
                }
            }
            public abstract decimal EstimatedCost
            {
                get;
            }
            
            public abstract void PrintShipment();

            public abstract string GetTrackingStatus();


            public Shipment(string trackingCode)
            {
                this.trackingCode = trackingCode == null ? "unknown" : trackingCode;

                description = "unknown";
                weight = 1;
                deliveryFee = 50;
                Destination = new DeliveryAddress("Cairo", "Unknown Street", 0);
            }
            public Shipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
            {
                this.trackingCode = trackingCode == null ? "Unknown" : trackingCode;
                this.description = description == null ? "Unknown" : description;
                this.weight = weight > 0 ? weight : 1;
                this.deliveryFee = deliveryFee > 0 ? deliveryFee : 50;
                Destination = destination;
            }
            public void UpdateDeliveryFee(decimal newFee)
            {
                if (newFee > 0)
                {
                    deliveryFee = newFee;
                }
            }
            public void UpdateWeight(decimal newWeight)
            {
                if (newWeight > 0)
                {
                    Weight = newWeight;
                }
            }
            public void UpdateWeight(decimal newWeight, decimal packingWeight)
            {
                if (newWeight > 0 && packingWeight >= 0)
                {
                    Weight = newWeight + packingWeight;
                }
            }
        }

        public class StandardShipment : Shipment
        {
            public StandardShipment(string description,decimal weight,decimal deliveryFee,string trackingCode,DeliveryAddress Destination): base(trackingCode,description,weight,deliveryFee,Destination)
            {

            }
            public override decimal EstimatedCost
            {
                get
                {
                    return DeliveryFee + (decimal)(Weight * 5);
                }
            }

            public override void PrintShipment()
            {
                Console.WriteLine("Standard Shipment");
                Console.WriteLine("Tracking Code : " + TrackingCode);
                Console.WriteLine("Description : " + Description);
                Console.WriteLine("Weight : " + Weight + " kg");
                Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
                Console.WriteLine("Destination : " + Destination.GetFullAddress());
                Console.WriteLine("Estimated Cost : " + EstimatedCost + " EGP");
            }

            public override string GetTrackingStatus()
            {
                return $"Shipment {TrackingCode} has been Delivered.";
            }
        }

        public class ExpressShipment : Shipment
        {
            private decimal ExtraFee;

            public decimal extrafee
            {
                get { return ExtraFee; }

                set
                {
                    if (value >= 0)
                    {
                        ExtraFee = value;
                    }
                }
            }

            public override decimal EstimatedCost
            {
                get { return DeliveryFee + (decimal)(Weight * 5) + ExtraFee; }
            }

            public ExpressShipment(string description, decimal weight, decimal deliveryFee, string trackingCode, DeliveryAddress Destination, decimal ExtraFee) : base(trackingCode, description, weight, deliveryFee, Destination)
            {
                extrafee = ExtraFee;
            }
            public override void PrintShipment()
            {
                Console.WriteLine("Express Shipment");
                Console.WriteLine("Tracking Code : " + TrackingCode);
                Console.WriteLine("Description : " + Description);
                Console.WriteLine("Weight : " + Weight + " kg");
                Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
                Console.WriteLine("Extra Fee : " + ExtraFee + " EGP");
                Console.WriteLine("Destination : " + Destination.GetFullAddress());
                Console.WriteLine("Estimated Cost : " + EstimatedCost + " EGP");
            }

            public override string GetTrackingStatus()
            {
                return $"Shipment {TrackingCode} is Out for Delivery.";
            }
        }

        public class InternationalShipment : Shipment
        {
            private string DestinationCountry;
            private decimal CustomerFee;

            public string destinationCountry
            {
                get { return DestinationCountry; }

                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        DestinationCountry = value;
                    }
                }
            }

            public decimal customerFee
            {
                get { return CustomerFee; }

                set
                {
                    if (value >= 0)
                    {
                        CustomerFee = value;
                    }
                }
            }

            public override decimal EstimatedCost
            {
                get { return DeliveryFee + (decimal)(Weight * 5) + customerFee; }
            }

            public InternationalShipment(string description, decimal weight, decimal deliveryFee, string trackingCode, DeliveryAddress Destination, string DestinationCountry, decimal CustomerFee) : base(trackingCode, description, weight, deliveryFee, Destination)
            {
                destinationCountry = DestinationCountry;
                customerFee = CustomerFee;
            }

            public virtual void GenerateCustomsReport()
            {
                Console.WriteLine("Customs Report Generated.");
            }

            public override void PrintShipment()
            {
                Console.WriteLine("International Shipment");
                Console.WriteLine("Tracking Code : " + TrackingCode);
                Console.WriteLine("Description : " + Description);
                Console.WriteLine("Weight : " + Weight + " kg");
                Console.WriteLine("Delivery Fee : " + DeliveryFee + " EGP");
                Console.WriteLine("Destination : " + Destination.GetFullAddress());
                Console.WriteLine("Destination Country : " + DestinationCountry);
                Console.WriteLine("Customs Fee : " + CustomerFee + " EGP");
                Console.WriteLine("Estimated Cost : " + EstimatedCost + " EGP");
            }

            public override string GetTrackingStatus()
            {
                return $"Shipment {TrackingCode} is Ready.";
            }
        }
        static void Main(string[] args)
        {
            #region Part01
            #region Question01
            //a)  What is Abstraction in Object-Oriented Programming?

            //it is a process of hiding unnecessary implementation details and show only the essential feature of the object

            //b)  Why is abstraction considered one of the four pillars of OOP?

            //bc it helps  hide the implementation details and reduce complexity
            #endregion

            #region Question02
            //a)  What is the difference between an Abstract Class and an Interface?

            /*
             abstract: class can contain fields and properties and methods

             interface: define a contract that class must implement
            */

            //b)  When would you choose an Interface instead of an Abstract Class?

            //when different unrelated classes need to follow the same behavior

            //c)  Can a class inherit from multiple abstract classes? Can it implement multiple interfaces?

            //for the abstract no it can not and for the interface it can implement from multiple interfaces
            #endregion

            #endregion

            #region Part02

            #endregion
        }
    }
}
