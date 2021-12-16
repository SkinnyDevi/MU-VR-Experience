namespace RatingModel
{
	public class Rating
	{
		private int _ratingId, _userId, _clipId;
		private string _rating;

		public Rating()
		{
			this._ratingId = -1;
			this._userId = -1;
			this._clipId = -1;
			this._rating = "";
		}

		public Rating(int _ratingId, int _userId, int _clipId, string _rating)
		{
			this._ratingId = _ratingId;
			this._userId = _userId;
			this._clipId = _clipId;
			this._rating = _rating;
		}

		public int GetRatingID()
		{
			return this._ratingId;
		}

		public void SetRatingID(int num)
		{
			this._ratingId = num;
		}

		public int GetUserID()
		{
			return this._userId;
		}

		public void SetUserID(int num)
		{
			this._userId = num;
		}		

		public int GetClipID()
		{
			return this._clipId;
		}

		public void SetClipID(int num)
		{
			this._clipId = num;
		}				

		public string GetRating()
		{
			return this._rating;
		}

		public void SetRating(string str)
		{
			this._rating = str;
		}

		public string RatingToJson()
		{
			string uratingID = "\"rating_id\": \"" + this._ratingId + "\",";
			string uUserID = "\"user_id\": \"" + this._userId + "\",";
			string uClipID = "\"clip_id\": \"" + this._clipId + "\",";
			string uRating = "\"rating\": \"" + this._rating + "\"";
			return "{" + uratingID + uUserID + uClipID + uRating + "}";
		}
	}
}

