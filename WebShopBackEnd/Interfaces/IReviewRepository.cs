using WebShopBackEnd.Models;

namespace WebShopBackEnd.Interfaces
{
    public interface IReviewRepository
    {

         ICollection<Review> getReviews();
         Review getReviewById(int ReviewId);
         void createReview(Review review);    
         void updateReview(Review review);
         void deleteReview(int ReviewId);
         bool saveChanges();
    }
}
