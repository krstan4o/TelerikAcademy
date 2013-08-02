namespace BookstoreCatalogue
{
    using System;
    class QueryEntry
    {
        private String reviewsCount;

        public String BookName { get; set; }

        public override string ToString()
        {
            return string.Format("{0} --> {1}", BookName, ReviewsCount);
        }

        public String ReviewsCount
        {
            get
            {
                if (reviewsCount == "0")
                {
                    return "no reviews";
                }
                return reviewsCount + " reviews";
            }
            set
            {
                this.reviewsCount = value;
            }
        }
    }
}