using System;
using System.Collections.Generic;

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
            double totalAmount = 0;
            int frequentRenterPoints = 0;

            string result = "Rental Record for " + getName() + "\n";

            foreach (var each in _rentals)
            {
                double thisAmount = 0;
                switch (each.GetMovie().getPriceCode())
                {
                    case Movie.REGULAR: // 普通片
                        thisAmount += 2;
                        if (each.getDaysRented() > 2)
                            thisAmount += (each.getDaysRented() - 2) * 1.5;
                        break;
                    case Movie.NEW_RELEASE: // 新片
                        thisAmount += each.getDaysRented() * 3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (each.getDaysRented() > 3)
                            thisAmount += (each.getDaysRented() - 3) * 1.5;
                        break;                      
                }

                // 累加常客積點
                frequentRenterPoints++;
                // add bonus
                if ((each.GetMovie().getPriceCode() == Movie.NEW_RELEASE) &&
                    each.getDaysRented() > 1)
                {
                    frequentRenterPoints++;
                }

                // 顯示此筆租借資料
                result += "\t" + each.GetMovie().getTitle() + "\t" 
                    + thisAmount.ToString() + "\n";
                totalAmount += thisAmount;                
            }

            // 列印結尾
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter popints";
            return result;
        }
    }
    
}
