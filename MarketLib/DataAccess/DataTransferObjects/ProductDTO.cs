using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataTransferObjects
{
    internal class ProductDTO
    {
        private int pId;
        private string pName;
        private double price;
        private ReviewDTO review;
        private int storeid;


        private string category;

        //private string subcategory
        private double rating;
        private bool Opened = true;
    }
}
