using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketLib.src.StoreNS
{
    public class Product
    {
        private int pId;
        private string pName;
        private double price;
        private Review review;
        private int storeid;


        private string category;

        //private string subcategory
        private double rating;
        private bool Opened = true;

        public Product(int pid, string name, double price, string category, double rating)
        {
            this.pId = pid;
            this.pName = name;
            this.price = price;
            this.category = category;
            this.rating = rating;
        }

        public Product()
        {
        }

        public override string ToString(){ 
            return "\tProduct name: "+pName+", price: "+price;
        }

        public int ProductId
        {
            get => pId;
        }

        public string ProductName
        {
            get => pName;
        }

        public double Price
        {
            get => price;
            set => price = value;
        }

        public string Category
        {
            get => category;
        }

        public double Rating
        {
            get => rating;
            set => rating = value;
        }

        public void unlockstore()
        {
            this.Opened = false;
        }



        public void lockstore()
        {
            this.Opened = true;
        }

        public bool isOpened()
        {
            return Opened;
        }

        // public void addReview(Review review)
        // {
        //     reviews.add(review);
        // }
    }
}
