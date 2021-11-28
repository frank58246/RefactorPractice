using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Movie
    {
        public const  int CHILDRENS = 2;
        public const  int REGULAR = 0;
        public const  int NEW_RELEASE = 1;

        private string _title;
        private int _priceCode;

        public Movie(string title, int priceCode)
        {
            _title = title;
            _priceCode = priceCode;
        }

        public int getPriceCode() {
            return _priceCode;
        }

        public void setPriceCode(int arg)
        {
            _priceCode = arg;
        }

        public string getTitle()
        {
            return _title;
        }
    }

    public class Rental 
    {
        private Movie _movie;
        private int _daysRented;

        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }

        public int getDaysRented()
        {
            return _daysRented;
        }

        public Movie GetMovie()
        {
            return _movie;
        }

        public  double getCharge()
        {
            double result = 0;
            switch (GetMovie().getPriceCode())
            {
                case Movie.REGULAR: // 普通片
                    result += 2;
                    if (getDaysRented() > 2)
                        result += (getDaysRented() - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE: // 新片
                    result += getDaysRented() * 3;
                    break;
                case Movie.CHILDRENS:
                    result += 1.5;
                    if (getDaysRented() > 3)
                        result += (getDaysRented() - 3) * 1.5;
                    break;
            }
            return result;
        }

        public int getFrequentRenterPoints()
        {
            if ((GetMovie().getPriceCode() == Movie.NEW_RELEASE) && getDaysRented() > 1)
            {
                return 2;
            }
            return 1;
        }
    }
   public class Customer
    {
        private string _name;
        private List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            _name = name;
            
        }

        public void addRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string getName()
        {
            return _name;
        }

        public string statement()
        {

            string result = "Rental Record for " + getName() + "\n";

            foreach (var each in _rentals)
            {
              
                // 顯示此筆租借資料
                result += "\t" + each.GetMovie().getTitle() + "\t" 
                    + each.getCharge().ToString() + "\n";
            }

            // 列印結尾
            result += "Amount owed is " + getTotalCharge() + "\n";
            result += "You earned " + getTotalFrequestRenterPoints() + " frequent renter popints";
            return result;
        }

        private double getTotalCharge()
        {
           return _rentals.Sum(x => x.getCharge());
        }

        private int getTotalFrequestRenterPoints()
        {
            return _rentals.Sum(x => x.getFrequentRenterPoints());
        }
      
    }
    
}
