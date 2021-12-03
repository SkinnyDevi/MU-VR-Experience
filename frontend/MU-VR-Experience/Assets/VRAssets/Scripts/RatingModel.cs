namespace RatingModel
{
	public class Rating
	{
		private int rating_id, user_id, clip_id;
		private string rating;

		public Rating()
		{
			this.rating_id = -1;
			this.user_id = -1;
			this.clip_id = -1;
			this.rating = "";
		}

		public Rating(int rating_id, int user_id, int clip_id, string rating)
		{
			this.rating_id = rating_id;
			this.user_id = user_id;
			this.clip_id = clip_id;
			this.rating = rating;
		}

		public int GetRatingID()
		{
			return this.rating_id;
		}

		public void SetRatingID(int num)
		{
			this.rating_id = num;
		}

		public int GetUserID()
		{
			return this.user_id;
		}

		public void SetUserID(int num)
		{
			this.user_id = num;
		}		

		public int GetClipID()
		{
			return this.clip_id;
		}

		public void SetClipID(int num)
		{
			this.clip_id = num;
		}				

		public string GetRating()
		{
			return this.rating;
		}

		public void SetRating(string str)
		{
			this.rating = str;
		}

		public string RatingToJson()
		{
			string uratingID = "\"rating_id\": \"" + this.rating_id + "\",";
			string uUserID = "\"user_id\": \"" + this.user_id + "\",";
			string uClipID = "\"clip_id\": \"" + this.clip_id + "\",";
			string uRating = "\"rating\": \"" + this.rating + "\"";
			return "{" + uratingID + uUserID + uClipID + uRating + "}";
		}
	}
}

