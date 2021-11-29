namespace UserModel
{
	public class User
	{
		private int id;
		private string email;
		private string username;
		private bool isAdmin;

		public User(int id, string email, string username)
		{
			this.id = id;
			this.email = email;
			this.username = username;
		}

		public User()
		{
			this.id = 0;
			this.email = "--";
			this.username = "--";
		}

		public int GetId()
		{
			return this.id;
		}

		public void SetId(int id)
		{
			this.id = id;
		}

		public string GetEmail()
		{
			return this.email;
		}

		public void SetEmail(string newEmail)
		{
			this.email = newEmail;
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
			string uId = "\"user_id\": \"" + this.id + "\",";
			string uUsername = "\"username\": \"" + this.username + "\",";
			string uEmail = "\"email\": \"" + this.email + "\"";
			return "{" + uId + uUsername + uEmail + "}";
		}
	}
}

