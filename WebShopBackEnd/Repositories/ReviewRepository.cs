using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;

namespace WebShopBackEnd.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public void createReview(Review review)
        {
            if (review == null)
            {
                   throw new ArgumentNullException(nameof(review));
            }
            _context.Reviews.Add(review);
        }

        public void deleteReview(int ReviewId)
        {
          var review = _context.Reviews.FirstOrDefault(r => r.ReviewId== ReviewId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
            }
            else throw new ArgumentNullException(nameof(review));

        }

        public Review getReviewById(int ReviewId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.ReviewId == ReviewId);
            if (review != null)
            {
                return review;
            }
            else  throw new ArgumentNullException(nameof(review));
        }

        public ICollection<Review> getReviews()
        {
           return  _context.Reviews.ToList();
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() <= 0);   
        }

        public void updateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
