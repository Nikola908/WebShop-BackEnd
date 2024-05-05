using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopBackEnd.Interfaces;
using WebShopBackEnd.Models;
using WebShopBackEnd.Repositories;

namespace WebShopBackEnd.Controllerss
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        readonly IReviewRepository _reviewRepository;
        readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Review>> getReviews()
        {
            return Ok(_reviewRepository.getReviews());
        }

        [HttpGet("ReviewId")]
        public ActionResult<Review> getReviewById(int ReviewId)
        {
            var review = _reviewRepository.getReviewById(ReviewId);
            if (review == null)
            {
                return NotFound();
            }

            else { return Ok(review); }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Review> createReview(Review review)
        {
 
                _reviewRepository.createReview(review);
                _reviewRepository.saveChanges();
                return review;
           
            
        }

        [HttpPut]
        [AllowAnonymous]
        public ActionResult<Review> updateReview(Review review)
        {
            var oldReview = _reviewRepository.getReviewById(review.ReviewId);
            if (oldReview == null)
            {
                return NotFound();
            }
                Review newReview = _mapper.Map<Review>(review);
                _mapper.Map(newReview, oldReview);
                _reviewRepository.saveChanges();
            

            return review;

        }

        [HttpDelete("{ReviewId}")]
        [AllowAnonymous]
        public ActionResult<Review> deleteReview(int ReviewId)
        {
            var review = _reviewRepository.getReviewById(ReviewId);
            if (review == null)
            {
                return NotFound();
            }

            _reviewRepository.deleteReview(ReviewId);
            _reviewRepository.saveChanges();
            return review;
        }
    }
}
