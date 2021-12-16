namespace UserModel
{
	public class User
	{
		private int _id;
		private string _email;
		private string username;
		private bool isAdmin;

		public User(int _id, string _email, string username)
		{
			this._id = _id;
			this._email = _email;
			this.username = username;
		}

		public User()
		{
			this._id = 0;
			this._email = "--";
			this.username = "--";
		}

		public int GetId()
		{
			return this._id;
		}

		public void SetId(int id)
		{
			this._id = id;
		}

		public string GetEmail()
		{
			return this._email;
		}

		public void SetEmail(string newEmail)
		{
			this._email = newEmail;
		}

		public string GetUsername()
		{
			return this.username;
		}

		public void SetUsername(string newUsername)
		{
			this.username = newUsername;
		}

		public string UserToJson()
		{
			string uId = "\"user_id\": \"" + this._id + "\",";
			string uUsername = "\"username\": \"" + this.username + "\",";
			string uEmail = "\"email\": \"" + this._email + "\"";
			return "{" + uId + uUsername + uEmail + "}";
		}
	}
}

