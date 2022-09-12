using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProductReviewManagement
{
    public class ProductManagement
    {

        public readonly DataTable dataTable = new DataTable();
        /// <summary>
        /// UC2 - selected records with order by clause.
        /// </summary>
        /// <param name="review">The review.</param>
        public void TopRecords(List<ProductReview> listReview)
        {
            //using query syntax
            var recordedData = (from productReviews in listReview
                                orderby productReviews.Rating descending
                                select productReviews).Take(3);
            //using lambda syntax
            var recordData = listReview.OrderByDescending(r => r.Rating).Take(3);
            foreach (var list in recordData)
            {
                Console.WriteLine("ProductId:-" + list.ProductId + " UserId:-" + list.UserId + " Ratings:-" + list.Rating + " Review:-" + list.Review + " IsLike:-" + list.isLike);
            }
        }


        /// <summary>
        /// Selected records with where condition UC3
        /// </summary>
        /// <param name="listReview">The list review.</param>
        public void SelectedRecords(List<ProductReview> listReview)
        {
            //lambda syntax
            var recordData = listReview.Where(r => r.Rating > 3 && (r.ProductId == 1 || r.ProductId == 4 || r.ProductId == 9)).ToList();
            //query syntax
            var recordedData = (from productReviews in listReview
                                where productReviews.Rating > 3 && (productReviews.ProductId == 1 || productReviews.ProductId == 4 || productReviews.ProductId == 9)
                                select productReviews);
            foreach (var list in recordedData)
            {
                Console.WriteLine("ProductId:-" + list.ProductId + " UserId:-" + list.UserId + " Ratings:-" + list.Rating + " Review:-" + list.Review + " IsLike:-" + list.isLike);
            }
        }

        /// <summary>
        /// UC4 - counts the of reviews grouping by user id
        /// </summary>
        /// <param name="listProductReview">The list product review.</param>
        public void countOfReviews(List<ProductReview> listProductReview)
        {
            //lambda syntax
            var recordData = listProductReview.GroupBy(r => r.UserId).Select(r => new { productId = r.Key, count = r.Count() });
            //query syntax
            var recordedData = from products in listProductReview
                               group products by products.UserId into g
                               select new
                               {
                                   userId = g.Key,
                                   count = g.Count()
                               };

            foreach (var list in recordedData)
            {
                Console.WriteLine("UserId:-" + list.userId + " Count:-" + list.count);
            }
        }

        /// <summary>
        /// UC5 7 UC7 - Retrieves the product id and review.
        /// </summary>
        /// <param name="productReviewList">The product review list.</param>
        public void retrieveProductIDandreview(List<ProductReview> productReviewList)
        {
            var recordData = productReviewList.Select(r => new { r.ProductId, r.Review });
            foreach (var list in recordData)
            {
                Console.WriteLine("product Id:-" + list.ProductId + " Review :-" + list.Review);
            }
        }


        /// <summary>
        /// Skippings the records. UC6
        /// </summary>
        /// <param name="productReviewList">The product review list.</param>
        public void SkippingRecords(List<ProductReview> productReviewList)
        {
            var recordData = productReviewList.Skip(5);
            var recordedData = (from products in productReviewList
                                select products).Skip(5);
            foreach (var list in recordedData)
            {
                Console.WriteLine("ProductId:-" + list.ProductId + " UserId:-" + list.UserId + " Ratings:-" + list.Rating + " Review:-" + list.Review + " IsLike:-" + list.isLike);
            }

        }


        /// <summary>
        /// UC9 - Retrievings the records.
        /// </summary>
        /// <param name="table">The table.</param>
        public void RetrievingRecords(DataTable table)
        {
            //in where condition, need to cast is like value to string
            //query syntax
            var recordData = from products in table.AsEnumerable()
                             where (products.Field<string>("isLike") == true.ToString())
                             select products;
            //lambda syntax
            var recordedData = table.AsEnumerable().Where(r => r.Field<string>("isLike") == true.ToString());
            foreach (var list in recordedData)
            {
                Console.WriteLine("ProductId:-" + list.Field<string>("productId") + " UserId:-" + list.Field<string>("userId") + " Ratings:-" + list.Field<string>("ratings") + " Review:-" + list.Field<string>("reviews") + " IsLike:-" + list.Field<string>("isLike"));
            }
        }

        /// <summary>
        /// UC10 - Averages the rating for user identifier using data table.
        /// </summary>
        /// <param name="table">The table.</param>
        public void AverageRatingForUserIDUsingDataTable(DataTable table)
        {
            //field for data table always takes string as data type and then casted to integer.
            //used lambda syntax
            var recordData = table.AsEnumerable().GroupBy(r => r.Field<string>("userId")).Select(r => new { userid = r.Key, averageRatings = r.Average(x => Convert.ToInt32(x.Field<string>("ratings"))) });
            foreach (var list in recordData)
            {
                Console.WriteLine("user Id:-" + list.userid + " Ratings :" + list.averageRatings);
            }
        }


        /// <summary>
        /// Reviews message retrieval for average. UC11
        /// </summary>
        /// <param name="table">The table.</param>
        public void ReviewMessageRetrieval(DataTable table)
        {
            var recordData = table.AsEnumerable().Where(r => r.Field<string>("reviews") == "Average");
            foreach (var list in recordData)
            {
                //field datatype is string here for every column
                Console.WriteLine("ProductId:-" + list.Field<string>("productId") + " UserId:-" + list.Field<string>("userId") + " Ratings:-" + list.Field<string>("ratings") + " Review:-" + list.Field<string>("reviews") + " IsLike:-" + list.Field<string>("isLike"));
            }

        }
    }
}
